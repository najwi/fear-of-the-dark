using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public int dmg = 1;
    
    void Start()
    {
        rb.velocity = transform.right * speed;
    }
}
