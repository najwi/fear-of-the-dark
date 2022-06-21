using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsScript : MonoBehaviour
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
        SetSlider();
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
        
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("MenuScene");
        }

        if(Input.GetKeyDown("left")){
            if(option == 0){
                ChangeVolume(-1*volumeChange);
            }
        }

        if(Input.GetKeyDown("right")){
            if(option == 0){
                ChangeVolume(volumeChange);
            }
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
        SetSlider(newVol);
        AudioListener.volume = newVol;
    }

    void SetSlider(){
        slider.value = PlayerPrefs.GetFloat("volume", 0.5f);
    }

    void SetSlider(float val){
        slider.value = val;
    }

    void UpdateMenu(){
        for(int i = 0; i<texts.Count; i++)
            texts[i].color = i==option ? selectedOptionColor : defaultOptionColor;
    }
}