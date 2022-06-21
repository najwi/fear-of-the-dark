using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoysticksManager : MonoBehaviour
{
    public Joystick movementJoystick;
    public Joystick attackJoystick;
    public Button bombButton;

    // Start is called before the first frame update
    void Start()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            movementJoystick.gameObject.SetActive(true);
            attackJoystick.gameObject.SetActive(true);
            bombButton.gameObject.SetActive(true);
        }
    }
}
