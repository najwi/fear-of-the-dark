using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraController : MonoBehaviour
{
    public float cameraSpeed;
    
    
    private float currentPosX;
    private float minPosX = -3.5f;
    private float maxPosX = 3.5f;

    private float currentPosY;
    private const float PosYOffset = 2f;
    private float currentPosYOffset = PosYOffset;
    private Vector3 velocity = Vector3.zero;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        currentPosX = player.transform.position.x;
        if (currentPosX < minPosX)
            currentPosX = minPosX;
        else if (currentPosX > maxPosX)
            currentPosX = maxPosX;
        currentPosY = player.transform.position.y + currentPosYOffset;
        transform.position = Vector3.SmoothDamp(transform.position, 
            new Vector3(currentPosX, currentPosY, transform.position.z), ref velocity, cameraSpeed);
    }

    public void ResetCameraOffset()
    {
        currentPosYOffset = PosYOffset;
    }

    public void SetCameraYOffset(float newOffsetY)
    {
        currentPosYOffset = newOffsetY;
    }
}
