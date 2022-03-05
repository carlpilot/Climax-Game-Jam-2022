using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{

    int level { get => SceneManager.GetActiveScene().buildIndex; }

    public void LoadLeaderboard (string highscores) {
        // TODO
    }

    public void Trigger () {

    }

    public void NextLevel () => SceneManager.LoadScene (level + 1);

    public void MainMenu () => SceneManager.LoadScene (0);
}
