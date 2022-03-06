using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollCredits : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update () {
        
        transform.Translate(Vector3.up * Time.deltaTime);
        
        if(transform.position.y >= 1.80f){
            Destroy(gameObject);
        }
    }
}
