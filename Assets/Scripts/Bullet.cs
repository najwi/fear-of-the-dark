using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public int dmg = 1;

    private const float Lifetime = 10f;  // Time after which bullet is destroyed

    void Start()
    {
        rb.velocity = transform.right * speed;
        DeleteAfterLifetime(Lifetime);
    }

    private void OnCollisionEnter2D(Collision2D col){
        Destroy(gameObject);
    }

    private IEnumerator DeleteAfterLifetime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

}
