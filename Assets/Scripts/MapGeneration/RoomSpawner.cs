using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class RoomSpawner : MonoBehaviour
{
    private RoomTemplates templates;
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
        }
    }
}
