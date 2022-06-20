using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class FireballScript : MonoBehaviour
{
    public Vector2 direction;
    public int damage;
    public float moveSpeed = 6;
    public PlayerMovementScript player;

    private const float Lifetime = 10f;  // Time after which fireball is destroyed
    private Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(DeleteAfterLifetime(Lifetime));
    }

    private IEnumerator DeleteAfterLifetime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Vector2 position = (Vector2)transform.position + (direction * moveSpeed * Time.deltaTime);
        rb.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            if (player)
                player.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
