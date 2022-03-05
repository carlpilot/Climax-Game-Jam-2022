using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    void OnTriggerEnter(Collider col){
      if (col.gameObject.tag == "Player"){
        var pc = col.gameObject.transform.parent.gameObject.GetComponent<PlayerController>();
        if (pc.P1 == col.gameObject.GetComponent<Rigidbody>()){
          // Player 1
          pc.SpeedBoostP1 = 2;
        } else{
          // Player 2
          pc.SpeedBoostP2 = 2;
        }
        Destroy(this.gameObject);
      }
    }
}
