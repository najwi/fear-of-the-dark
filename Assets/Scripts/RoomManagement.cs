using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagement : MonoBehaviour
{
    public bool visited = false;
    public bool roomFinished = false;
    private GameObject closedDoor;
    private GameObject openedDoor;
    private RoomTemplates templates;

    void Start(){
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }

    void Update(){
        int enemiesCount = 0;
        foreach (Transform roomElem in gameObject.transform){
            if (roomElem.gameObject.CompareTag("ObstacleTemplate")){
                if (roomElem.childCount <= 2){
                    roomFinished = true;
                }
            }
        }
        if (roomFinished){
            OpenDoors();
            roomFinished = false;
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
        foreach (Transform roomElem in gameObject.transform){
            if (roomElem.gameObject.CompareTag("Door")){
                SpriteRenderer doorSpriteRenderer = roomElem.gameObject.GetComponent<SpriteRenderer>();
                doorSpriteRenderer.sprite = templates.openedDoor;
                roomElem.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

}