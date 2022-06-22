using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDesigner : MonoBehaviour
{
    public GameObject entryPoint;
    private int longestPath = -1;
    public static GameObject bossRoom;
    private bool started = false;
    private bool control = true;

    void Start()
    {
        entryPoint.GetComponent<RoomManagement>().CloseDoors();
    }

    void Update(){
        // Debug.Log();
        if(RoomGenerator.finished && control){
            started = true;
            control = false;
        }
        if(started){
            Invoke("Begin", 0.5f);
            started = false;
        }
    }

    public void Begin(){
        List<GameObject> adjacentRooms = entryPoint.GetComponent<RoomGenerator>().adjacentRooms;
        FindBossRoom(entryPoint, 0);
        while (bossRoom == null){
            
        }
        bossRoom.GetComponent<BossMaker>().checker.SetActive(true);
        MakeVariousDifficulty(entryPoint, 0);
        bossRoom.GetComponent<BossMaker>().MakeBossRoom();
        ResetRoomsSpawned();
    }

    void FindBossRoom(GameObject adjacentRoom, int depth){

        if (adjacentRoom == null)
            return;


        List<GameObject> adjacentRooms = adjacentRoom.GetComponent<RoomGenerator>().adjacentRooms;

        if (depth > longestPath){
            longestPath = depth;
            bossRoom = adjacentRoom;
        }

        foreach (var room in adjacentRooms)
        {
            FindBossRoom(room, depth + 1);
        }

    }

    void MakeVariousDifficulty(GameObject adjacentRoom, int depth){
        if (adjacentRoom == null){
            return;
        }
            

        List<GameObject> adjacentRooms = adjacentRoom.GetComponent<RoomGenerator>().adjacentRooms;

        if (depth != 0 && adjacentRoom.name != "Shop"){
            if (longestPath/depth > 3){
                adjacentRoom.GetComponent<BossMaker>().SetDifficulty(0);
            }else if(longestPath/depth >= 2 && longestPath/depth <= 3){
                adjacentRoom.GetComponent<BossMaker>().SetDifficulty(1);
            }else{
                adjacentRoom.GetComponent<BossMaker>().SetDifficulty(2);
            }
        }

        foreach (var room in adjacentRooms)
        {
            MakeVariousDifficulty(room, depth + 1);
        }
    }

    void ResetRoomsSpawned(){
        RoomGenerator.roomsSpawned = 0;
        RoomGenerator.finished = false;
        entryPoint.GetComponent<RoomManagement>().OpenDoors();
    }
}
