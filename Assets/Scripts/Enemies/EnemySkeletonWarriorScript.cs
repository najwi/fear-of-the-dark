using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class EnemySkeletonWarriorScript : MonoBehaviour, TakeBombDamageDecorator
{
    public bool isNotAttacking = true;
    public float moveSpeed = 1.5f;
    public float playerComfortZone = 4f;
    public float wallComfortZone = 0.5f;
    public int health = 12;
    public int attackDamage = 1;
    public float attackRange = 2.5f;
    public float attackEscapeRange = 2.6f;
    public float moveDelay = 5f;
    public float attackDelay = 2.5f;
    public float protectDistance = 3f;

    private GameObject player;
    private GameObject protectedGog;
    private PlayerMovementScript playerScript;
    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 movement;
    private float lastAttackTime = -2.5f;
    private bool stop = false;
    private bool isDead = false;
    private bool isAnyGogAlive = false;

    private void OnEnable()
    {
        SetProtectedGog();
    }

    private void SetProtectedGog()
    {
        List<GameObject> gogs = FindGogs();
        if (gogs.Count > 0)
        {
            protectedGog = FindClosestGog(gogs);
            isAnyGogAlive = true;
        }
        else
        {
            isAnyGogAlive = false;
        }
    }

    private List<GameObject> FindGogs()
    {
        GameObject enemiesContainer = transform.parent.gameObject;
        List<GameObject> gogs = new List<GameObject>();
        foreach (Transform enemy in enemiesContainer.transform)
        {
            Component gog = enemy.GetComponent(typeof(EnemyGogScript));
            if (gog)
                gogs.Add(gog.gameObject);
        }
        return gogs;
    }

    private GameObject FindClosestGog(List<GameObject> gogs)
    {
        GameObject gog = gogs[0];
        for (int i = 1; i < gogs.Count; i++)
        {
            if (GetDistance(gog) > GetDistance(gogs[i]))
                gog = gogs[i];
        }
        return gog;
    }

    private float GetDistance(GameObject gog)
    {
        float x = transform.position.x, y = transform.position.y;
        float x2 = gog.transform.position.x, y2 = gog.transform.position.y;

        return Mathf.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));
    }

    private float GetDistance(Transform a, Transform b)
    {
        float x = a.position.x, y = a.position.y;
        float x2 = b.position.x, y2 = b.position.y;

        return Mathf.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));
    }

    private float GetDistance(Vector2 a, Vector2 b)
    {
        float x = a.x, y = a.y;
        float x2 = b.x, y2 = b.y;

        return Mathf.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));
    }

    private void CheckIfGogStillAlive()
    {
        if (protectedGog)
        {
            return;
        }
        else
        {
            SetProtectedGog();
        }
    }

    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerMovementScript>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        isNotAttacking = false;
        StartCoroutine(EnableActionOnStart());
    }

    private IEnumerator EnableActionOnStart()
    {
        yield return new WaitForSeconds(1);
        isNotAttacking = true;
    }

    private void Update()
    {
        CheckIfGogStillAlive();
        if (isAnyGogAlive)
        {
            SwitchCharacterDirection(player.transform.position - transform.position);
            Protect();
            float currentTime = Time.fixedTime;
            Vector2 direction = player.transform.position - transform.position;
            if (InAttackRange(direction))  // When close to player
            {
                if (currentTime - lastAttackTime > attackDelay)  // If can attack
                {
                    lastAttackTime = currentTime;
                    isNotAttacking = false;
                    anim.SetTrigger("attack");
                    stop = true;
                }
                // Stop moving if close
                movement = Vector2.zero;
                anim.SetBool("isMoving", false);
            }
            return;
        }
        else if (isNotAttacking)
        {
            Vector2 direction = player.transform.position - transform.position;
            float currentTime = Time.fixedTime;

            if (InAttackRange(direction))  // When close to player
            {
                if (currentTime - lastAttackTime > attackDelay)  // If can attack
                {
                    lastAttackTime = currentTime;
                    isNotAttacking = false;
                    anim.SetTrigger("attack");
                    stop = true;
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
            SwitchCharacterDirection(direction);
        }
        else
        {
            //Debug.Log("Probably attacking");  
        }
    }

    private bool InAttackRange(Vector2 direction)
    {
        float distance = Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);
        return distance < attackRange;
    }

    private void FixedUpdate()
    {
        if (stop)
            return;
        MoveCharacter(movement);
    }

    private void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }


    private void SwitchCharacterDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Which side should enemy face (front to player when not moving)
        if (angle < 90 && angle > -90)
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }

    private void Protect()
    {
        Vector2 gogPos = protectedGog.transform.position;
        Vector2 direction = (Vector2)player.transform.position - gogPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 45 && angle > -45)  // Right
        {
            direction = new Vector2(gogPos.x + 3, gogPos.y) - (Vector2)transform.position;
        }
        else if (angle > 45 && angle < 135)  // Top
        {
            direction = new Vector2(gogPos.x, gogPos.y + 4) - (Vector2)transform.position;
        }
        else if (angle > 135 && angle > - 135)  // Left
        {
            direction = new Vector2(gogPos.x - 3, gogPos.y) - (Vector2)transform.position;
        }
        else  // Bottom
        {
            direction = new Vector2(gogPos.x, gogPos.y - 4) - (Vector2)transform.position;
        }
        if (GetDistance(transform, protectedGog.transform) > protectDistance || PlayerCanSeeGog(player, protectedGog))
        {
            direction.Normalize();
            movement = direction;
            anim.SetBool("isMoving", true);
        }
        else
        {
            movement = Vector2.zero;
            anim.SetBool("isMoving", false);
        }


        //// Temporarily in middle
        //if (GetDistance(transform, protectedGog.transform) > protectDistance || PlayerCanSeeGog(player, protectedGog))
        //{
        //    Debug.Log("too far or can see");
        //    Vector2 temp = (protectedGog.transform.position + player.transform.position) / 2;
        //    Vector2 pointInMiddle = new Vector2(temp.x * 5 , temp.y);
        //    Vector2 direction = pointInMiddle - (Vector2)transform.position;
        //    direction.Normalize();
        //    movement = direction;
        //    anim.SetBool("isMoving", true);
        //}
        //else
        //{
        //    movement = Vector2.zero;
        //    anim.SetBool("isMoving", false);
        //}
    }

    private bool PlayerCanSeeGog(GameObject source, GameObject target)
    {
        var dir = target.transform.position - source.transform.position;
        dir.Normalize();
        RaycastHit2D hit;
        if (hit = Physics2D.Raycast(source.transform.position, dir, 50, layerMask: LayerMask.GetMask("Enemy")))
        {
            if (hit.collider.gameObject.CompareTag("EnemyDefender"))
                return false;
            return true;
        }
        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AllyProjectile"))
        {
            movement = Vector2.zero;
            stop = true;
            if (collision.otherCollider.GetType() == typeof(BoxCollider2D))
            {
                TakeDamage(collision.gameObject.GetComponent<Bullet>().dmg);
            }
            else if (collision.otherCollider.GetType() == typeof(CapsuleCollider2D))
            {
                BlockDamage();
            }
        }
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


    private void BlockDamage()
    {
        anim.SetTrigger("block");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        isNotAttacking = true;
        if (health <= 0 && !isDead)
        {
            anim.SetTrigger("die");
            stop = true;
            isDead = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            movement = Vector2.zero;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
        else
            anim.SetTrigger("hit");
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

    public void StartMovingAgain()
    {
        stop = false;
    }
}
