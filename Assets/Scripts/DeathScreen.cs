using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space") || Input.GetKeyDown("escape")){
            SceneManager.LoadScene("MenuScene");
        }
    }
}
