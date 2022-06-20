using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject bottomClosedRoom;
    public GameObject topClosedRoom;
    public GameObject leftClosedRoom;
    public GameObject rightClosedRoom;

    public GameObject closedRoom;
    public GameObject openedRoom;

    public Sprite[] floorSprites;
    public Sprite[] wallSprites;
    public Sprite[] mediumFloorSprites;
    public Sprite[] mediumWallSprites;
    public Sprite[] hardFloorSprites;
    public Sprite[] hardWallSprites;
    public Sprite[] bossFloorSprites;
    public Sprite[] bossWallSprites;

    public GameObject[] obstacleTemplates;

    public GameObject[] easyObstacleTemplates;
    public GameObject[] mediumObstacleTemplates;
    public GameObject[] hardObstacleTemplates;

    public Sprite closedDoor;
    public Sprite openedDoor;

    // public Sprite[] shopFloorSprites;
    public Sprite[] shopWallSprites;
}
