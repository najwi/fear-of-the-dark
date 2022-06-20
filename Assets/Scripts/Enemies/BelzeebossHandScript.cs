using System.Collections;
using UnityEngine;

public class BelzeebossHandScript : MonoBehaviour, TakeBombDamageDecorator
{
    private Vector3 handPosition;
    private EnemyBossBeelzebossScript bossHead;
    private Animator anim;

    private void Start()
    {
        bossHead = GameObject.Find("Beelzeboss_head").GetComponent<EnemyBossBeelzebossScript>();
        anim = gameObject.GetComponent<Animator>();
    }

    public void MoveHandToSmash()
    {
        var pos = transform.position;
        handPosition = new Vector3(pos.x, pos.y, pos.z);
        transform.position = new Vector3(pos.x, 11.25f, pos.z);
    }

    public void ResetHandPosition()
    {
        transform.position = handPosition;
    }

    public void MoveHandToRest()
    {
        transform.position = new Vector3(transform.position.x, 12, transform.position.z);
    }

    public void CutsceneCancel()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        MoveHandToRest();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AllyProjectile"))
            bossHead.TakeDamage(collision.gameObject.GetComponent<Bullet>().dmg);
    }
    public bool TakeBombDamage(int damage)
    {
        bossHead.TakeDamage(damage);
        return true;
    }
}
