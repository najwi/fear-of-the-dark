using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDesigner : MonoBehaviour
{
    public GameObject entryPoint;
    private int longestPath = -1;
    public static GameObject bossRoom;

    void Start()
    {
        Invoke("Begin", 10.5f);
    }

    void Begin(){
        List<GameObject> adjacentRooms = entryPoint.GetComponent<RoomGenerator>().adjacentRooms;
        FindBossRoom(entryPoint, 0);
        while (bossRoom == null){
            
        }
        bossRoom.GetComponent<BossMaker>().checker.SetActive(true);
        bossRoom.GetComponent<BossMaker>().MakeBossRoom();
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
}
