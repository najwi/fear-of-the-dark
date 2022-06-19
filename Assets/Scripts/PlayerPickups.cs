using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickups : MonoBehaviour
{
    private PlayerMovementScript script;

    private void Start(){
        script = gameObject.GetComponent<PlayerMovementScript>();
    }
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("NotePickup")){
            script.NotePickup();
            Destroy(col.gameObject);
        }else if(col.gameObject.CompareTag("BombPickup")){
            script.BombPickup();
            Destroy(col.gameObject);
        }else if(col.gameObject.CompareTag("HealthPickup")){
            if(script.TryHeal())
                Destroy(col.gameObject);
        }else if(col.gameObject.CompareTag("DamageUpPickup")){
            script.TryBuyDamageUp();
        }else if(col.gameObject.CompareTag("SpeedUpPickup")){
            script.TryBuySpeedUp();
        }else if(col.gameObject.CompareTag("HealthUpPickup")){
            script.TryBuyHealthUp();
        }
    }
}
