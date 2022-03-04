using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerHealth health;
    public Transform Player1, Player2;

    public GameObject gameOverScreen;

    public float voidCutoff = -10f; // player dies below this level

    bool hasLost = false;

    private void Update () {
        if(health.health1 <= 0 || health.health2 <= 0 || Player1.position.y < voidCutoff || Player2.position.y < voidCutoff) {
            Lose ();
        }
    }

    public void Lose () {
        if (hasLost) return; // don't lose twice
        print ("Died");
        hasLost = true;
        gameOverScreen.SetActive (true);
        gameOverScreen.GetComponent<GameOverScreen> ().Trigger ();
    }
}
