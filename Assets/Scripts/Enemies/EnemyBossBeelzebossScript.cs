using System.Collections;
using UnityEngine;

public class EnemyBossBeelzebossScript : MonoBehaviour, TakeBombDamageDecorator
{
    #region variables
    public GameObject bossCamera;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject fireball;
    public BossHealthBarScript healthBar;
    public int health = 300;
    public float attacksDelayTime = 5;
    public int fireballsCount = 10;
    public int fireballAttackDamage = 2;
    public float fireballSpread = 2.5f;
    public float fireballDelay = 0.3f;

    private GameObject player;
    private PlayerMovementScript playerScript;
    private Animator anim;
    private SpriteRenderer headSprite;
    private SpriteRenderer handLSprite;
    private SpriteRenderer handRSprite;

    private bool canAttack = false;
    private GameObject[] fireballs;
    #endregion variables

    private void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerMovementScript>();
        anim = gameObject.GetComponent<Animator>();
        headSprite = gameObject.GetComponent<SpriteRenderer>();
        handLSprite = leftHand.GetComponent<SpriteRenderer>();
        handRSprite = rightHand.GetComponent<SpriteRenderer>();
        fireballs = new GameObject[fireballsCount];

        healthBar.SetBossName("Beelzeboss");
        healthBar.SetBossMaxHealth(health);
    }

    void Update()
    {
        if (canAttack)
        {
            DoRandomAttack();
        }
    }

    #region attacks
    private IEnumerator PauseBetweenAttacks(float time)
    {
        yield return new WaitForSeconds(time);
        canAttack = true;
    }

    private void DoRandomAttack()
    {
        float rand = Random.Range(0.0f, 1.0f);
        // 80% chance of fireballs
        if (rand < 0.8f)
        {
            ThrowFireballs();
        }
        // 20% chance of spawning new enemies
        else
        {
            SpawnEnemies();
        }
    }
    #region ThrowFireballs
    private void ThrowFireballs()
    {
        canAttack = false;
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
            StartCoroutine(SpawnFireballWithDelay((i + 1) * fireballDelay, offsetX, i == fireballsCount - 1, target, i));
            offsetX += offsetMultiplier * fireballSpread;
        }
    }

    private IEnumerator SpawnFireballWithDelay(float delay, float offsetX, bool last, Vector2 target, int index)
    {
        yield return new WaitForSeconds(delay);
        var ball = Instantiate(fireball);
        ball.transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        fireballs[index] = ball;

        Vector2 direction = target - (Vector2)transform.position;
        direction = new Vector2(direction.x - offsetX, direction.y);
        direction.Normalize();

        FireballScript script = ball.AddComponent<FireballScript>();
        script.damage = fireballAttackDamage;
        script.direction = direction;
        script.player = playerScript;

        if (last)
        {
            anim.SetTrigger("idle");
            StartCoroutine(PauseBetweenAttacks(attacksDelayTime));
        }
    }
    #endregion ThrowFireballs

    #region SpawnEnemies
    private void SpawnEnemies()
    {

    }
    #endregion SpawnEnemies
    #endregion attacks

    #region takingDamage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AllyProjectile"))
        {
            TakeDamage(collision.gameObject.GetComponent<Bullet>().dmg);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            anim.SetTrigger("die");
            StopAttacks();
        }
        else
        {
            StartCoroutine(StrobeColorHelper(0, 5, headSprite, Color.white, new Color(1, 1, 1, 0.5f)));
            StartCoroutine(StrobeColorHelper(0, 5, handLSprite, Color.white, new Color(1, 1, 1, 0.5f)));
            StartCoroutine(StrobeColorHelper(0, 5, handRSprite, Color.white, new Color(1, 1, 1, 0.5f)));
        }
        healthBar.SetBossCurrentHealth(health);
    }
    public bool TakeBombDamage(int damage)
    {
        TakeDamage(damage);
        return true;
    }

    private void StopAttacks()
    {
        StopAllCoroutines();
        canAttack = false;
        foreach (var ball in fireballs)
        {
            if (ball)
                Destroy(ball);
        }
    }
    #endregion takingDamage

    #region animation
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
        canAttack = true;
    }

    public void CutsceneCancel()
    {
        transform.position = new Vector3(transform.position.x, 16, transform.position.z);
        StartCoroutine(PauseBetweenAttacks(1));
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
    public void FadeOut()
    {
        StartCoroutine(FadeToDestroy(headSprite, 0.0f, 2.0f));
        StartCoroutine(FadeToDestroy(handLSprite, 0.0f, 2.0f));
        StartCoroutine(FadeToDestroy(handRSprite, 0.0f, 2.0f));
    }

    private IEnumerator FadeToDestroy(SpriteRenderer sprite, float aValue, float aTime)
    {
        float alpha = sprite.material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            sprite.material.color = newColor;
            yield return null;
        }
        Destroy(sprite.gameObject);
    }
    #endregion animation
}
