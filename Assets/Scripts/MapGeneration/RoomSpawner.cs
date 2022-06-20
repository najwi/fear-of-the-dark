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
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        parRoom = transform.parent.gameObject;
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Room")){
            Destroy(gameObject);
        }
        
        if (other.CompareTag("SpawnPoint")){
            
            GameObject parentRoom = transform.parent.gameObject;
            GameObject otherRoom = other.transform.parent.gameObject;

            Transform parentPosition = parentRoom.transform;
            Transform otherPosition = otherRoom.transform;

            
            int roomType = parentRoom.GetComponent<RoomGenerator>().roomType;
            int otherRoomType = otherRoom.GetComponent<RoomGenerator>().roomType;

            GameObject parentGeneratedFrom = parentRoom.GetComponent<RoomGenerator>().generatedFrom;
            GameObject otherGeneratedFrom = otherRoom.GetComponent<RoomGenerator>().generatedFrom;

            if(parentGeneratedFrom == otherGeneratedFrom){
                switch (roomType){
                case 1:
                    parentGeneratedFrom.GetComponent<RoomGenerator>().adjacentRooms.Add(Instantiate(templates.topClosedRoom, parentPosition.position, Quaternion.identity));
                    break;
                case 2:
                    parentGeneratedFrom.GetComponent<RoomGenerator>().adjacentRooms.Add(Instantiate(templates.rightClosedRoom, parentPosition.position, Quaternion.identity));
                    break;
                case 3:
                    parentGeneratedFrom.GetComponent<RoomGenerator>().adjacentRooms.Add(Instantiate(templates.bottomClosedRoom, parentPosition.position, Quaternion.identity));
                    break;
                case 4:
                    parentGeneratedFrom.GetComponent<RoomGenerator>().adjacentRooms.Add(Instantiate(templates.leftClosedRoom, parentPosition.position, Quaternion.identity));
                    break;
                case 0:
                    Debug.Log("From opened");
                    break;
                default:
                    Debug.Log("er");
                    break;
                }
                Destroy(gameObject);
                Destroy(other.gameObject);
                Destroy(otherRoom);
                Destroy(parentRoom);
                return;
            }

            Destroy(gameObject);
            Destroy(other.gameObject);
            Destroy(otherRoom);
            Destroy(parentRoom);

            switch (roomType){
                case 1:
                    parentGeneratedFrom.GetComponent<RoomGenerator>().adjacentRooms.Add(Instantiate(templates.topClosedRoom, parentPosition.position, Quaternion.identity));
                    break;
                case 2:
                    parentGeneratedFrom.GetComponent<RoomGenerator>().adjacentRooms.Add(Instantiate(templates.rightClosedRoom, parentPosition.position, Quaternion.identity));
                    break;
                case 3:
                    parentGeneratedFrom.GetComponent<RoomGenerator>().adjacentRooms.Add(Instantiate(templates.bottomClosedRoom, parentPosition.position, Quaternion.identity));
                    break;
                case 4:
                    parentGeneratedFrom.GetComponent<RoomGenerator>().adjacentRooms.Add(Instantiate(templates.leftClosedRoom, parentPosition.position, Quaternion.identity));
                    break;
                default:
                    break;
            }
            switch (otherRoomType){
                case 1:
                    otherGeneratedFrom.GetComponent<RoomGenerator>().adjacentRooms.Add(Instantiate(templates.topClosedRoom, otherPosition.position, Quaternion.identity));
                    break;
                case 2:
                    otherGeneratedFrom.GetComponent<RoomGenerator>().adjacentRooms.Add(Instantiate(templates.rightClosedRoom, otherPosition.position, Quaternion.identity));
                    break;
                case 3:
                    otherGeneratedFrom.GetComponent<RoomGenerator>().adjacentRooms.Add(Instantiate(templates.bottomClosedRoom, otherPosition.position, Quaternion.identity));
                    break;
                case 4:
                    otherGeneratedFrom.GetComponent<RoomGenerator>().adjacentRooms.Add(Instantiate(templates.leftClosedRoom, otherPosition.position, Quaternion.identity));
                    break;
                default:
                    break;
            }
        }
    }
}
