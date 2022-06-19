using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject col;
    public Animator animator;
    public float timer = 2;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", timer);
    }

    private void Explode(){
        col.SetActive(true);
        animator.SetTrigger("Explosion");
    }

    public void DestroyBomb(){
        Destroy(gameObject);
    }

}
