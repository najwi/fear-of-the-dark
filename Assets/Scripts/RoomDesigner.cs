using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDesigner : MonoBehaviour
{
    public GameObject entryPoint;
    private int longestPath = -1;
    public static GameObject bossRoom;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("Begin", 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Begin(){
        List<GameObject> adjacentRooms = entryPoint.GetComponent<RoomGenerator>().adjacentRooms;
        FindBossRoom(entryPoint, 0);
        while (bossRoom == null){
            Debug.Log("Waiting");
        }
        Debug.Log("Finished");
        if (bossRoom == null)
            Debug.Log("null");

        if (bossRoom.GetComponent<BossMaker>() == null){
            Debug.Log("Script null");
        }

        if (bossRoom.GetComponent<BossMaker>().checker == null){
            Debug.Log("Checker null");
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
        // if (adjacentRooms == null){
        //     if (depth > longestPath){
        //         longestPath = depth;
        //         bossRoom = adjacentRoom;
        //         Debug.Log("Found2");
        //     }
        // }

        foreach (var room in adjacentRooms)
        {
            FindBossRoom(room, depth + 1);
        }

    }
}
