using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBottom : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Chain") {
          Debug.Log("HNNNNGGG");
          transform.parent.parent.gameObject.GetComponent<PushButton>().UnPush();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Chain") {
          transform.parent.parent.gameObject.GetComponent<PushButton>().Push();
        }
    }
}
