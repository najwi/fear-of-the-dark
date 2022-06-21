using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoysticksManager : MonoBehaviour
{
    public Joystick movementJoystick;
    public Joystick attackJoystick;
    // Start is called before the first frame update
    void Start()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            movementJoystick.gameObject.SetActive(true);
            attackJoystick.gameObject.SetActive(true);
        }   
    }
}
