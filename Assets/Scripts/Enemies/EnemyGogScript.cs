using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class EnemyGogScript : MonoBehaviour
{
    public bool isNotAttacking = true;
    public float moveSpeed = 1.5f;
    public float playerComfortZone = 4f;
    public float wallComfortZone = 0.5f;
    public int health = 6;
    public int attackDamage = 1;
    public float moveDelay = 5f;
    public float attackDelay = 2.5f;

    public GameObject projectileRight;
    public GameObject projectileUp;
    public GameObject projectileBottom;

    private GameObject player;
    private PlayerMovementScript playerScript;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private Vector2 movement;
    private float lastMoveTime = -5f;
    private float lastAttackTime = -2.5f;
    private bool isDead = false;

    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerMovementScript>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        // Add delay so all shots are not fired at player at once if multiple enemies are present
        lastAttackTime += Random.Range(0, 5);
    }

    void Update()
    {
        if (isNotAttacking)
        {
            float currentTime = Time.fixedTime;
            Vector2 direction = player.transform.position - transform.position;
            if (currentTime - lastAttackTime > attackDelay)  // can attack
            {
                lastAttackTime = currentTime;
                Attack();
            }
            else if (TooCloseToPlayer(direction))
            {
                MoveAway(direction);
                SwitchCharacterDirection(movement);
            }
            else if (currentTime  - lastMoveTime > moveDelay)
            {
                lastMoveTime = currentTime;
                // Move somewhere
               
            }
            // Else do nothing
            else
            {
                movement = Vector2.zero;
                anim.SetBool("isMoving", false);
                SwitchCharacterDirection(player.transform.position - transform.position);
            }
        }
    }

    private void Attack()
    {
        movement = Vector2.zero;
        anim.SetBool("isMoving", false);
        anim.SetTrigger("attack");
        isNotAttacking = false;
        SwitchCharacterDirection(player.transform.position - transform.position);
    }

    private void MoveAway(Vector2 direction)
    {
        direction = new Vector2(direction.x, direction.y);
        // Move away (if possible - somehow check)
        direction.x *= -1;
        direction.y *= -1;
        direction.Normalize();
        anim.SetBool("isMoving", true);

        // Check if can move
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
        if (hit.collider != null)
        {
            float distance = GetDistanceBetween(hit, transform);
            if (distance < wallComfortZone)  // Too close to wall, move sideways
            {
                int random = Random.Range(0, 4);
                //switch (random)
                //{
                //    case 0:  // Up
                //        direction.y += 2;
                //        break;
                //    case 1:  // Right
                //        direction.x -= 2;
                //        break;
                //    case 2:  // Bottom
                //        direction.y -= 2;
                //        break;
                //    case 3:  // Left
                //        direction.x -= 2;
                //        break;
                //}
                //direction.Normalize();
            }
        }

        movement = direction;
    }

    private void SwitchCharacterDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Which side should enemy face (front to player when not moving)
        if (angle < 90 && angle > -90)
        {
            if (sprite.flipX)
                sprite.flipX = false;
        }
        else
        {
            if (!sprite.flipX)
                sprite.flipX = true;
        }
    }

    private float GetDistanceBetween(RaycastHit2D target, Transform origin)
    {
        Vector2 distance = target.point - (Vector2)origin.position;
        return Mathf.Sqrt(distance.x * distance.x + distance.y * distance.y);
    }

    private void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    private void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private bool TooCloseToPlayer(Vector2 direction)
    {
        float distance = Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);
        return distance < playerComfortZone;
    }

    public void PerformRangedAttack()
    {
        // Instantiate and throw fireball
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        SpawnFireball(direction, angle);
    }

    public void FinalizeAttack()
    {
        isNotAttacking = true;
    }

    private GameObject SpawnFireball(Vector2 direction, float angle)
    {
        GameObject fireball;

        if (angle < 45 && angle > -45)  // Right
            fireball = Instantiate(projectileRight);
        else if (angle > 45 && angle < 135)  // Up
            fireball = Instantiate(projectileUp);
        else if (angle < -45 && angle > -135)  // Bottom
            fireball = Instantiate(projectileBottom);
        else  // Left
        {
            fireball = Instantiate(projectileRight);
            fireball.GetComponent<SpriteRenderer>().flipX = true;
        }

        fireball.transform.position = transform.position;

        FireballScript script = fireball.AddComponent<FireballScript>();
        script.damage = attackDamage;
        script.direction = direction;
        script.player = playerScript;
        return fireball;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AllyProjectile"))
        {
            TakeDamage(collision.gameObject.GetComponent<Bullet>().dmg);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        isNotAttacking = true;
        if (health <= 0 && !isDead)
        { 
            anim.SetTrigger("die");
            isDead = true;
        }
        else
            anim.SetTrigger("hit");
    }

    private void TakeDamageFinalize()
    {
        if (health <= 0)
            Destroy(gameObject);
    }
}
