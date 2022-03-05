using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLift : MonoBehaviour
{
  public PushButton button;
  public float maxHeight;
  public float maxSpeed = 1f;
  Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
      startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      if (button.isPushed){
        transform.position = Vector3.MoveTowards(transform.position, startPos + (Vector3.up * maxHeight), maxSpeed*Time.deltaTime);
      } else{
        transform.position = Vector3.MoveTowards(transform.position, startPos, maxSpeed*Time.deltaTime);
      }
    }
}
