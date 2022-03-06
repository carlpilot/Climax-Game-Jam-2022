using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollCredits : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update () {
        
        transform.Translate(0, 2, 00);
        
        if(transform.position.y >= 4200){
            Destroy(gameObject);
        }
    
    }
}
