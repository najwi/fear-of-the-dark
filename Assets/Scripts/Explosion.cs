using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            Destroy(col.gameObject);
        }
        else if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("EnemyDefender"))
        {
            col.gameObject.GetComponent<TakeBombDamageDecorator>().TakeBombDamage(20);
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerMovementScript>().TakeDamage(1);
        }

    }
}
