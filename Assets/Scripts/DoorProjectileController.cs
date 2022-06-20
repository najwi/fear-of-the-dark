using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorProjectileController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("AllyProjectile")){
            Destroy(other.gameObject);
        }
    }
}
