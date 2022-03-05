using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalOscilator : MonoBehaviour
{
  public float maxX = 5;
  Vector3 startPos;
  float dir = 1;
  public float speed = 1f;
  public PushButton button;
    // Start is called before the first frame update
    void Start()
    {
      startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      if (button.isPushed){
        if (transform.position.x > startPos.x + maxX){
          dir = -1;
        } else if (transform.position.x < startPos.x){
          dir = 1;
        }
        transform.Translate(new Vector3(dir*speed*Time.deltaTime, 0, 0));
      }
    }
}
