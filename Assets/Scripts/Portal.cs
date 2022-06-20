using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public Animator animator;

    public void TeleportToBoss(){
        SceneManager.LoadScene("BossScene");
    }

    private void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("Player")){
            animator.SetTrigger("Teleport");
            col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
