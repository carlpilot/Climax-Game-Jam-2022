using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    PlayerController pc;
    PlayerHealth he;

    // Start is called before the first frame update
    void Start() {
        pc = FindObjectOfType<PlayerController> ();
        he = FindObjectOfType<PlayerHealth> ();
    }


    void OnCollisionEnter(Collision other) {
        if (other.collider.GetComponent<Rigidbody> () == pc.P1) {
            he.health1 -= 2;
        } else if (other.collider.GetComponent<Rigidbody>() == pc.P2) {
            he.health2 -= 2;
        } else if (other.gameObject.tag == "Chain") {
            Destroy(other.gameObject);
            FindObjectOfType<GameManager>().Lose();
        }

        // Destroy icicle and respawn
        Destroy(gameObject);
    }
}
