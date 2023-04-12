using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerationManager : MonoBehaviour
{
    public RoomManager roomManager;
    public RoomDesigner roomDesigner;
    private int roomsCount = 3;
    public static int maxRoomQuantity = 40;

    void Start()
    {
        Invoke("GenerateMap", 0.5f);
    }

    void GenerateMap()
    {
        roomManager.GenerateRooms();
        roomDesigner.FixBegin();
    }
}
