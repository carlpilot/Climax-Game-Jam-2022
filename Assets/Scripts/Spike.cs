using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {

    public float damagePerSecond = 3f;
    int counter1 = 0;
    int counter2 = 0;

    int ticksPerDamage;

    private void Awake () {
        ticksPerDamage = Mathf.FloorToInt (50.0f / damagePerSecond);
    }

    void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "Chain") {
            Destroy (col.gameObject);
            FindObjectOfType<GameManager> ().Lose ();
        }
    }

    private void OnTriggerStay (Collider col) {
        if (col.gameObject.tag == "Player") {
            PlayerController pc = col.transform.parent.GetComponent<PlayerController> ();
            if (col.attachedRigidbody == pc.P1) counter1++;
            if (col.attachedRigidbody == pc.P2) counter2++;
            if(counter1 > ticksPerDamage) {
                counter1 = 0;
                pc.GetComponent<PlayerHealth> ().health1--;
            }
            if (counter2 > ticksPerDamage) {
                counter2 = 0;
                pc.GetComponent<PlayerHealth> ().health2--;
            }
        }
    }
}
