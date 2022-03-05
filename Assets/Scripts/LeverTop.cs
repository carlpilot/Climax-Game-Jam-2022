using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTop : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Chain") {
            Debug.Log("HNMM");
          transform.parent.gameObject.GetComponent<PushButton>().Push();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Chain") {
          transform.parent.gameObject.GetComponent<PushButton>().UnPush();
        }
    }
}
