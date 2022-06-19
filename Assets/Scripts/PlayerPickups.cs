using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickups : MonoBehaviour
{
    private PlayerMovementScript script;
    public AudioSource smallPickup;

    private void Start(){
        script = gameObject.GetComponent<PlayerMovementScript>();
    }

    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("NotePickup")){
            script.NotePickup();
            smallPickup.Play();
            Destroy(col.gameObject);
        }else if(col.gameObject.CompareTag("BombPickup")){
            script.BombPickup();
            smallPickup.Play();
            Destroy(col.gameObject);
        }else if(col.gameObject.CompareTag("HealthPickup")){
            if(script.TryHeal()){
                smallPickup.Play();
                Destroy(col.gameObject);
            }
        }else if(col.gameObject.CompareTag("DamageUpPickup")){
            script.TryBuyDamageUp();
        }else if(col.gameObject.CompareTag("SpeedUpPickup")){
            script.TryBuySpeedUp();
        }else if(col.gameObject.CompareTag("HealthUpPickup")){
            script.TryBuyHealthUp();
        }
    }
}
