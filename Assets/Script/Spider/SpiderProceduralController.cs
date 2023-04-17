using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class SpiderProceduralController : MonoBehaviour
{
    // Position initiale de la patte
    Vector3 originalPosition;

    // Cible au sol qui fait avancer la patte   
    [SerializeField] LegTargetMovement targetMovement;

    // Distance (entre la position du targetMovement et la position de la patte) à partir de laquelle on bouge la patte
    [SerializeField] float moveDistance;

    // La durée d'un pas (Le temps que la patte prend pour se lever et redescendre au sol)
    [SerializeField] float moveDuration = 1.0f;

    // Booléen qui renvoie true si la patte bouge, false sinon
    public bool isMoving = false;
    bool moving = false;

    // Fraction de la distance maximale de la cible qu'on veut dépasser
    // Quand la patte redescend au sol, elle dépasse un peu la cible d'abord, puis se positionne sur la cible
    // Pour un mouvement plus réaliste 
    [SerializeField] float stepOvershootFraction;

    [SerializeField] SpiderProceduralController[] oppositeLeg;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        float distanceFromStart = Vector3.Distance(transform.position, targetMovement.transform.position);

        // Si la patte ne bouge pas et la position de la cible est trop loin de la patte, on bouge la patte
        if (!oppositeLegMoving() && distanceFromStart > moveDistance || moving)
        {
            moving = true;
            StartCoroutine(MoveToTarget());
            transform.position = targetMovement.transform.position;
            originalPosition = transform.position;
        }
        else
        {
            transform.position = originalPosition;
        }
    }

    // On utilise une coroutine pour pouvoir répartir le mouvement d'un pas sur plusieurs frames.
    // La coroutine nous permet de faire des pauses dans l'exécution du code,
    // ici on fait des pauses à chaque frame pour voir correctement le mouvement de la patte qui 
    // monte et redescend.
    // 

    IEnumerator MoveToTarget()
    {
        isMoving = true; // La patte bouge

        // Position initiale de la patte
        Vector3 startPoint = transform.position;
        Quaternion startRot = transform.rotation;

        Quaternion endRot = targetMovement.transform.rotation;

        // Vecteur directionnel de la patte à la cible
        Vector3 towardHome = (targetMovement.transform.position - transform.position);

        // Distance totale du pas 
        float overshootDistance = moveDistance * stepOvershootFraction;
        Vector3 overshootVector = towardHome * overshootDistance;
        overshootVector = Vector3.ProjectOnPlane(overshootVector, Vector3.up);

        // Position finale de la patte
        Vector3 endPoint = targetMovement.transform.position + overshootVector;

        // Position centrale en haut du sol par laquelle la patte passe
        Vector3 centerPoint = (startPoint + endPoint) / 2;
        centerPoint += -targetMovement.transform.up * Vector3.Distance(startPoint, endPoint) / 2f;

        // Temps écoulé depuis le début du pas
        float timeElapsed = 0;

        do
        {
            timeElapsed += Time.deltaTime;
            float normalizedTime = timeElapsed / moveDuration;
            normalizedTime = Easing.InOutCubic(normalizedTime);

            // Interpolation de la position
            transform.position = Vector3.Lerp(
                                    Vector3.Lerp(startPoint, centerPoint, normalizedTime), // la patte atteint d'abord la position centrale
                                    Vector3.Lerp(centerPoint, endPoint, normalizedTime), // La patte atteint ensuite la position finale au sol
                                    normalizedTime
                                  );

            transform.rotation = Quaternion.Slerp(startRot, endRot, normalizedTime);


            yield return null; // Attente pendant 1 frame pour marquer une pause dans l'exécution du code

        } while (timeElapsed < moveDuration);

        isMoving = false; // Le pas est terminé, la patte ne bouge plus
        moving = false;
    }

    public bool oppositeLegMoving()
    {
        for (int i = 0; i < oppositeLeg.Length; i++)
        {
            if (oppositeLeg[i].isMoving)
            {
                return true;
            }
        }

        return false;
    }
}
