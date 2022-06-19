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
        // if(other.CompareTag("EnemyProjectile")){
        //     Destroy(other.gameObject);
        // }
        GameObject par = gameObject.transform.parent.gameObject;
        
        if (par.name != "Shop"){
            RoomManagement parentRoomManager = par.GetComponent<RoomManagement>();
            if (!parentRoomManager.visited){
                parentRoomManager.visited = true;
                parentRoomManager.CloseDoors();
            }
        }
        foreach (Transform roomChild in par.transform){
            if (roomChild.gameObject.CompareTag("ObstacleTemplate")){
                foreach (Transform obstacleChild in roomChild){
                    if (obstacleChild.gameObject.name == "Enemies"){
                        foreach(Transform enemy in obstacleChild){
                            enemy.gameObject.SetActive(true);
                        }
                        // obstacleChild.gameObject.SetActive(true);
                    }
                }
                
            }
        }
    }
}
