using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlaneController : MonoBehaviour
{   

    private bool isActive;
    public Rigidbody planeBody;

    public Vector3 zoomDirection;
    private bool zoomToggle;
    

    // Update is called once per frame
    void Update()
    {   
        // Zoom toggle
        if (isActive){
            if (Input.GetKeyDown(KeyCode.Space)){
                zoomToggle = true;
            }
        }   

        if (Input.GetKeyUp(KeyCode.Space)){
            zoomToggle = true;
        }

        // Actual zoom
        if (zoomToggle){
            zoom();
            FindObjectOfType<GameManager> ().Win ();
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {   
        Debug.Log("Touch");
        isActive = true;
    }

    private void OnTriggerExit(Collider other){
        Debug.Log("Leave");
        isActive = false;
    }

    private void zoom(){
        Debug.Log("Zoom!");
        planeBody.AddForce(zoomDirection);
    }
}
