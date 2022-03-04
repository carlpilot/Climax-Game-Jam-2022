using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public float restartDelay = 2.0f;

    public void Trigger () {
        Invoke ("RestartScene", restartDelay);
    }

    void RestartScene () {
        // Reload current scene
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
    }
}
