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
      for (int i = 0; i < chainLength; i++){
        if (i != chainLength - 1) {
          currentLink = Instantiate(linkPrefab);
        } else{
          currentLink = Instantiate(endLinkPrefab);
        }
        currentLink.transform.position = pos;
        pos += add;
        currentLink.GetComponent<HingeJoint>().connectedBody = prevLink.GetComponent<Rigidbody>();
        //currentLink.GetComponent<Rigidbody>().isKinematic = true;
        prevLink = currentLink;
      }
      currentLink.GetComponents<HingeJoint>()[1].connectedBody = B;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
