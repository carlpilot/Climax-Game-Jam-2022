using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderGenerator : MonoBehaviour
{
    public GameObject prefab;
    public float timeDelay;
    float t;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      t += Time.deltaTime;
      if (t > timeDelay){
        t -= timeDelay;
        var p = Instantiate(prefab);
        p.transform.position = transform.position;
      }
    }
}
