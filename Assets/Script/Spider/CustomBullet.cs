using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomBullet : MonoBehaviour
{
    //Assignables
    public Rigidbody rb;
	public string LevelToLoad = "02GameOver";

    // Player's health

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.gameObject);
            Destroy(rb);
            damage(10);
        }
    }

    public void damage(int damageAmount)
    {
        GameObject test = GameObject.Find("healthManager");
        Health playerLife = (Health)test.GetComponent(typeof(Health));
        playerLife.health -= damageAmount;

        if (playerLife.health <= 0)
        {
           ChangeScene();
        }

    }
    	private void ChangeScene(){ 
        SceneManager.LoadScene(LevelToLoad);
    }
}
