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

    public void SetDifficulty(int diff){
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

        if (diff == 0){
            int r = Random.Range(0, templates.easyObstacleTemplates.Length);
            if (gameObject.name != "Opened" && gameObject.name != "Shop"){
                Instantiate(templates.easyObstacleTemplates[r], transform.position, Quaternion.identity).transform.parent = gameObject.transform;
            }
        }else if (diff == 1){
            foreach (Transform floorChild in floor.gameObject.transform){
                SpriteRenderer floorChildSpriteRenderer = floorChild.gameObject.GetComponent<SpriteRenderer>();
                int random = Random.Range(0, templates.mediumFloorSprites.Length);
                floorChildSpriteRenderer.sprite = templates.mediumFloorSprites[random];
            }
            foreach (Transform wallChild in walls.gameObject.transform){
                SpriteRenderer wallChildSpriteRenderer = wallChild.gameObject.GetComponent<SpriteRenderer>();
                int random = Random.Range(0, templates.mediumWallSprites.Length);
                wallChildSpriteRenderer.sprite = templates.mediumWallSprites[random];
            }
            int r = Random.Range(0, templates.mediumObstacleTemplates.Length);
            if (gameObject.name != "Opened" && gameObject.name != "Shop"){
                Instantiate(templates.mediumObstacleTemplates[r], transform.position, Quaternion.identity).transform.parent = gameObject.transform;
            }
        }else{
            foreach (Transform floorChild in floor.gameObject.transform){
                SpriteRenderer floorChildSpriteRenderer = floorChild.gameObject.GetComponent<SpriteRenderer>();
                int random = Random.Range(0, templates.hardFloorSprites.Length);
                floorChildSpriteRenderer.sprite = templates.hardFloorSprites[random];
            }
            foreach (Transform wallChild in walls.gameObject.transform){
                SpriteRenderer wallChildSpriteRenderer = wallChild.gameObject.GetComponent<SpriteRenderer>();
                int random = Random.Range(0, templates.hardWallSprites.Length);
                wallChildSpriteRenderer.sprite = templates.hardWallSprites[random];
            }
            int r = Random.Range(0, templates.hardObstacleTemplates.Length);
            if (gameObject.name != "Opened" && gameObject.name != "Shop"){
                Instantiate(templates.hardObstacleTemplates[r], transform.position, Quaternion.identity).transform.parent = gameObject.transform;
            }
        }

        //-------------------------------------------
        // foreach (Transform wallChild in walls.gameObject.transform){
        //     SpriteRenderer wallChildSpriteRenderer = wallChild.gameObject.GetComponent<SpriteRenderer>();
        //     int random = Random.Range(0, templates.bossWallSprites.Length);
        //     wallChildSpriteRenderer.sprite = templates.bossWallSprites[random];
        // }
        // foreach (Transform floorChild in floor.gameObject.transform){
        //     SpriteRenderer floorChildSpriteRenderer = floorChild.gameObject.GetComponent<SpriteRenderer>();
        //     int random = Random.Range(0, templates.bossFloorSprites.Length);
        //     floorChildSpriteRenderer.sprite = templates.bossFloorSprites[random];
        // }
    }
}
