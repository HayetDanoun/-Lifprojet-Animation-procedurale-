using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegTargetMovement : MonoBehaviour
{
    int layerMask;

    // Start is called before the first frame update
    void Start()
    {
       layerMask = LayerMask.GetMask("Ground");
       
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + new Vector3(0f, 1f, 0f), -transform.up, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, -transform.up * 10, Color.red);
            transform.position = hit.point; // + new Vector(0f, 2f, 0f) Pour que ce soit bien sur le sol (pas en dessous)
            Debug.Log(hit.collider.name);
        }
    }
}
