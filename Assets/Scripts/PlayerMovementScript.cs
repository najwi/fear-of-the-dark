using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        currentHp = maxHp;
    }

    private void Update()
    {
        GetInputs();

        if(currentBulletCooldown > 0){
            currentBulletCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Move();
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
            animator.SetTrigger("Die");
        }
    }

    public void Die(){
        SceneManager.LoadScene("DeathScene");
    }

    public void DamageUp(){
        damage += damageUp;
    }

    public void SpeedUp(){
        moveSpeed += speedUp;
    }

    public void HealthUp(){
        maxHp += maxHpUp;
        currentHp += maxHpUp;
    }

    public void Heal(){
        if(currentHp < maxHp){
            currentHp += 1;
        }
    }
}
