using System.Collections;
using UnityEngine;

public class BossCameraController : MonoBehaviour
{
    public float cameraSpeed;
    public GameObject hud;
    
    private float currentPosX;
    private float minPosX = -3.5f;
    private float maxPosX = 3.5f;

    private float currentPosY;
    private const float PosYOffset = 4f;
    private float currentPosYOffset = PosYOffset;
    private Vector3 velocity = Vector3.zero;
    private GameObject player;
    private GameObject boss;
    private bool unlocked = false;
    private bool firstFrame = true;

    private float cinematicPosY = 10;
    private float cinematicPosX = 0;

    private void Start()
    {
        player = GameObject.Find("Player");
        player.GetComponent<PlayerMovementScript>().enabled = false;
        boss = GameObject.Find("Boss");
        hud.SetActive(false);
    }

    void Update()
    {
        GetInput();
        if (unlocked)
        {
            currentPosX = player.transform.position.x;
            if (currentPosX < minPosX)
                currentPosX = minPosX;
            else if (currentPosX > maxPosX)
                currentPosX = maxPosX;
            currentPosY = player.transform.position.y + currentPosYOffset;
            transform.position = Vector3.SmoothDamp(transform.position,
                new Vector3(currentPosX, currentPosY, transform.position.z), ref velocity, cameraSpeed);
        }
        else
        {
            if (firstFrame)
            {
                firstFrame = false;
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position,
                        new Vector3(cinematicPosX, cinematicPosY, transform.position.z), ref velocity, 2f);
            }
        }
    }

    private void GetInput()
    {
        if(Input.GetKey(KeyCode.Space) && !unlocked)  // Stop cutscene
        {
            var head = boss.transform.Find("Beelzeboss_head").gameObject;
            var handL = boss.transform.Find("Beelzeboss_handL").gameObject;
            var handR = boss.transform.Find("Beelzeboss_handR").gameObject;
            head.GetComponent<Animator>().CrossFade("beelzeboss_idle", 0);
            head.GetComponent<EnemyBossBeelzebossScript>().CutsceneCancel();
            handL.GetComponent<Animator>().CrossFade("hand_idle", 0);
            handL.GetComponent<BelzeebossHandScript>().CutsceneCancel();
            handR.GetComponent<Animator>().CrossFade("hand_idle", 0);
            handR.GetComponent<BelzeebossHandScript>().CutsceneCancel();
            Unlock();
        }
    }

    public void Unlock()
    {
        unlocked = true;
        player.GetComponent<PlayerMovementScript>().enabled = true;
        hud.SetActive(true);
    }

    public void ResetCameraOffset()
    {
        currentPosYOffset = PosYOffset;
    }

    public void SetCameraYOffset(float newOffsetY)
    {
        currentPosYOffset = newOffsetY;
    }

    public void SetCinematicPosY(float newY)
    {
        cinematicPosY = newY;
    }

    public void SetCinematicPosX(float newX)
    {
        cinematicPosX = newX;
    }

    public void BossDied()
    {
        StartCoroutine(CameraToPlayer(5));
    }

    private IEnumerator CameraToPlayer(float time)
    {
        yield return new WaitForSeconds(time);
        SetCameraYOffset(0);
    }
}
