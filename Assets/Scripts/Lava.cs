using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    PlayerController pc;
    PlayerHealth he;

    int p1Counter = 0;
    int p2Counter = 0;

    public int ticksPerBurn = 50/6;

    private void Start () {
        pc = FindObjectOfType<PlayerController> ();
        he = FindObjectOfType<PlayerHealth> ();
    }

    private void OnTriggerStay (Collider other) {
        if (other.GetComponent<Rigidbody> () == null) return;
        if (other.GetComponent<Rigidbody> () == pc.P1 || other.GetComponent<Rigidbody> () == pc.P2) {
            // Player in the lava
            if (other.GetComponent<Rigidbody> () == pc.P1){
              p1Counter ++;
              if (p1Counter >= ticksPerBurn) {
                  p1Counter = 0;
                  he.health1--;
                  if (he.health1 == 0) {
                      pc.burn_P1();
                  }
              }
            }
            if (other.GetComponent<Rigidbody> () == pc.P2){
              p2Counter ++;
              if (p2Counter >= ticksPerBurn) {
                p2Counter = 0;
                  he.health2--;
                  if (he.health2 == 0) {
                      pc.burn_P2();
                  }
              }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<Rigidbody> () == null) return;
        if (other.GetComponent<Rigidbody> () == pc.P1) {
            pc.toggle_burn(1, false);
        } else if (other.GetComponent<Rigidbody>() == pc.P2) {
            pc.toggle_burn(2, false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Rigidbody> () == null) return;
        if (other.GetComponent<Rigidbody> () == pc.P1) {
            pc.toggle_burn(1, true);
        } else if (other.GetComponent<Rigidbody>() == pc.P2) {
            pc.toggle_burn(2, true);
        }
    }
}
