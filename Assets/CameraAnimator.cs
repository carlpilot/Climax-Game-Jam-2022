using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    public GameObject[] targets;
    public float timePerTarget = 5f;
    public float cameraTurnSpeed = 1f;
    public float cameraMoveSpeed = 1f;
    public float targetDistance = 5f;
    int i = 0;
    float timer = 0;
    Vector3 tp;
    // Start is called before the first frame update
    void Start()
    {
      recalcPos();
    }

    // Update is called once per frame
    void Update()
    {
        if (i < targets.Length && timer > timePerTarget){
          i ++;
          i = i%(targets.Length);
          timer = 0;
          recalcPos();
        }
        timer += Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, tp, cameraMoveSpeed*Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targets[i].transform.position-transform.position), cameraTurnSpeed*Time.deltaTime);
    }

    void recalcPos(){
      tp = targets[i].transform.position + ((transform.position - targets[i].transform.position).normalized * targetDistance);
    }
}
