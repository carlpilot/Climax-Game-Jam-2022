using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerHealth health;
    public Transform Player1, Player2;
    public MeshRenderer Player1Mesh, Player2Mesh, Player1Torus, Player2Torus;

    public GameObject winScreen;
    public GameObject gameOverScreen;

    public float voidCutoff = -10f; // player dies below this level

    bool hasEnded = false;

    private void Start () {
        Color colour1 = new Color (PlayerPrefs.GetFloat ("R1"), PlayerPrefs.GetFloat ("G1"), PlayerPrefs.GetFloat ("B1"));
        Color colour2 = new Color (PlayerPrefs.GetFloat ("R2"), PlayerPrefs.GetFloat ("G2"), PlayerPrefs.GetFloat ("B2"));
        Player1Mesh.material.color = colour1;
        Player2Mesh.material.color = colour2;
        Player1Torus.material.color = colour1 / 2;
        Player2Torus.material.color = colour2 / 2;
    }

    private void Update () {
        if( health.health1 <= 0 || health.health2 <= 0 ||
            Player1.position.y < voidCutoff || Player2.position.y < voidCutoff) {
            Lose ();
        }
    }

    public void Win () {
        if (hasEnded) return;
        hasEnded = true;
        winScreen.SetActive (true);
        winScreen.GetComponent<WinScreen> ().Trigger ();
    }

    public void Lose () {
        if (hasEnded) return; // don't lose twice
        print ("Died");
        hasEnded = true;
        gameOverScreen.SetActive (true);
        gameOverScreen.GetComponent<GameOverScreen> ().Trigger ();
        Timer t = FindObjectOfType<Timer> ();
        t.StopTime ();
        if(!t.extended) t.ToggleTimerExtended (false);
    }
}
