using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Color selectedOptionColor;
    public Color defaultOptionColor;
    private int option = 0;
    //0 - play
    //1 - settings
    //2 - exit

    public List<Image> texts;

    void Start()
    {

        if (SystemInfo.deviceType != DeviceType.Handheld)
        {
            //Disable cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        UpdateMenu();
        var music = GameObject.FindGameObjectWithTag("Music").GetComponent<Music>();
        if (music)
            music.PlayMusic();
    }

    void Update()
    {
        if(Input.GetKeyDown("right")){
            if(option > 0){
                option--;
            }else{
                option = 2;
            }
            UpdateMenu();
        }

        if(Input.GetKeyDown("left")){
            if(option < 2){
                option++;
            }
            else{
                option = 0;
            }
            UpdateMenu();
        }

        if(Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
            switch(option){
                case 0: StartGame(); break;
                case 1: SceneManager.LoadScene("SettingsScene"); break;
                case 2: ExitGame(); break;
            }
        }
    }

    public void StartGame(){
        SceneManager.LoadScene("PlaymodeScene");
    }

    public void ExitGame(){
        Application.Quit();
    }

    void UpdateMenu(){
        for(int i = 0; i<texts.Count; i++)
            texts[i].color = i==option ? selectedOptionColor : defaultOptionColor;
    }
}
