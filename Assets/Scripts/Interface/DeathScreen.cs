using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
