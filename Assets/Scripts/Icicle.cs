using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    PlayerController pc;
    PlayerHealth he;
    private float x;
    private float y;
    private float z;

    // Start is called before the first frame update
    void Start() {
        pc = FindObjectOfType<PlayerController> ();
        he = FindObjectOfType<PlayerHealth> ();
        Vector3 v = transform.position;
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
    }


    void OnCollisionEnter(Collision other) {
        /*
        if (other.GetComponent<Rigidbody> () == null) return;
        if (other.GetComponent<Rigidbody> () == pc.P1) {
            pc.toggle_burn(1, true);
        } else if (other.GetComponent<Rigidbody>() == pc.P2) {
            pc.toggle_burn(2, true);
        }*/

        // Destroy icicle and respawn
        Debug.Log("AKJESKEJFHKPSJEFHKSJEF");
        transform.position = new Vector3(x, y, z);
    }
}
