using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private int roomsCount = 3;
    public static int maxRoomQuantity = 20;
    public GameObject[] initialRooms;
    //public LinkedList<RoomGenerator> rooms = new LinkedList<RoomGenerator>();
    public Queue<GameObject> rooms = new Queue<GameObject>();
    public HashSet<(int, int)> roomPositions = new HashSet<(int, int)>();

    void Start()
    {
        //Invoke("GenerateRooms", 0.5f);
        roomPositions.Add((0,0));
        roomPositions.Add((18, 0));
        roomPositions.Add((-18, 0));
    }

    public bool RoomOnPosition(Transform transform)
    {
        return roomPositions.Contains(((int)transform.position.x, (int)transform.position.y));
    }

    public void GenerateRooms()
    {
        foreach (var room in initialRooms)
        {
            rooms.Enqueue(room);
        }
        while (rooms.Count > 0)
        {
            var room = rooms.Dequeue();
            var generator = room.GetComponent<RoomGenerator>();
            List<GameObject> generatedRooms;
            if (roomsCount < maxRoomQuantity)
            {
                generatedRooms = generator.SpawnRoom();
            }
            else
            {
                generatedRooms = generator.SpawnClosedRoom();

            }

            foreach (var generatedRoom in generatedRooms)
            {
                roomsCount++;
                rooms.Enqueue(generatedRoom);
            }
        }
    }
}
