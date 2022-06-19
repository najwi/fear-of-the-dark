using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("Obstacle")){
            Destroy(col.gameObject);
        }
        else if(col.gameObject.CompareTag("Enemy")){
            //TODO DAMAGE TO ENEMY
        }else if(col.gameObject.CompareTag("Player")){
            col.GetComponent<PlayerMovementScript>().TakeDamage(1);
        }

    }
}
