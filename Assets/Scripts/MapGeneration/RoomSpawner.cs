using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    private RoomTemplates templates;
    private bool spawned = false;

    // // Start is called before the first frame update
    void Start()
    {
        // Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        
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
            // if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false){
            //     Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
            //     Destroy(gameObject);
            // }
            // spawned = true;
            // Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (other.CompareTag("SpawnPoint")){
            GameObject parentRoom = transform.parent.gameObject;
            Debug.Log(parentRoom.name);
            Debug.Log(gameObject.name);
            GameObject otherRoom = other.transform.parent.gameObject;
            Debug.Log(otherRoom.name);
            Debug.Log(other.gameObject.name);

            Destroy(gameObject);
            int roomType = parentRoom.GetComponent<RoomGenerator>().roomType;
            switch (roomType){
                case 1:
                    Instantiate(templates.topClosedRoom, parentRoom.transform.position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(templates.rightClosedRoom, parentRoom.transform.position, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(templates.bottomClosedRoom, parentRoom.transform.position, Quaternion.identity);
                    break;
                case 4:
                    Instantiate(templates.leftClosedRoom, parentRoom.transform.position, Quaternion.identity);
                    break;
                case 0:
                    Debug.Log("From opened");
                    break;
                default:
                    break;

            }
            Destroy(parentRoom);
            Debug.Log("collision on - " + roomType);
        }

        // if (other.CompareTag("SpawnPoint")){
        //     //  Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
        //     //  Destroy(gameObject);
        //      Destroy(other.gameObject);
        // }
        // spawned = true;
    }
}
