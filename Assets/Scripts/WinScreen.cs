using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{

    public RectTransform scrollContent;
    public GameObject leaderboardItemPrefab;
    public LeaderboardItem thisScore;

    int level { get => SceneManager.GetActiveScene().buildIndex; }

    public void LoadLeaderboard (string highscores) {
        print (highscores);
        string[] scores = highscores.Split ('\n');
        for(int i = 0; i < scores.Length; i++) {
            if (scores[i].Length < 2) continue;
            string[] vals = scores[i].Split ('|');
            GameObject newLBI = Instantiate (leaderboardItemPrefab);
            newLBI.transform.parent = scrollContent;
            LeaderboardItem lbi = newLBI.GetComponent<LeaderboardItem> ();
            lbi.SetRank (i + 1); lbi.SetUsername (vals[0]); lbi.SetTime (int.Parse (vals[2]) / 1000.0f);
        }
    }

    public void Trigger () {

    }

    public void NextLevel () => SceneManager.LoadScene (level + 1);

    public void MainMenu () => SceneManager.LoadScene (0);
}
