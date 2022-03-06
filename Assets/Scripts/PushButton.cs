using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
  public GameObject unPushedButton;
  public GameObject unPushedButtonPersist;
  public GameObject pushedButton;
  public bool isPushed{get; private set;}
  public bool persist;
  List<GameObject> touchingObjects;
    // Start is called before the first frame update
    void Start()
    {
      if(persist){
      	unPushedButton.SetActive(false);
      	unPushedButton = unPushedButtonPersist;
      	unPushedButton.SetActive(true);
      } 
      touchingObjects = new List<GameObject>();
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
          if (touchingObjects.Count == 0) Push();
          if (!touchingObjects.Contains(other.gameObject)) touchingObjects.Add(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !persist){
          if (touchingObjects.Contains(other.gameObject)) touchingObjects.Remove(other.gameObject);
          if (touchingObjects.Count == 0) UnPush();
        }
    }

    public void Push(){
      isPushed = true;
    }

    public void UnPush(){
      isPushed = false;
    }
}
