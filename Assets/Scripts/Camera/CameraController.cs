using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed;
    private float currentPosX;
    private float currentPosY;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, currentPosY, transform.position.z), 
        ref velocity, cameraSpeed);
    }

    public void Move(Transform destination){
        currentPosX = destination.position.x;
        currentPosY = destination.position.y;
    }
}
