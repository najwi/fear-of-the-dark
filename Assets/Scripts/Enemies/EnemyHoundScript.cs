using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class EnemyHoundScript : MonoBehaviour
{
    public bool isNotAttacking = true;
    public float moveSpeed = 2f;
    public float attackRange = 1.5f;
    public float attackEscapeRange = 2.5f;
    public float attackDelay = 1.5f;

    private GameObject player;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private Vector2 movement;
    private float lastAttackTime = 0;

    void Start()
    {
        player = GameObject.Find("Player");
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
                    Debug.Log("Start Attacking");
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
                anim.SetBool("isMoving", true);
                direction.Normalize();  // Get direction
                movement = direction;
            }

            SwitchCharacterDirection(angle);
        }
    }

    private void FixedUpdate()
    {
        if (isNotAttacking)
        {
            MoveCharacter(movement);
        }
    }

    private void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
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

    private bool EscapedAttackRange(Vector2 direction)
    {
        float distance = Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);
        return distance < attackEscapeRange;
    }


    private void FinalizeAttack()
    {
        Debug.Log("End of attack");
        isNotAttacking = true;

        Vector2 direction = player.transform.position - transform.position;
        if (EscapedAttackRange(direction))
        {
            Debug.Log("Hit");
            player.GetComponent<PlayerMovementScript>().TakeDamage(1);
        }
        else
        {
            Debug.Log("Missed");
        }
    }
}
