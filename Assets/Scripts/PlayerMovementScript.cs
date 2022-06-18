using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementScript : MonoBehaviour
{
    public float moveSpeed = 5;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    public Transform firepoint;
    public GameObject bulletPrefab;
    public float bulletCooldown = 0.4f;
    private float currentBulletCooldown = 0f;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
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
        Instantiate(bulletPrefab, firepoint.position, rotation);
    }

    private void FireUp(){
        Quaternion rotation = Quaternion.LookRotation(firepoint.forward, firepoint.right*-1);
        Instantiate(bulletPrefab, firepoint.position, rotation);
    }

    private void FireLeft(){
        Quaternion rotation = Quaternion.LookRotation(firepoint.forward, firepoint.up*-1);
        Instantiate(bulletPrefab, firepoint.position, rotation);
    }

    private void FireRight(){
        Quaternion rotation = Quaternion.LookRotation(firepoint.forward, firepoint.up);
        Instantiate(bulletPrefab, firepoint.position, rotation);
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
