using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    public int bulletsLeft  = 310 ; // notre reserve de balles (le nombre totale que possède le joueur)
    public int bulletsPerMag = 31; //nombre de balles qu'on a par chargeur
    public int currentBullets; //balles que l'on a

    public float fireRate = 10.0f; //la cadence de votre arme/la frequence de tire
    private float fireTimer = 0.0f;//le temps avant d'acceder au fireRate/avant qu'on puisse tirer une autre fois

    public float range =50.0f;//la distance à laquelle on va pouvoir tirer maximum(ici 50 metres)


    public Transform shootPoint; //object pour savoir jusqu'à où tirer
    
    public AudioSource SoundShot;
    public AudioSource SoundReload;

    public GameObject _bulletHolePrefab;

    public ParticleSystem muzzelFlash ;


    public GameObject objectPlay;
    public GameObject objectPanelSetting;
    public GameObject objectPanelTouch;
    private bool isMenu = false;
    // Start is called before the first frame update
    void Start()
    {
        //currentBullets = bulletsPerMag; //des qu'on lance le jeu, les balles qu'on a en ce moment, c'est les mêmes qu'on a dans le chargeur(ici 31)
        currentBullets = 40 ; //des qu'on lance le jeu, les balles qu'on a en ce moment, c'est les mêmes qu'on a dans le chargeur(ici 31)
   
    }

    // Update is called once per frame 
    void Update()
    {
        Debug.DrawRay(shootPoint.position, shootPoint.forward * range);

        if(objectPlay.activeSelf || objectPanelTouch.activeSelf || objectPanelSetting.activeSelf){
			 isMenu = true;
		}
		else{
			isMenu = false;
		}

        if(Input.GetButton("Fire1") && !isMenu){ //si on appuie sur le bouton gauche de la souris (edit/projet Setting/Input)
            Firee(); //fonction pour tirer
        }

        if(Input.GetKeyDown(KeyCode.R)){
            Reload();
        }

        if(fireTimer < fireRate){
            fireTimer += Time.deltaTime; //fireTimer += Time.Time
        }
        
    }

    private void Firee(){

        if(fireTimer < fireRate || currentBullets <= 0){
            return;
        }
        
        //on va dire que la ligne commence de la direction de l'objet shootPoint jusqu'à 
        RaycastHit hit;

        if(Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range)){
            Debug.Log(hit.transform.name + " has been Found");
            if(hit.transform.gameObject.tag == "Mur"){
                Instantiate(_bulletHolePrefab, hit.point + hit.normal/10000 , Quaternion.LookRotation(hit.normal));
            }
            
            if(hit.transform.gameObject.tag == "Ennemi"){
                Destroy(hit.transform.gameObject);
                //Instantiate(_bulletHolePrefab, hit.point + hit.normal/10000 , Quaternion.LookRotation(hit.normal));
            }
		}

        if(SoundShot) SoundShot.Play();

		if( /*!Input.GetKey(KeyCode.LeftShit) || */ !Input.GetKey(KeyCode.R) ) {
            muzzelFlash.Play();
            currentBullets--;
            //fireTimer=0.0f;
        }
        fireTimer=0.0f;
    }

    private void Reload(){
        if(SoundReload) SoundReload.Play();
        if(bulletsLeft <= 0) return ;

        int bulletsToLoad = bulletsPerMag - currentBullets;

        int bulletsToDeduct = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;

        bulletsLeft -= bulletsToDeduct;
        currentBullets += bulletsToDeduct;
    }

}

