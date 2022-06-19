using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject col;
    public Animator animator;
    public float timer = 2;
    public AudioSource audioExplosion;
    public AudioSource audioTick;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", timer);
    }

    private void Explode(){
        audioTick.Stop();
        col.SetActive(true);
        Invoke("DisableCollider", 0.1f);
        audioExplosion.Play();
        animator.SetTrigger("Explosion");
    }

    private void DisableCollider(){
        col.SetActive(false);
    }

    public void DestroyBomb(){
        Destroy(gameObject);
    }

}
