using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntranceChecker : MonoBehaviour
{
    public CameraController cam;
    public Transform destinationView;

    void Start(){
        cam =  FindObjectOfType<CameraController>();
        destinationView = transform.parent.transform;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            cam.Move(destinationView);
        }
        if(other.CompareTag("AllyProjectile")){
            Destroy(other.gameObject);
        }
    }
}
