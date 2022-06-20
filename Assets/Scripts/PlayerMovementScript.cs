using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    public Transform firepoint;
    public GameObject bulletPrefab;
    public float bulletCooldown = 0.4f;
    private float currentBulletCooldown = 0f;
    public Animator animator;
    public static int maxHp = 4;
    public int maxHpUp = 1;
    private static int currentHp;
    public static int damage = 3;
    public int damageUp = 1;
    public static float moveSpeed = 5f;
    public float speedUp = 2f;
    public bool noDmg = false;
    public static int notes = 3;
    public static int bombs = 1;
    public SpriteRenderer sprite;
    private bool alive = true;
    private int guiHeartsCount;
    public GameObject fullHearts;
    public GameObject emptyHearts;
    public TextMeshProUGUI notesText;
    public TextMeshProUGUI bombsText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI damageText;
    public GameObject bombPrefab;
    public int itemPrice = 10;
    public AudioSource itemPickupSound;
    public AudioSource shootSound;
    private bool paused;
    public GameObject pauseText;
    public GameObject player2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        currentHp = maxHp;
        UpdateHud();
        AudioListener.volume = PlayerPrefs.GetFloat("volume", 0.5f);
        paused = false;
    }

    private void Update()
    {
        if(alive)
            GetInputs();

        if(currentBulletCooldown > 0){
            currentBulletCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if(alive)
            Move();
    }

    private void UpdateHud(){
        UpdateHearts();
        damageText.text = damage.ToString();
        speedText.text = moveSpeed.ToString();
        bombsText.text = bombs.ToString();
        notesText.text = notes.ToString();
    }
    
    private void UpdateHearts(){
        for(int i = 0; i < fullHearts.transform.childCount; i++){
            fullHearts.transform.GetChild(i).gameObject.SetActive(i<currentHp);
            emptyHearts.transform.GetChild(i).gameObject.SetActive(i>=currentHp && i<maxHp);
        }
    }

    private void GetInputs()
    {
        if(Input.GetKeyDown("c")){
            noDmg = !noDmg;
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            Pause();
        }

        if(paused)
            return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY);

        if(moveX > 0){
            animator.SetBool("RunLeft", false);
            animator.SetBool("RunBack", false);
            animator.SetBool("RunForward", false);
            animator.SetBool("RunRight", true);
        }else
        if(moveX < 0){
            animator.SetBool("RunRight", false);
            animator.SetBool("RunBack", false);
            animator.SetBool("RunForward", false);
            animator.SetBool("RunLeft", true);
        }else
        if(moveY > 0){
            animator.SetBool("RunRight", false);
            animator.SetBool("RunLeft", false);
            animator.SetBool("RunForward", false);
            animator.SetBool("RunBack", true);
        }else
        if (moveY < 0){
            animator.SetBool("RunRight", false);
            animator.SetBool("RunLeft", false);
            animator.SetBool("RunBack", false);
            animator.SetBool("RunForward", true);
        }
        else{
            animator.SetBool("RunRight", false);
            animator.SetBool("RunForward", false);
            animator.SetBool("RunLeft", false);
            animator.SetBool("RunBack", false);
        }

        if(currentBulletCooldown <= 0){        
            if(Input.GetKey("right")){
                FireRight();
                shootSound.Play();
                currentBulletCooldown = bulletCooldown;
            }else

            if(Input.GetKey("left")){
                FireLeft();
                shootSound.Play();
                currentBulletCooldown = bulletCooldown;
            }else

            if(Input.GetKey("down")){
                FireDown();
                shootSound.Play();
                currentBulletCooldown = bulletCooldown;
            }else

            if(Input.GetKey("up")){
                FireUp();
                shootSound.Play();
                currentBulletCooldown = bulletCooldown;
            }
        }

        if(Input.GetKeyDown("e")){
            PlaceBomb();
        }
    }

    private void Pause(){
        if(!paused){
            Time.timeScale = 0;
            paused = true;
            pauseText.SetActive(true);
        }else{
            Time.timeScale = 1;
            paused = false;
            pauseText.SetActive(false);
        }
    }

    private void PlaceBomb(){
        if(bombs > 0){
            bombs--;
            bombsText.text = bombs.ToString();
            Instantiate(bombPrefab, firepoint.position, firepoint.rotation);
        }
    }

    private void FireDown(){
        Quaternion rotation = Quaternion.LookRotation(firepoint.forward, firepoint.right);
        var go = Instantiate(bulletPrefab, firepoint.position, rotation);
        go.GetComponent<Bullet>().dmg = damage;
    }

    private void FireUp(){
        Quaternion rotation = Quaternion.LookRotation(firepoint.forward, firepoint.right*-1);
        var go = Instantiate(bulletPrefab, firepoint.position, rotation);
        go.GetComponent<Bullet>().dmg = damage;
    }

    private void FireLeft(){
        Quaternion rotation = Quaternion.LookRotation(firepoint.forward, firepoint.up*-1);
        var go = Instantiate(bulletPrefab, firepoint.position, rotation);
        go.GetComponent<Bullet>().dmg = damage;
    }

    private void FireRight(){
        Quaternion rotation = Quaternion.LookRotation(firepoint.forward, firepoint.up);
        var go = Instantiate(bulletPrefab, firepoint.position, rotation);
        go.GetComponent<Bullet>().dmg = damage;
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public void TakeDamage(int dmg){
        //Cheat code
        if(noDmg)
            return;

        currentHp -= dmg;
        if(currentHp <= 0){
            alive = false;
            animator.SetTrigger("Die");
        }else{
            //Flash player sprite on damage
            StartCoroutine(StrobeColorHelper(0, 7, sprite, Color.white, new Color(1,1,1, 0.1f)));
        }
        UpdateHearts();
    }

    public void Die(){
        SceneManager.LoadScene("DeathScene");
    }

    public void TryBuyDamageUp(){
        if(notes >= itemPrice){
            itemPickupSound.Play();
            notes -= itemPrice;
            DamageUp();
            notesText.text = notes.ToString();
        }
    }

    public void TryBuyHealthUp(){
        if(notes >= itemPrice && maxHp < fullHearts.transform.childCount){
            itemPickupSound.Play();
            notes -= itemPrice;
            HealthUp();
            notesText.text = notes.ToString();
        }
    }

    public void TryBuySpeedUp(){
        if(notes >= itemPrice){
            itemPickupSound.Play();
            notes -= itemPrice;
            SpeedUp();
            notesText.text = notes.ToString();
        }
    }

    public void DamageUp(){
        damage += damageUp;
        damageText.text = damage.ToString();
    }

    public void SpeedUp(){
        moveSpeed += speedUp;
        speedText.text = moveSpeed.ToString();
    }

    public void HealthUp(){
        if(maxHp < 10)
            maxHp += maxHpUp;
        if(maxHp - currentHp > 0)
            currentHp += 1;
        UpdateHearts();
    }

    public bool TryHeal(){
        if(currentHp < maxHp){
            currentHp += 1;
            UpdateHearts();
            return true;
        }
        return false;
    }

    public void BombPickup(){
        bombs += 1;
        bombsText.text = bombs.ToString();
    }

    public void NotePickup(){
        notes += 1;
        notesText.text = notes.ToString();
    }

    private IEnumerator StrobeColorHelper( int _i, int _stopAt, SpriteRenderer _mySprite, Color _color, Color _toStrobe)
    {
        if(_i <= _stopAt)
        {
            if (_i % 2 == 0)
                _mySprite.color = _toStrobe;
            else
                _mySprite.color = _color;
 
            yield return new WaitForSeconds(.1f);
            StartCoroutine(StrobeColorHelper( (_i+1), _stopAt, _mySprite, _color, _toStrobe));
        }
    }
}
