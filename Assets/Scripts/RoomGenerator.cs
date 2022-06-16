using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public int roomType;
    // 0 - opened
    // 1 - top door
    // 2 - right door
    // 3 - bottom door
    // 4 - left door

    public int[] doors;
    public bool topDoor;
    public bool rightDoor;
    public bool leftDoor;
    public bool bottomDoor;
    
    public GameObject[] spawnPoints;
    private RoomTemplates templates;

    public static int minRoomQuantity = 10;
    public static int maxRoomQuantity = 40;
    private static int roomsSpawned = 0;

    public GameObject topSpawnPoint;
    public GameObject rightSpawnPoint;
    public GameObject bottomSpawnPoint;
    public GameObject leftSpawnPoint;

    private int rand;
    // public 

    public List<GameObject> adjacentRooms = new List<GameObject>();
    public static GameObject bossRoom;

    // public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

        CustomizeRoom();
        
        // Invoke("SpawnRoom", 0.1f);
        if (roomsSpawned < maxRoomQuantity){
            // SpawnRoom();
            Invoke("SpawnRoom", 0.5f);
        }
        else{
            // Debug.Log("closedroom");
            // SpawnClosedRoom();
            Invoke("SpawnClosedRoom", 0.5f);
        }
    }

    void SpawnRoom(){
        // int i = 0;
        for (int i=0; i < 4; i++){
            if (i == 0){
                if (topSpawnPoint != null){
                    rand = Random.Range(0, templates.topRooms.Length);
                    adjacentRooms.Add(Instantiate(templates.topRooms[rand], topSpawnPoint.transform.position, Quaternion.identity));
                    // Instantiate(templates.topRooms[rand], topSpawnPoint.transform.position, Quaternion.identity);
                    Destroy(topSpawnPoint);
                    roomsSpawned++;
                }
            }else if (i == 1){
                if (rightSpawnPoint != null){
                    rand = Random.Range(0, templates.rightRooms.Length);
                    adjacentRooms.Add(Instantiate(templates.rightRooms[rand], rightSpawnPoint.transform.position, Quaternion.identity));
                    Destroy(rightSpawnPoint);
                    roomsSpawned++;
                }
            }
            else if (i == 2){
                if (bottomSpawnPoint != null){
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    adjacentRooms.Add(Instantiate(templates.bottomRooms[rand], bottomSpawnPoint.transform.position, Quaternion.identity));
                    Destroy(bottomSpawnPoint);
                    roomsSpawned++;
                }
            }
            else if (i == 3){
                if (leftSpawnPoint != null){
                    rand = Random.Range(0, templates.leftRooms.Length);
                    adjacentRooms.Add(Instantiate(templates.leftRooms[rand], leftSpawnPoint.transform.position, Quaternion.identity));
                    Destroy(leftSpawnPoint);
                    roomsSpawned++;
                }
            }
        }
    }

    void SpawnClosedRoom(){
        for (int i=0; i < 4; i++){
            if (i == 0){
                if (topSpawnPoint != null){
                    adjacentRooms.Add(Instantiate(templates.bottomClosedRoom, topSpawnPoint.transform.position, Quaternion.identity));
                    Destroy(topSpawnPoint);
                    roomsSpawned++;
                }
            }else if (i == 1){
                if (rightSpawnPoint != null){
                    adjacentRooms.Add(Instantiate(templates.leftClosedRoom, rightSpawnPoint.transform.position, Quaternion.identity));
                    Destroy(rightSpawnPoint);
                    roomsSpawned++;
                }
            }
            else if (i == 2){
                if (bottomSpawnPoint != null){
                    adjacentRooms.Add(Instantiate(templates.topClosedRoom, bottomSpawnPoint.transform.position, Quaternion.identity));
                    Destroy(bottomSpawnPoint);
                    roomsSpawned++;
                }
            }
            else if (i == 3){
                if (leftSpawnPoint != null){
                    adjacentRooms.Add(Instantiate(templates.rightClosedRoom, leftSpawnPoint.transform.position, Quaternion.identity));
                    Destroy(leftSpawnPoint);
                    roomsSpawned++;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Room")){
            Instantiate(templates.closedRoom, other.gameObject.transform.position, Quaternion.identity);
            adjacentRooms.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
        // if (topSpawnPoint != null){
        //     Destroy(topSpawnPoint);
        // }
        // if (bottomSpawnPoint != null){
        //     Destroy(bottomSpawnPoint);
        // }
        // if (rightSpawnPoint != null){
        //     Destroy(rightSpawnPoint);
        // }
        // if (leftSpawnPoint != null){
        //     Destroy(leftSpawnPoint);
        // }
    }

    void CustomizeRoom() {
        Transform walls = gameObject.transform.GetChild(0);
        Transform floor = gameObject.transform.GetChild(1);
        foreach (Transform wallChild in walls.gameObject.transform){
            SpriteRenderer wallChildSpriteRenderer = wallChild.gameObject.GetComponent<SpriteRenderer>();
            int random = Random.Range(0, templates.wallSprites.Length);
            wallChildSpriteRenderer.sprite = templates.wallSprites[random];
            // Something(child.gameObject);
        }
        foreach (Transform floorChild in floor.gameObject.transform){
            SpriteRenderer floorChildSpriteRenderer = floorChild.gameObject.GetComponent<SpriteRenderer>();
            int random = Random.Range(0, templates.floorSprites.Length);
            floorChildSpriteRenderer.sprite = templates.floorSprites[random];
        }
        int r = Random.Range(0, templates.obstacleTemplates.Length);
        Instantiate(templates.obstacleTemplates[r], transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
