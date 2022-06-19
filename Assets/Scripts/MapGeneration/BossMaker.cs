using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMaker : MonoBehaviour
{
    public GameObject checker;
    private RoomTemplates templates;

    // Start is called before the first frame update
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }

    public void MakeBossRoom(){
        Transform walls = null;
        Transform floor = null;
        foreach (Transform roomElem in gameObject.transform){
            if (roomElem.gameObject.name == "Walls"){
                walls = roomElem;
            }else if(roomElem.gameObject.name == "Floor"){
                floor = roomElem;
            }
            if (roomElem.gameObject.CompareTag("ObstacleTemplate")){
                Destroy(roomElem.gameObject);
            }
        }
        // walls = gameObject.transform.GetChild(0);
        // walls.gameObject.SetActive(true);
        // floor = gameObject.transform.GetChild(1);
        foreach (Transform wallChild in walls.gameObject.transform){
            SpriteRenderer wallChildSpriteRenderer = wallChild.gameObject.GetComponent<SpriteRenderer>();
            int random = Random.Range(0, templates.bossWallSprites.Length);
            wallChildSpriteRenderer.sprite = templates.bossWallSprites[random];
        }
        foreach (Transform floorChild in floor.gameObject.transform){
            SpriteRenderer floorChildSpriteRenderer = floorChild.gameObject.GetComponent<SpriteRenderer>();
            int random = Random.Range(0, templates.bossFloorSprites.Length);
            floorChildSpriteRenderer.sprite = templates.bossFloorSprites[random];
        }
    }
}
