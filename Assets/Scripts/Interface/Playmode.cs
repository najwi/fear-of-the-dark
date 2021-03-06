using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playmode : MonoBehaviour
{
    public Color selectedOptionColor;
    public Color defaultOptionColor;
    private int option = 0;
    //0 - single
    //1 - multi offline
    //2 - back

    private Music music;
    public List<GameObject> texts;

    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<Music>();
        if (music)
            music.PlayMusic();
        UpdateMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("down")){
            if(option < texts.Count-1){
                option++;
            }else{
                option = 0;
            }
            UpdateMenu();
        }

        if(Input.GetKeyDown("up")){
            if(option > 0){
                option--;
            }
            else{
                option = texts.Count-1;
            }
            UpdateMenu();
        }

        if(Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
            switch(option){
                case 0: StartGameSingle(); break;
                case 1: StartGameMultiOffline(); break;
                case 2: Back(); break;
            }
        }
    }

    public void StartGameSingle(){
        PlayerMovementScript.multiplayer = false;
        PrepPlayer();
        music.StopMusic();
        SceneManager.LoadScene("Game");
    }

    public void StartGameMultiOffline(){
        PlayerMovementScript.multiplayer = true;
        PrepPlayer();
        music.StopMusic();
        SceneManager.LoadScene("Game");
    }

    public void Back(){
        SceneManager.LoadScene("MenuScene");
    }

    public void PrepPlayer(){
        PlayerMovementScript.maxHp = 4;
        PlayerMovementScript.moveSpeed = 5;
        PlayerMovementScript.bombs = 2;
        PlayerMovementScript.notes = 5;
        PlayerMovementScript.damage = 3;
        PlayerMovementScript.currentHp = 4;
    }

    void StartGame(){
        SceneManager.LoadScene("Game");
        if(music)
            music.StopMusic();
    }

    void UpdateMenu(){
        for(int i = 0; i<texts.Count; i++)
            texts[i].GetComponent<SpriteRenderer>().color = i==option ? selectedOptionColor : defaultOptionColor;
    }
}
