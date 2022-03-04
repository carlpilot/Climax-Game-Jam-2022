using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainGenerator : MonoBehaviour
{
    public GameObject linkPrefab;
    public GameObject endLinkPrefab;
    public int chainLength = 10;
    public Rigidbody A;
    public Rigidbody B;
    // Start is called before the first frame update
    void Start()
    {
      var currentLink = A.gameObject;
      var prevLink = A.gameObject;
      var pos = A.gameObject.transform.position;
      var add = (B.transform.position - A.transform.position) / (float)(chainLength);
      var dir = Quaternion.LookRotation(B.transform.position - A.transform.position);
      bool a = false;
      for (int i = 0; i < chainLength; i++){
        if (i != chainLength - 1) {
          currentLink = Instantiate(linkPrefab);
        } else{
          currentLink = Instantiate(endLinkPrefab);
        }

        /*if (i == 0 || i == chainLength - 1){
          foreach (Collider col in currentLink.GetComponentsInChildren<Collider>()){
            col.enabled = false;
          }
        }*/
        currentLink.transform.position = pos;
        pos += add;
        currentLink.GetComponent<ConfigurableJoint>().connectedBody = prevLink.GetComponent<Rigidbody>();
        //currentLink.GetComponent<Rigidbody>().isKinematic = true;
        //currentLink.transform.Rotate(0, 0, -90);
        if (a){
          foreach (Transform child in currentLink.transform){
            child.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
          }
        }
        prevLink = currentLink;
        a = !a;
      }
      currentLink.GetComponents<ConfigurableJoint>()[1].connectedBody = B;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
