using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

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

    public static bool finished = false;
    
    public GameObject[] spawnPoints;
    private RoomTemplates templates;
    public List<GameObject> roomsOnTop = new List<GameObject>();

    public static int minRoomQuantity = 10;
    public static int maxRoomQuantity = 40;
    private static int roomsSpawned = 0;

    public GameObject topSpawnPoint;
    public GameObject rightSpawnPoint;
    public GameObject bottomSpawnPoint;
    public GameObject leftSpawnPoint;
    private RoomDesigner roomDesigner;

    private int rand;

    public List<GameObject> adjacentRooms = new List<GameObject>();
    public GameObject generatedFrom;
    public static GameObject bossRoom;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        roomDesigner = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RoomDesigner>();

        switch (roomType){
            case 1:
                if (bottomSpawnPoint != null){
                    bottomSpawnPoint.SetActive(true);
                }
                if (leftSpawnPoint != null){
                    leftSpawnPoint.SetActive(true);
                }
                if (rightSpawnPoint != null){
                    rightSpawnPoint.SetActive(true);
                }
                break;
            case 2:
                if (bottomSpawnPoint != null){
                    bottomSpawnPoint.SetActive(true);
                }
                if (leftSpawnPoint != null){
                    leftSpawnPoint.SetActive(true);
                }
                if (topSpawnPoint != null){
                    topSpawnPoint.SetActive(true);
                }
                break;
            case 3:
                if (topSpawnPoint != null){
                    topSpawnPoint.SetActive(true);
                }
                if (leftSpawnPoint != null){
                    leftSpawnPoint.SetActive(true);
                }
                if (rightSpawnPoint != null){
                    rightSpawnPoint.SetActive(true);
                }
                break;
            case 4:
                if (bottomSpawnPoint != null){
                    bottomSpawnPoint.SetActive(true);
                }
                if (topSpawnPoint != null){
                    topSpawnPoint.SetActive(true);
                }
                if (rightSpawnPoint != null){
                    rightSpawnPoint.SetActive(true);
                }
                break;
        }

        CustomizeRoom();

        if (roomsSpawned < maxRoomQuantity){
            Invoke("SpawnRoom", 0.4f);
        }
        else{
            Invoke("SpawnClosedRoom", 0.1f);
            finished = true;
        }
    }

    // void FindBossRoom(){
    //     roomDesigner.Begin();
    // }

    void SpawnRoom(){
        for (int i=0; i < 4; i++){
            if (i == 0){
                if (topSpawnPoint != null){
                    if (topSpawnPoint.activeSelf){
                        rand = Random.Range(0, templates.topRooms.Length);
                        GameObject topRoom = Instantiate(templates.topRooms[rand], topSpawnPoint.transform.position, Quaternion.identity);
                        topRoom.GetComponent<RoomGenerator>().roomType = 3;
                        topRoom.GetComponent<RoomGenerator>().generatedFrom = gameObject;
                        adjacentRooms.Add(topRoom);
                        // Instantiate(templates.topRooms[rand], topSpawnPoint.transform.position, Quaternion.identity);
                        Destroy(topSpawnPoint);
                        roomsSpawned++;
                    }
                }
            }else if (i == 1){
                if (rightSpawnPoint != null){
                    if (rightSpawnPoint.activeSelf){
                        rand = Random.Range(0, templates.rightRooms.Length);
                        GameObject rightRoom = Instantiate(templates.rightRooms[rand], rightSpawnPoint.transform.position, Quaternion.identity);
                        rightRoom.GetComponent<RoomGenerator>().roomType = 4;
                        rightRoom.GetComponent<RoomGenerator>().generatedFrom = gameObject;
                        adjacentRooms.Add(rightRoom);
                        Destroy(rightSpawnPoint);
                        roomsSpawned++;
                    }
                }
            }
            else if (i == 2){
                if (bottomSpawnPoint != null){
                    if (bottomSpawnPoint.activeSelf){
                        rand = Random.Range(0, templates.bottomRooms.Length);
                        GameObject bottomRoom = Instantiate(templates.bottomRooms[rand], bottomSpawnPoint.transform.position, Quaternion.identity);
                        bottomRoom.GetComponent<RoomGenerator>().roomType = 1;
                        bottomRoom.GetComponent<RoomGenerator>().generatedFrom = gameObject;
                        adjacentRooms.Add(bottomRoom);
                        Destroy(bottomSpawnPoint);
                        roomsSpawned++;
                    }
                }
            }
            else if (i == 3){
                if (leftSpawnPoint != null){
                    if (leftSpawnPoint.activeSelf){
                        rand = Random.Range(0, templates.leftRooms.Length);
                        GameObject leftRoom = Instantiate(templates.leftRooms[rand], leftSpawnPoint.transform.position, Quaternion.identity);
                        leftRoom.GetComponent<RoomGenerator>().roomType = 2;
                        leftRoom.GetComponent<RoomGenerator>().generatedFrom = gameObject;
                        adjacentRooms.Add(leftRoom);
                        Destroy(leftSpawnPoint);
                        roomsSpawned++;
                    }
                }
            }
        }
    }

    void SpawnClosedRoom(){
        for (int i=0; i < 4; i++){
            if (i == 0){
                if (topSpawnPoint != null){
                    if (topSpawnPoint.activeSelf){
                        adjacentRooms.Add(Instantiate(templates.bottomClosedRoom, topSpawnPoint.transform.position, Quaternion.identity));
                        Destroy(topSpawnPoint);
                        roomsSpawned++;
                    }
                }
            }else if (i == 1){
                if (rightSpawnPoint != null){
                    if (rightSpawnPoint.activeSelf){
                        adjacentRooms.Add(Instantiate(templates.leftClosedRoom, rightSpawnPoint.transform.position, Quaternion.identity));
                        Destroy(rightSpawnPoint);
                        roomsSpawned++;
                    }
                }
            }
            else if (i == 2){
                if (bottomSpawnPoint != null){
                    if (bottomSpawnPoint.activeSelf){
                        adjacentRooms.Add(Instantiate(templates.topClosedRoom, bottomSpawnPoint.transform.position, Quaternion.identity));
                        Destroy(bottomSpawnPoint);
                        roomsSpawned++;
                    }
                }
            }
            else if (i == 3){
                if (leftSpawnPoint != null){
                    if (leftSpawnPoint.activeSelf){
                        adjacentRooms.Add(Instantiate(templates.rightClosedRoom, leftSpawnPoint.transform.position, Quaternion.identity));
                        Destroy(leftSpawnPoint);
                        roomsSpawned++;
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        // if (other.CompareTag("Room")){
        //     // roomsOnTop.Add(Instantiate(templates.closedRoom, other.gameObject.transform.position, Quaternion.identity));
        //     adjacentRooms.Remove(other.gameObject);
        //     Destroy(other.gameObject);
        //     Destroy(gameObject);
        // }
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
        
        foreach (Transform floorChild in floor.gameObject.transform){
            SpriteRenderer floorChildSpriteRenderer = floorChild.gameObject.GetComponent<SpriteRenderer>();
            int random = Random.Range(0, templates.floorSprites.Length);
            floorChildSpriteRenderer.sprite = templates.floorSprites[random];
        }
        if (gameObject.name != "Shop"){
            foreach (Transform wallChild in walls.gameObject.transform){
                SpriteRenderer wallChildSpriteRenderer = wallChild.gameObject.GetComponent<SpriteRenderer>();
                int random = Random.Range(0, templates.wallSprites.Length);
                wallChildSpriteRenderer.sprite = templates.wallSprites[random];
            }
        }else{
            foreach (Transform wallChild in walls.gameObject.transform){
                SpriteRenderer wallChildSpriteRenderer = wallChild.gameObject.GetComponent<SpriteRenderer>();
                int random = Random.Range(0, templates.shopWallSprites.Length);
                wallChildSpriteRenderer.sprite = templates.shopWallSprites[random];
            }
        }
        // int r = Random.Range(0, templates.easyObstacleTemplates.Length);
        // if (gameObject.name != "Opened" && gameObject.name != "Shop"){
        //     Instantiate(templates.easyObstacleTemplates[r], transform.position, Quaternion.identity).transform.parent = gameObject.transform;
        // }
    }
}
