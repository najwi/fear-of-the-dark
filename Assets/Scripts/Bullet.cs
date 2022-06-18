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

    void OnTriggerEnter2D(Collider2D col){
        if(col.name != "Player" && !col.CompareTag("Room")){
            Destroy(gameObject);
        }
    }
}
