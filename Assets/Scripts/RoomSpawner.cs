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

        // if (other.CompareTag("SpawnPoint")){
        //     //  Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
        //     //  Destroy(gameObject);
        //      Destroy(other.gameObject);
        // }
        // spawned = true;
    }
}
