using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public Color selectedOptionColor;
    public Color defaultOptionColor;
    private int option = 0;
    //0 - volume
    //1 - back
    public float volumeChange = 0.1f;
    public Slider slider;

    public List<Image> texts;

    void Start()
    {
        //Disable cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        UpdateMenu();
    }

    void Update()
    {
        if(Input.GetKeyDown("up")){
            if(option > 0){
                option--;
            }else{
                option = 1;
            }
            UpdateMenu();
        }

        if(Input.GetKeyDown("down")){
            if(option < 1){
                option++;
            }
            else{
                option = 0;
            }
            UpdateMenu();
        }

        if(Input.GetKeyDown("space") || Input.GetKeyDown("enter")){
            switch(option){
                case 0: break;
                case 1: SceneManager.LoadScene("MenuScene"); break;
            }
        }

        if(Input.GetKeyDown("left")){
            if(option == 0){
                ChangeVolume(-1*volumeChange);
            }
        }

        if(Input.GetKeyDown("right")){
            ChangeVolume(volumeChange);
        }
    }

    void ChangeVolume(float change){
        float vol = PlayerPrefs.GetFloat("volume", 0.5f);
        float newVol = vol + change;

        if(newVol < 0)
            newVol = 0;

        if(newVol > 1)
            newVol = 1;

        PlayerPrefs.SetFloat("volume", newVol);
    }

    void SetSlider(float value){
        
    }

    void UpdateMenu(){

    }
}
