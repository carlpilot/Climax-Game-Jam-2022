using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health1 = 6;
    public int health2 = 6;

    HealthBar hb;

    private void Start () {
        hb = FindObjectOfType<HealthBar> ();
    }

    private void Update () {
        hb.SetHealth (health1, health2);
    }
}
