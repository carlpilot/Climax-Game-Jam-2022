using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
  public Animator anim;
  public PushButton a;
  public PushButton b;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if (a.isPushed && b.isPushed){
        Finish();
      }
    }

    void Finish(){
      anim.SetBool("doorOpen", true);
    }
}
