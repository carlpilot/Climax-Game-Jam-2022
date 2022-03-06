using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalOscilator : MonoBehaviour
{
  public float maxX = 5;
  Vector3 startPos;
  float dir = 1;

  public Vector3 direction = new Vector3(1, 0, 0);
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
        if ((transform.position - (startPos + (maxX * direction))).magnitude < 0.1f){
          dir = -1;
        } else if ((transform.position - startPos).magnitude < 0.1f){
          dir = 1;
        }
        transform.Translate(direction * dir * speed * Time.deltaTime);
      }
    }
}
