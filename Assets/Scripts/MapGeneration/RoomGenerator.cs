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

    public static bool finished;
    
    public GameObject[] spawnPoints;
    private RoomTemplates templates;
    public List<GameObject> roomsOnTop = new List<GameObject>();

    public static int minRoomQuantity = 10;
    public static int maxRoomQuantity = 40;
    public static int roomsSpawned = 0;

    public GameObject topSpawnPoint;
    public GameObject rightSpawnPoint;
    public GameObject bottomSpawnPoint;
    public GameObject leftSpawnPoint;
    private RoomDesigner roomDesigner;
    private RoomManager roomManager;

    private int rand;

    public List<GameObject> adjacentRooms = new List<GameObject>();
    public GameObject generatedFrom;
    public static GameObject bossRoom;

    void Awake()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        roomDesigner = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RoomDesigner>();
        roomManager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>();
    }

    void Start()
    {
        //templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        //roomDesigner = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RoomDesigner>();

        //CustomizeRoom();

        //if (roomsSpawned < maxRoomQuantity)
        //{
        //    Invoke("SpawnRoom", 0.3f);
        //}
        //else
        //{
        //    Invoke("SpawnClosedRoom", 0.1f);
        //    Invoke("FinishIt", 0.2f);
        //}
    }

    public void SetSpawnPoints()
    {
        switch (roomType)
        {
            case 1:
                if (bottomSpawnPoint != null)
                {
                    if (roomManager.RoomOnPosition(bottomSpawnPoint.transform))
                    {
                        Destroy(bottomSpawnPoint);
                    } else
                        bottomSpawnPoint.SetActive(true);
                }
                if (leftSpawnPoint != null)
                {
                    if (roomManager.RoomOnPosition(leftSpawnPoint.transform))
                    {
                        Destroy(leftSpawnPoint);
                    }
                    else
                        leftSpawnPoint.SetActive(true);
                }
                if (rightSpawnPoint != null)
                {
                    if (roomManager.RoomOnPosition(rightSpawnPoint.transform))
                    {
                        Destroy(rightSpawnPoint);
                    }
                    else
                        rightSpawnPoint.SetActive(true);
                }
                break;
            case 2:
                if (bottomSpawnPoint != null)
                {
                    if (roomManager.RoomOnPosition(bottomSpawnPoint.transform))
                    {
                        Destroy(bottomSpawnPoint);
                    }
                    else
                        bottomSpawnPoint.SetActive(true);
                }
                if (leftSpawnPoint != null)
                {
                    if (roomManager.RoomOnPosition(leftSpawnPoint.transform))
                    {
                        Destroy(leftSpawnPoint);
                    }
                    else
                        leftSpawnPoint.SetActive(true);
                }
                if (topSpawnPoint != null)
                {
                    if (roomManager.RoomOnPosition(topSpawnPoint.transform))
                    {
                        Destroy(topSpawnPoint);
                    }
                    else
                        topSpawnPoint.SetActive(true);
                }
                break;
            case 3:
                if (leftSpawnPoint != null)
                {
                    if (roomManager.RoomOnPosition(leftSpawnPoint.transform))
                    {
                        Destroy(leftSpawnPoint);
                    }
                    else
                        leftSpawnPoint.SetActive(true);
                }
                if (topSpawnPoint != null)
                {
                    if (roomManager.RoomOnPosition(topSpawnPoint.transform))
                    {
                        Destroy(topSpawnPoint);
                    }
                    else
                        topSpawnPoint.SetActive(true);
                }
                if (rightSpawnPoint != null)
                {
                    if (roomManager.RoomOnPosition(rightSpawnPoint.transform))
                    {
                        Destroy(rightSpawnPoint);
                    }
                    else
                        rightSpawnPoint.SetActive(true);
                }
                break;
            case 4:
                if (bottomSpawnPoint != null)
                {
                    if (roomManager.RoomOnPosition(bottomSpawnPoint.transform))
                    {
                        Destroy(bottomSpawnPoint);
                    }
                    else
                        bottomSpawnPoint.SetActive(true);
                }
                if (topSpawnPoint != null)
                {
                    if (roomManager.RoomOnPosition(topSpawnPoint.transform))
                    {
                        Destroy(topSpawnPoint);
                    }
                    else
                        topSpawnPoint.SetActive(true);
                }
                if (rightSpawnPoint != null)
                {
                    if (roomManager.RoomOnPosition(rightSpawnPoint.transform))
                    {
                        Destroy(rightSpawnPoint);
                    }
                    else
                        rightSpawnPoint.SetActive(true);
                }
                break;
        }
    }

    void FinishIt(){
        finished = true;
    }

    public List<GameObject> SpawnRoom(){
        var rooms = new List<GameObject>();
        for (int i=0; i < 4; i++){
            if (i == 0){
                if (topSpawnPoint != null){
                    if (topSpawnPoint.activeSelf){
                        if (roomManager.RoomOnPosition(topSpawnPoint.transform))
                        {
                            continue;
                        }
                        rand = Random.Range(0, templates.topRooms.Length);
                        GameObject topRoom = Instantiate(templates.topRooms[rand], topSpawnPoint.transform.position, Quaternion.identity);
                        RoomGenerator roomGenerator = topRoom.GetComponent<RoomGenerator>();
                        roomGenerator.roomType = 3;
                        roomGenerator.generatedFrom = gameObject;
                        roomGenerator.SetSpawnPoints();
                        roomManager.roomPositions.Add(((int)topSpawnPoint.transform.position.x, (int)topSpawnPoint.transform.position.y));
                        adjacentRooms.Add(topRoom);
                        Destroy(topSpawnPoint);
                        roomsSpawned++;
                        rooms.Add(topRoom);
                    }
                }
            }else if (i == 1){
                if (rightSpawnPoint != null){
                    if (rightSpawnPoint.activeSelf){
                        if (roomManager.RoomOnPosition(rightSpawnPoint.transform))
                        {
                            continue;
                        }
                        rand = Random.Range(0, templates.rightRooms.Length);
                        GameObject rightRoom = Instantiate(templates.rightRooms[rand], rightSpawnPoint.transform.position, Quaternion.identity);
                        RoomGenerator roomGenerator = rightRoom.GetComponent<RoomGenerator>();
                        roomGenerator.roomType = 4;
                        roomGenerator.generatedFrom = gameObject;
                        roomGenerator.SetSpawnPoints();
                        roomManager.roomPositions.Add(((int)rightSpawnPoint.transform.position.x, (int)rightSpawnPoint.transform.position.y));
                        adjacentRooms.Add(rightRoom);
                        Destroy(rightSpawnPoint);
                        roomsSpawned++;
                        rooms.Add(rightRoom);
                    }
                }
            }
            else if (i == 2){
                if (bottomSpawnPoint != null){
                    if (bottomSpawnPoint.activeSelf){
                        if (roomManager.RoomOnPosition(bottomSpawnPoint.transform))
                        {
                            continue;
                        }
                        rand = Random.Range(0, templates.bottomRooms.Length);
                        GameObject bottomRoom = Instantiate(templates.bottomRooms[rand], bottomSpawnPoint.transform.position, Quaternion.identity);
                        RoomGenerator roomGenerator = bottomRoom.GetComponent<RoomGenerator>();
                        roomGenerator.roomType = 1;
                        roomGenerator.generatedFrom = gameObject;
                        roomGenerator.SetSpawnPoints();
                        roomManager.roomPositions.Add(((int)bottomSpawnPoint.transform.position.x, (int)bottomSpawnPoint.transform.position.y));
                        adjacentRooms.Add(bottomRoom);
                        Destroy(bottomSpawnPoint);
                        roomsSpawned++;
                        rooms.Add(bottomRoom);
                    }
                }
            }
            else if (i == 3){
                if (leftSpawnPoint != null){
                    if (leftSpawnPoint.activeSelf){
                        if (roomManager.RoomOnPosition(leftSpawnPoint.transform))
                        {
                            continue;
                        }
                        rand = Random.Range(0, templates.leftRooms.Length);
                        GameObject leftRoom = Instantiate(templates.leftRooms[rand], leftSpawnPoint.transform.position, Quaternion.identity);
                        RoomGenerator roomGenerator = leftRoom.GetComponent<RoomGenerator>();
                        roomGenerator.roomType = 2;
                        roomGenerator.generatedFrom = gameObject;
                        roomGenerator.SetSpawnPoints();
                        roomManager.roomPositions.Add(((int)leftSpawnPoint.transform.position.x, (int)leftSpawnPoint.transform.position.y));
                        adjacentRooms.Add(leftRoom);
                        Destroy(leftSpawnPoint);
                        roomsSpawned++;
                        rooms.Add(leftRoom);
                    }
                }
            }
        }
        return rooms;
    }

    public new List<GameObject> SpawnClosedRoom(){
        var rooms = new List<GameObject>();
        for (int i=0; i < 4; i++){
            if (i == 0){
                if (topSpawnPoint != null){
                    if (topSpawnPoint.activeSelf){
                        if (roomManager.RoomOnPosition(topSpawnPoint.transform))
                        {
                            continue;
                        }
                        GameObject topRoom = Instantiate(templates.bottomClosedRoom, topSpawnPoint.transform.position, Quaternion.identity);
                        RoomGenerator roomGenerator = topRoom.GetComponent<RoomGenerator>();
                        roomGenerator.SetSpawnPoints();
                        roomManager.roomPositions.Add(((int)topSpawnPoint.transform.position.x, (int)topSpawnPoint.transform.position.y));
                        adjacentRooms.Add(topRoom);
                        topRoom.GetComponent<RoomGenerator>().generatedFrom = gameObject;
                        Destroy(topSpawnPoint);
                        roomsSpawned++;
                        rooms.Add(topRoom);
                    }
                }
            }else if (i == 1){
                if (rightSpawnPoint != null){
                    if (rightSpawnPoint.activeSelf){
                        if (roomManager.RoomOnPosition(rightSpawnPoint.transform))
                        {
                            continue;
                        }
                        GameObject leftRoom = Instantiate(templates.leftClosedRoom, rightSpawnPoint.transform.position, Quaternion.identity);
                        RoomGenerator roomGenerator = leftRoom.GetComponent<RoomGenerator>();
                        roomGenerator.SetSpawnPoints();
                        roomManager.roomPositions.Add(((int)rightSpawnPoint.transform.position.x, (int)rightSpawnPoint.transform.position.y));
                        adjacentRooms.Add(leftRoom);
                        leftRoom.GetComponent<RoomGenerator>().generatedFrom = gameObject;
                        Destroy(rightSpawnPoint);
                        roomsSpawned++;
                        rooms.Add(leftRoom);
                    }
                }
            }
            else if (i == 2){
                if (bottomSpawnPoint != null){
                    if (bottomSpawnPoint.activeSelf){
                        if (roomManager.RoomOnPosition(bottomSpawnPoint.transform))
                        {
                            continue;
                        }
                        GameObject bottomRoom = Instantiate(templates.topClosedRoom, bottomSpawnPoint.transform.position, Quaternion.identity);
                        RoomGenerator roomGenerator = bottomRoom.GetComponent<RoomGenerator>();
                        roomGenerator.SetSpawnPoints();
                        roomManager.roomPositions.Add(((int)bottomSpawnPoint.transform.position.x, (int)bottomSpawnPoint.transform.position.y));
                        adjacentRooms.Add(bottomRoom);
                        bottomRoom.GetComponent<RoomGenerator>().generatedFrom = gameObject;
                        Destroy(bottomSpawnPoint);
                        roomsSpawned++;
                        rooms.Add(bottomRoom);
                    }
                }
            }
            else if (i == 3){
                if (leftSpawnPoint != null){
                    if (leftSpawnPoint.activeSelf){
                        if (roomManager.RoomOnPosition(leftSpawnPoint.transform))
                        {
                            continue;
                        }
                        GameObject rightRoom = Instantiate(templates.rightClosedRoom, leftSpawnPoint.transform.position, Quaternion.identity);
                        RoomGenerator roomGenerator = rightRoom.GetComponent<RoomGenerator>();
                        roomGenerator.SetSpawnPoints();
                        roomManager.roomPositions.Add(((int)leftSpawnPoint.transform.position.x, (int)leftSpawnPoint.transform.position.y));
                        adjacentRooms.Add(rightRoom);
                        rightRoom.GetComponent<RoomGenerator>().generatedFrom = gameObject;
                        Destroy(leftSpawnPoint);
                        roomsSpawned++;
                        rooms.Add(rightRoom);
                    }
                }
            }
        }
        return rooms;
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
