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
    public int maxHp = 4;
    public int maxHpUp = 1;
    private int currentHp;
    public float damage = 5f;
    public float damageUp = 2f;
    public float moveSpeed = 5f;
    public float speedUp = 2f;
    public bool noDmg = false;
    public int notes = 3;
    public int bombs = 1;
    public SpriteRenderer sprite;
    private bool alive = true;
    private int guiHeartsCount;
    public GameObject fullHearts;
    public GameObject emptyHearts;
    public TextMeshProUGUI notesText;
    public TextMeshProUGUI bombsText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI damageText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        currentHp = maxHp;
        UpdateHud();
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
                currentBulletCooldown = bulletCooldown;
            }

            if(Input.GetKey("left")){
                FireLeft();
                currentBulletCooldown = bulletCooldown;
            }

            if(Input.GetKey("down")){
                FireDown();
                currentBulletCooldown = bulletCooldown;
            }

            if(Input.GetKey("up")){
                FireUp();
                currentBulletCooldown = bulletCooldown;
            }
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

    public void Heal(){
        if(currentHp < maxHp){
            currentHp += 1;
            UpdateHearts();
        }
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