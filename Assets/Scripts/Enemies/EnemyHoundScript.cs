using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class EnemyHoundScript : MonoBehaviour, TakeBombDamageDecorator
{
    public bool isNotAttacking = true;
    public float moveSpeed = 2f;
    public float dodgeSpeed = 10;
    public float dodgeTime = 0.10f;
    public int health = 3;
    public int attackDamage = 1;
    public float attackRange = 1.8f;
    public float attackEscapeRange = 2.8f;
    public float attackDelay = 1.5f;
    public float dodgeDelay = 2.2f;

    private GameObject player;
    private PlayerMovementScript playerScript;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private Vector2 movement;
    private Vector2 dodgeMovement;
    private bool dodge = false;
    private float lastAttackTime = -1.5f;
    private float lastDodgeTime = -3f;

    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerMovementScript>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (isNotAttacking)
        {
            Vector2 direction = player.transform.position - transform.position;
            float currentTime = Time.fixedTime;

            // For now rotate - later split angle into 4 sections and use appropriate sprite
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (InAttackRange(direction))  // When close to player
            {
                if (currentTime - lastAttackTime > attackDelay)  // If can attack
                {
                    lastAttackTime = currentTime;
                    isNotAttacking = false;
                    anim.SetTrigger("attack");
                }
                // Stop moving if close
                movement = Vector2.zero;
                anim.SetBool("isMoving", false);
            }
            else  // If not close to player move
            {
                direction.Normalize();  // Get direction
                movement = direction;
                anim.SetBool("isMoving", true);
            }

            SwitchCharacterDirection(angle);
        }
    }

    private void FixedUpdate()
    {
        if (isNotAttacking)
        {
            if (dodge)
                MoveWithDodge(dodgeMovement);
            else
                MoveCharacter(movement);
        }
    }

    private void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void MoveWithDodge(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * dodgeSpeed * Time.deltaTime));
    }

    private void SwitchCharacterDirection(float angle)
    {
        // Which side should enemy face (always front to player)
        if (angle < 90 && angle > - 90)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }

    private bool InAttackRange(Vector2 direction)
    {
        float distance = Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);
        return distance < attackRange;
    }

    private bool InAttackEscapeRange(Vector2 direction)
    {
        float distance = Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);
        return distance < attackEscapeRange;
    }
    private void FinalizeAttack()
    {
        Vector2 direction = player.transform.position - transform.position;
        if (InAttackEscapeRange(direction))
        {
            playerScript.TakeDamage(attackDamage);
        }
        else
        {
        }
        isNotAttacking = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AllyProjectile"))
        {
            float currentTime = Time.fixedTime;
            if (currentTime - lastDodgeTime > dodgeDelay)  // If can dodge
            {
                Dodge(collision.gameObject);
                lastDodgeTime = currentTime;
            }
        }
    }

    private void Dodge(GameObject projectile)
    {
        Vector2 direction = projectile.transform.position - transform.position;
        // Debug.Log("Direction: " + direction + "   Angle: " + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        float x = direction.x, y = direction.y;
        if (direction.y < 0)
            x *= -1;
        if (direction.x < 0)
            y *= -1;
        direction.Normalize();
        dodgeMovement = new Vector2(y, x);
        dodge = true;
        StartCoroutine(StopDodge(dodgeTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AllyProjectile"))
        {
            TakeDamage(collision.gameObject.GetComponent<Bullet>().dmg);
        }
    }

    private IEnumerator StopDodge(float time)
    {
        yield return new WaitForSeconds(time);
        dodge = false;
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        anim.SetTrigger("die");
    }

    private void TakeDamageFinalize()
    {
        if (health <= 0)
            Destroy(gameObject);
    }

    public bool TakeBombDamage(int damage)
    {
        TakeDamage(damage);
        return true;
    }
}
