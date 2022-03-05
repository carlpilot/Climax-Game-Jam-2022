using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
  public GameObject unPushedButton;
  public GameObject pushedButton;
  public bool isPushed{get; private set;}
  public bool persist;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPushed){
          unPushedButton.SetActive(false);
          pushedButton.SetActive(true);
        } else{
          unPushedButton.SetActive(true);
          pushedButton.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"){
          Push();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !persist){
          UnPush();
        }
    }

    public void Push(){
      isPushed = true;
    }

    public void UnPush(){
      isPushed = false;
    }
}
