using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAnimation : MonoBehaviour
{
    public Animator _animator;
    public GameObject objectViseur;

    private bool isMenu = false;
    //private bool groundedPlayer;
    //private bool isAim = false;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(_animator == null) return;

        if(  Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || 
           Input.GetKey(KeyCode.RightArrow) ||Input.GetKey(KeyCode.LeftArrow) ) { 
            _animator.SetBool("isRun",true);  
        }      
        else{
            _animator.SetBool("isRun",false);        
        }

        
        if(Input.GetKeyDown(KeyCode.M)){
            isMenu = !isMenu;
        }
        if(Input.GetButtonDown("Fire1") && !isMenu /* && !isAim */ ){
            _animator.SetTrigger("isShot");
        }

        if(Input.GetKeyDown(KeyCode.R)){
            _animator.SetTrigger("isReaload");
        }

        if(Input.GetKeyDown(KeyCode.P)){
            _animator.SetBool("isAim",true);
            objectViseur.SetActive(false);
            //isAim = true;
        }

        if(Input.GetKeyUp(KeyCode.P)){
            _animator.SetBool("isAim",false);
            objectViseur.SetActive(true);
            //isAim = false;
        }
    }

}
