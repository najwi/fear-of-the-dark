using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagement : MonoBehaviour
{
    public bool visited = false;
    public bool roomFinished = false;
<<<<<<< HEAD
    private bool control = true;
=======
    public AudioSource openDoorsSound;
    public AudioSource closeDoorsSound;
>>>>>>> dc560072c19735260be25ac1973f9f5e07349b07
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
        closeDoorsSound.Play();
        foreach (Transform roomElem in gameObject.transform){
            if (roomElem.gameObject.CompareTag("Door")){
                SpriteRenderer doorSpriteRenderer = roomElem.gameObject.GetComponent<SpriteRenderer>();
                doorSpriteRenderer.sprite = templates.closedDoor;
                roomElem.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    public void OpenDoors(){
        openDoorsSound.Play();
        Debug.Log("Open doors" + gameObject.name);
        foreach (Transform roomElem in gameObject.transform){
            if (roomElem.gameObject.CompareTag("Door")){
                SpriteRenderer doorSpriteRenderer = roomElem.gameObject.GetComponent<SpriteRenderer>();
                doorSpriteRenderer.sprite = templates.openedDoor;
                roomElem.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

}
