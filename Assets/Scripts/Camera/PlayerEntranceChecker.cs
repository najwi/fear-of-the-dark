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
            if (other.gameObject.name == "Player"){
                GameObject player2 = other.gameObject.GetComponent<PlayerMovementScript>().player2;
                if (player2 != null){
                    player2.transform.position = other.gameObject.transform.position;
                }
                
            }else{
                other.gameObject.GetComponent<Player2Movement>().player.transform.position = other.gameObject.transform.position;
            }
        }
        GameObject par = gameObject.transform.parent.gameObject;
        string name = "";
        foreach (Transform roomChild in par.transform){
            if (roomChild.gameObject.CompareTag("ObstacleTemplate")){
                name = roomChild.name;
            }
        }
        
        if (par.name != "Shop" ){
            RoomManagement parentRoomManager = par.GetComponent<RoomManagement>();
            if (!parentRoomManager.visited){
                parentRoomManager.visited = true;
                //MediumTemplate1(Clone)
                if(name != "MediumTemplate1(Clone)"){
                    parentRoomManager.CloseDoors();
                }
                
            }
        }
        foreach (Transform roomChild in par.transform){
            if (roomChild.gameObject.CompareTag("ObstacleTemplate")){
                foreach (Transform obstacleChild in roomChild){
                    if (obstacleChild.gameObject.name == "Enemies"){
                        foreach(Transform enemy in obstacleChild){
                            enemy.gameObject.SetActive(true);
                        }
                    }
                }
                
            }
        }
    }
}
