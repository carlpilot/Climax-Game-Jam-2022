using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
  public PushButton button;
  public float maxHeight;
  public float maxSpeed = 1f;
  Vector3 startPos;
    
    public GameObject target;

    void isPushed() {
        return !target.isAtMax();        
    }
}
