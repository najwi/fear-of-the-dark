using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    private RoomTemplates templates;
    private bool spawned = false;
    private GameObject parRoom;

    void Start()
    {
        // Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        parRoom = transform.parent.gameObject;
        
        // Invoke("SpawnRoom", 0.1f);
        // if (roomsSpawned < maxRoomQuantity){
        //     // SpawnRoom();
        //     Invoke("SpawnRoom", 1.1f);
        // }else{
        //     // Debug.Log("closedroom");
        //     // SpawnClosedRoom();
        //     Invoke("SpawnClosedRoom", 0.1f);
        // }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Room")){
            Destroy(gameObject);
        }
        if (other.CompareTag("SpawnPoint")){
            GameObject parentRoom = transform.parent.gameObject;
            GameObject otherRoom = other.transform.parent.gameObject;

            Destroy(gameObject);
            int roomType = parRoom.GetComponent<RoomGenerator>().roomType;
            switch (roomType){
                case 1:
                    parRoom = Instantiate(templates.topClosedRoom, parRoom.transform.position, Quaternion.identity);
                    // Destroy(parRoom);
                    break;
                case 2:
                    parRoom = Instantiate(templates.rightClosedRoom, parRoom.transform.position, Quaternion.identity);
                    // Destroy(parRoom);
                    break;
                case 3:
                    parRoom = Instantiate(templates.bottomClosedRoom, parRoom.transform.position, Quaternion.identity);
                    // Destroy(parRoom);
                    break;
                case 4:
                    parRoom = Instantiate(templates.leftClosedRoom, parRoom.transform.position, Quaternion.identity);
                    // Destroy(parRoom);
                    break;
                case 0:
                    Debug.Log("From opened");
                    break;
                default:
                    break;
            }
        }
    }
}
