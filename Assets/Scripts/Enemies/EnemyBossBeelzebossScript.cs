using System.Collections;
using UnityEngine;

public class EnemyBossBeelzebossScript : MonoBehaviour
{
    public GameObject bossCamera;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject fireball;
    public float attacksDelayTime = 5;
    public int fireballsCount = 10;
    public int fireballAttackDamage = 2;
    public float fireballSpread = 2.5f;
    public float fireballDelay = 0.3f;

    private GameObject player;
    private PlayerMovementScript playerScript;
    private Animator anim;
    private SpriteRenderer sprite;
    private bool attacking = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerMovementScript>();
        anim = gameObject.GetComponent<Animator>();
        for (int i = 0; i < 100; i++)
            Destroy(fireball.GetComponent<FireballScript>());
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (attacking)
        {
            ThrowFireballs();
            StartCoroutine(PauseBetweenAttacks(attacksDelayTime));
        }
    }

    public void ShowHands()
    {
        leftHand.GetComponent<SpriteRenderer>().enabled = true;
        rightHand.GetComponent<SpriteRenderer>().enabled = true;
        leftHand.GetComponent<Animator>().SetTrigger("smash");
        rightHand.GetComponent<Animator>().SetTrigger("smash");
    }

    public void MoveCameraOnBoss()
    {
        bossCamera.GetComponent<BossCameraController>().SetCinematicPosY(15);
    }

    public void ResetCamera()
    {
        bossCamera.GetComponent<BossCameraController>().Unlock();
        attacking = true;
    }

    public void CutsceneCancel()
    {
        transform.position = new Vector3(transform.position.x, 16, transform.position.z);
        StartCoroutine(PauseBetweenAttacks(1));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AllyProjectile"))
            StartCoroutine(StrobeColorHelper(0, 5, sprite, Color.white, new Color(1, 1, 1, 0.5f)));
    }

    private IEnumerator StrobeColorHelper(int _i, int _stopAt, SpriteRenderer _mySprite, Color _color, Color _toStrobe)
    {
        if (_i <= _stopAt)
        {
            if (_i % 2 == 0)
                _mySprite.color = _toStrobe;
            else
                _mySprite.color = _color;

            yield return new WaitForSeconds(.1f);
            StartCoroutine(StrobeColorHelper((_i + 1), _stopAt, _mySprite, _color, _toStrobe));
        }
    }

    private IEnumerator PauseBetweenAttacks(float time)
    {
        attacking = false;
        yield return new WaitForSeconds(time);
        attacking = true;
    }

    private void ThrowFireballs()
    {
        anim.SetTrigger("fire");
        float offsetMultiplier = 1;
        Vector2 target = new Vector2(player.transform.position.x + 6, player.transform.position.y);
        if (Random.Range(0.0f, 1.0f) < 0.5f)
        {
            offsetMultiplier = -1;
            target = new Vector2(player.transform.position.x - 6, player.transform.position.y);
        }

        float offsetX = 0;
        for (int i = 0; i < fireballsCount; i++)
        {
            StartCoroutine(SpawnFireballWithDelay((i + 1) * fireballDelay, offsetX, i == fireballsCount - 1, target));
            offsetX += offsetMultiplier * fireballSpread;
        }
    }

    private IEnumerator SpawnFireballWithDelay(float delay, float offsetX, bool last, Vector2 target)
    {
        yield return new WaitForSeconds(delay);
        var ball = Instantiate(fireball);
        ball.transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);

        Vector2 direction = target - (Vector2)transform.position;
        direction = new Vector2(direction.x - offsetX, direction.y);
        direction.Normalize();

        FireballScript script = ball.AddComponent<FireballScript>();
        script.damage = fireballAttackDamage;
        script.direction = direction;
        script.player = playerScript;

        if (last)
            anim.SetTrigger("idle");
    }
}
