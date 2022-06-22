using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagement : MonoBehaviour
{
    public bool visited = false;
    public bool roomFinished = false;
    private bool control = true;
    private GameObject closedDoor;
    private GameObject openedDoor;
    private RoomTemplates templates;

    void Start(){
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }

    void Update(){
        foreach (Transform roomElem in gameObject.transform){
            if (roomElem.gameObject.CompareTag("ObstacleTemplate")){
                foreach (Transform templateElem in roomElem){
                    if(templateElem.gameObject.name == "Enemies"){
                        if (templateElem.childCount == 0){
                            roomFinished = true;
                        }
                    }
                }
                // if (roomElem.childCount <= 2){
                //     roomFinished = true;
                // }
            }
        }
        if (roomFinished && control){
            OpenDoors();
            roomFinished = false;
            control = false;
        }
    }

    public void CloseDoors(){
        foreach (Transform roomElem in gameObject.transform){
            if (roomElem.gameObject.CompareTag("Door")){
                SpriteRenderer doorSpriteRenderer = roomElem.gameObject.GetComponent<SpriteRenderer>();
                doorSpriteRenderer.sprite = templates.closedDoor;
                roomElem.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    public void OpenDoors(){
        Debug.Log("Open doors");
        Debug.Log(gameObject.name);
        Debug.Log(gameObject.transform.position);
        foreach (Transform roomElem in gameObject.transform){
            if (roomElem.gameObject.CompareTag("Door")){
                SpriteRenderer doorSpriteRenderer = roomElem.gameObject.GetComponent<SpriteRenderer>();
                doorSpriteRenderer.sprite = templates.openedDoor;
                roomElem.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

}
