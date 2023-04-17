using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuScene01 : MonoBehaviour
{
    public GameObject objectViseur;
    public GameObject objectPlay;
    public GameObject objectTouch;
    public GameObject objectPanelTouch;
    public GameObject objectSetting;
    public GameObject objectPanelSetting;
    public GameObject objectQuit;
    private bool visibleMenu = false;
    private bool visiblePanelOptions = false;
    private bool visiblePanelTouch = false;

    public Dropdown DResolution;
    public Slider SliderVolumeMusique;
    public AudioSource AudioMusique;
    public Slider SliderVolumeSon;
    public AudioSource AudioRun;
    public AudioSource AudioJump;
    public AudioSource AudioShot;

    //fonction qui permis de changer de scene    
    public void ChangeScene(string LevelToLoad){ 
        SceneManager.LoadScene(LevelToLoad);
    }
    
    //fonction qui permet de lancer le jeu
    public void PlayButton(){
        objectViseur.SetActive(true);
        objectPlay.SetActive(false);
        objectTouch.SetActive(false);
        objectSetting.SetActive(false);
        objectQuit.SetActive(false);
        visibleMenu = false;
        Cursor.visible = false;

    }

    //fonction qui permet d'afficher le Panel des touches du jeu
    public void TouchButton(){
        Cursor.visible = true;
        visiblePanelTouch = true ;  
        objectViseur.SetActive(false);
        objectPlay.SetActive(false);
        objectTouch.SetActive(false);
        objectSetting.SetActive(false);
        objectQuit.SetActive(false);
        objectPanelSetting.SetActive(false);    
        objectPanelTouch.SetActive(true);

    }

    //fonction qui permet d'afficher le Panel d'option (permet de regler la résolution + le son)
    public void SettingButton(){
        Cursor.visible = true;
        visiblePanelOptions = true ;  
        objectViseur.SetActive(false);
        objectPlay.SetActive(false);
        objectSetting.SetActive(false);
        objectQuit.SetActive(false); 
        objectPanelTouch.SetActive(false);    
        objectPanelSetting.SetActive(true);   
    }
    
    //fonction qui permet de quitter le jeu
    public void QuitButton(){
        Application.Quit();
    }

    //fonction qui permet de regler  le volume des musiques du fond du jeu grace au panel option
    public void SliderMusiqueChanger(){
        AudioMusique.volume = SliderVolumeMusique.value;
        //public Text TxtVolumeMusique
        //TxtVolumeMusique.text = "Volume " + (AudioMusique.volume * 100).ToString("00") + "%";
    } 

    //fonction qui permet de regler le volume des differents sons (ex : tirer/sauter...) du jeu grace au panel option
    public void SliderSonChanger(){
        AudioRun.volume = SliderVolumeSon.value;
        AudioJump.volume = SliderVolumeSon.value;
        AudioShot.volume = SliderVolumeSon.value;
    }

    //fonction qui permet de regler la resolution du jeu grace au panel option
    public void SetResolution(){
        switch(DResolution.value){
            case 0:
                Screen.SetResolution(640,360,true);
                break;
            case 1:
                Screen.SetResolution(1920,1080,true);
                break;
        }
    }

    public void CloseSettingButton(){
        visiblePanelOptions = false ;    
        objectPanelSetting.SetActive(false);
        if(visiblePanelTouch){
            TouchButton();
        }
        else if(visibleMenu){
            objectPlay.SetActive(true);
            objectTouch.SetActive(true);
            objectSetting.SetActive(true);
            objectQuit.SetActive(true); 
        }
        else
        {
            Cursor.visible = false;
            objectViseur.SetActive(true);
        }
    }

    public void CloseTouchButton(){
        visiblePanelTouch = false ;    
        objectPanelTouch.SetActive(false);

        if(visiblePanelOptions){
            SettingButton();
        }

        else if(visibleMenu){
            objectPlay.SetActive(true);
            objectTouch.SetActive(true);
            objectSetting.SetActive(true);
            objectQuit.SetActive(true); 
        }

        else
        {
            Cursor.visible = false;
            objectViseur.SetActive(true);
        }
    }

    void Update(){
       
        if( Input.GetKeyDown(KeyCode.M) && !visiblePanelOptions && !visiblePanelTouch){
            visibleMenu = !visibleMenu;
            objectViseur.SetActive(!visibleMenu);
            objectPlay.SetActive(visibleMenu);
            objectTouch.SetActive(visibleMenu);
            objectSetting.SetActive(visibleMenu);
            objectQuit.SetActive(visibleMenu);
            Cursor.visible = visibleMenu;

        }
                
        if( Input.GetKeyDown(KeyCode.O) ){
           if(!visiblePanelOptions) SettingButton();
           else CloseSettingButton();
        }
        
        if( Input.GetKeyDown(KeyCode.P) ){
           if(visiblePanelTouch) CloseTouchButton();
           else TouchButton();
        }
    }  
}
