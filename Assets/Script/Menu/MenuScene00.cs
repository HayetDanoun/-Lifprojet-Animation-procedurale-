using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuScene00 : MonoBehaviour
{
    public GameObject objectPlayGame;
    public GameObject objectQuit;
    private bool visibleMenu = true;

    //fonction qui permet de changer de scene    
    public void ChangeScene(string LevelToLoad){ 
        SceneManager.LoadScene(LevelToLoad);
    }
    
    //fonction qui permet de lancer le jeu
    public void PlayButton(){
        objectPlayGame.SetActive(false);
        objectQuit.SetActive(false);
        visibleMenu = false;
    }
    
    //fonction qui permet de quitter le jeu
    public void QuitButton(){
        Application.Quit();
    }

    void Update(){
       
        if( Input.GetKeyDown(KeyCode.M) ){
            visibleMenu = !visibleMenu;
            objectPlayGame.SetActive(visibleMenu);
            objectQuit.SetActive(visibleMenu);
        }
    }  
}
