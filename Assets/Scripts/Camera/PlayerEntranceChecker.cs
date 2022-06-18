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
        if(other.tag == "Player"){
            cam.Move(destinationView);
        }
    }
}
