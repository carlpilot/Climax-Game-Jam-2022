using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlaneController : MonoBehaviour
{   

    private bool isActive;
    public Rigidbody planeBody;

    public Vector3 zoomDirection;
    private bool zoomToggle;
    public float activationDist = 5.0f;
    public LayerMask activeLM;

    // Update is called once per frame
    void Update()
    {   
        // Zoom toggle
        if (Input.GetKeyDown (KeyCode.Space) && Physics.OverlapSphere(transform.position, activationDist, activeLM).Length > 0){
            zoomToggle = true;
        }

        // Actual zoom
        if (zoomToggle){
            zoom();
            FindObjectOfType<GameManager> ().Win ();
        }
        
    }

    private void zoom(){
        Debug.Log("Zoom!");
        planeBody.AddForce(zoomDirection);
    }
}
