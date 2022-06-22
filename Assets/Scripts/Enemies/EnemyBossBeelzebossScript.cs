 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossBeelzebossScript : MonoBehaviour, TakeBombDamageDecorator
{
    #region variables
    public GameObject bossCamera;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject fireball;
    public GameObject[] enemiesPrefabs;
    public GameObject enemyPortalPrefab;
    public BossHealthBarScript healthBar;
    public GameObject[] lavaToDelete;
    public AudioSource fireballSound;
    public int health = 500;
    public float attacksDelayTime = 3;
    public int fireballsCount = 10;
    public int fireballAttackDamage = 2;
    public float fireballSpread = 2.5f;
    public float fireballDelay = 0.3f;
    public int enemiesCount = 5;

    private GameObject player;
    private PlayerMovementScript playerScript;
    private Animator anim;
    private SpriteRenderer headSprite;
    private SpriteRenderer handLSprite;
    private SpriteRenderer handRSprite;

    private bool canAttack = false;
    private bool spawned = false;
    // private bool invulnerable = false;
    private int damageTakenDivider = 1;
    private int currentHealth;
    private bool recentlySpawned = true;
    private bool enteredPhaseTwo = false;
    private bool enteredPhaseThree = false;
    private List<GameObject> fireballs;
    private GameObject enemiesContainer;
    private GameObject[] enemiesAlive;
    private int fireballsInRow = 0;

    #endregion variables

    private void Start()
    {
        player = GameObject.Find("Player");
        if (PlayerMovementScript.multiplayer)
            health = (int)(health * 1.3);
        playerScript = player.GetComponent<PlayerMovementScript>();
        anim = gameObject.GetComponent<Animator>();
        headSprite = gameObject.GetComponent<SpriteRenderer>();
        handLSprite = leftHand.GetComponent<SpriteRenderer>();
        handRSprite = rightHand.GetComponent<SpriteRenderer>();
        enemiesAlive = new GameObject[enemiesCount];
        fireballs = new List<GameObject>();
        currentHealth = health;
        enemiesContainer = new GameObject("enemies");

        healthBar.SetBossName("Beelzeboss");
        healthBar.SetBossMaxHealth(health);
    }

    void Update()
    {
        if (canAttack)
        {
            DoRandomAttack();
        }
        else
        {
            CheckIfAllSpawnedAreDead();
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
        if (rand < 0.8f && fireballsInRow < 5)
        {
            ThrowFireballs();
            fireballsInRow += 1;
            if (fireballsInRow > 2)  // Wait two attacks before spawning again
                recentlySpawned = false;
        }
        // 20% chance of spawning new enemies
        else if (!recentlySpawned)
        {
            fireballsInRow = 0;
            SpawnEnemies();
            recentlySpawned = true;
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
            StartCoroutine(SpawnFireballWithDelay((i + 1) * fireballDelay, offsetX, i == fireballsCount - 1, target));
            offsetX += offsetMultiplier * fireballSpread;
        }
    }

    private IEnumerator SpawnFireballWithDelay(float delay, float offsetX, bool last, Vector2 target)
    {
        yield return new WaitForSeconds(delay);
        fireballSound.Play();
        var ball = Instantiate(fireball);
        ball.transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        fireballs.Add(ball);

        Vector2 direction = target - (Vector2)transform.position;
        direction = new Vector2(direction.x - offsetX, direction.y);
        direction.Normalize();

        FireballScript script = ball.AddComponent<FireballScript>();
        script.damage = fireballAttackDamage;
        script.direction = direction;
        script.player = playerScript;
        if (enteredPhaseThree)
            script.moveSpeed *= 1.5f;

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
        anim.SetTrigger("spawn");
        bossCamera.GetComponent<BossCameraController>().SetCameraYOffset(1);
        canAttack = false;
        damageTakenDivider = 4;
        enemiesAlive = new GameObject[enemiesCount];
        GameObject[] portals = PlacePortals(enemiesCount);
        for (int i = 0; i < enemiesCount; i++)
        {
            int enemyIndex = Random.Range(0, 10);
            if (enemyIndex < 3)
                enemyIndex = 0;  // 20% hound
            else if (enemyIndex < 9)
                enemyIndex = 1;  // 70% gog
            else
                enemyIndex = 2;  // 10% skeleton
            GameObject enemy = enemiesPrefabs[enemyIndex];
            StartCoroutine(SpawnEnemy(enemy, i, i == enemiesCount - 1, portals[i]));
        }
    }

    private GameObject[] PlacePortals(int enemiesCount)
    {
        GameObject[] portals = new GameObject[enemiesCount];
        int count = 0;
        while (count < enemiesCount)
        {
            bool taken = false;
            float x = Random.Range(0, 23) - 11;  // from -11 to 11
            float y = Random.Range(0.0f, 8.5f) - 2.5f;  // from -2.5 to 6
            for (int i = 0; i < count; i++)
            {
                if (portals[i].transform.position.x == x && portals[i].transform.position.y == y)  // If already taken
                {
                    taken = true;
                }
            }
            if (!taken)
            {
                portals[count] = Instantiate(enemyPortalPrefab);
                portals[count].transform.position = new Vector3(x, y, portals[count].transform.position.z);
                count += 1;
            }
        }
        return portals;
    }

    private IEnumerator SpawnEnemy(GameObject enemy, int index, bool last, GameObject portal)
    {
        yield return new WaitForSeconds(2);
        portal.SetActive(false);
        enemiesAlive[index] = Instantiate(enemy, enemiesContainer.transform);
        enemiesAlive[index].transform.position = portal.transform.position;
        Destroy(portal);
        if (last)
            StartCoroutine(SetSpawnedAfterDelay(0.1f));
    }

    private IEnumerator SetSpawnedAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        spawned = true;
    }

    private bool AreAllSpawnedDead()
    {
        int alive = 0;
        for (int i = 0; i < enemiesAlive.Length; i++)
        {
            if (enemiesAlive[i])
                alive += 1;
        }
        return alive == 0;
    }

    private void CheckIfAllSpawnedAreDead()
    {
        if (!spawned || !AreAllSpawnedDead())
            return;
        spawned = false;
        damageTakenDivider = 1;
        anim.SetTrigger("idle");
        bossCamera.GetComponent<BossCameraController>().ResetCameraOffset();
        StartCoroutine(PauseBetweenAttacks(attacksDelayTime));
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

    private int ApplyDamageReduction(int damage)
    {
        if (damage == 0)
            return 0;
        int damageTaken = damage / damageTakenDivider;
        if (damageTaken <= 0)
            damageTaken = 1;
        return damageTaken;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth == 0)
            return;
        int damageTaken = ApplyDamageReduction(damage);
        currentHealth -= damageTaken;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            anim.SetTrigger("die");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            leftHand.GetComponent<BoxCollider2D>().enabled = false;
            rightHand.GetComponent<BoxCollider2D>().enabled = false;
            foreach(var lava in lavaToDelete)
                Destroy(lava, 5f);
            StopAttacks();
            Destroy(healthBar.gameObject, 5f);
            bossCamera.GetComponent<BossCameraController>().BossDied();
        }
        else
        {
            if (!enteredPhaseTwo && currentHealth < 2 * health / 3)
            {
                enteredPhaseTwo = true;
                attacksDelayTime /= 2;
                fireballSpread *= 0.80f;
                fireballsCount = (int)(fireballsCount * 1.2f);
                fireballDelay *= 0.80f;
                enemiesCount += 2;
            } else if (!enteredPhaseThree && currentHealth <  health / 3)
            {
                enteredPhaseThree = true;
                attacksDelayTime /= 2;
                fireballSpread = 1.0f;
                fireballsCount = 6;
                fireballDelay *= 0.80f;
            }
            StartCoroutine(StrobeColorHelper(0, 5, headSprite, Color.white, new Color(1, 1, 1, 0.5f)));
            StartCoroutine(StrobeColorHelper(0, 5, handLSprite, Color.white, new Color(1, 1, 1, 0.5f)));
            StartCoroutine(StrobeColorHelper(0, 5, handRSprite, Color.white, new Color(1, 1, 1, 0.5f)));
        }
        healthBar.SetBossCurrentHealth(currentHealth);
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
        while (fireballs.Count > 0)
        {
            var ball = fireballs[0];
            fireballs.RemoveAt(0);
            if (ball)
            {
                ball.SetActive(false);
                Destroy(ball);
            }
        }
        foreach(var enemy in enemiesAlive)
        {
            if (enemy)
            {
                enemy.SetActive(false);
                Destroy(enemy, 0.1f );
            }
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
