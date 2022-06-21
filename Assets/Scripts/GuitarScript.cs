using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuitarScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Invoke("Credits", 1);
        }

    }

    private void Credits(){
        SceneManager.LoadScene("CreditsScene");
    }
}
