using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinish : MonoBehaviour {
    public Animator anim;
    public PushButton a;
    public PushButton b;

    void Start () {

    }

    void Update () {
        if (a.isPushed && b.isPushed) {
            Finish ();
        }
    }

    void Finish () {
        anim.SetBool ("doorOpen", true);
        FindObjectOfType<GameManager> ().Win ();
    }
}
