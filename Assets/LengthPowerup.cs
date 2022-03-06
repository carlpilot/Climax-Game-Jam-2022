using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LengthPowerup : MonoBehaviour
{
  public GameObject sourcePrefab;
  void OnTriggerEnter(Collider col){
    if (col.gameObject.tag == "Player"){
      foreach (GameObject g in GameObject.FindGameObjectsWithTag("Chain")){
        g.transform.localScale *= 2;
        g.GetComponent<ConfigurableJoint>().connectedAnchor *= 0.8f;
        //g.GetComponent<ConfigurableJoint>().anchor *= 2;
      }
      Instantiate(sourcePrefab);
      Destroy(this.gameObject);
    }
  }
}
