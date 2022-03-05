using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{

    public RectTransform scrollContent;
    public GameObject leaderboardItemPrefab;
    public LeaderboardItem thisScore;

    public void LoadLeaderboard (string highscores) {
        string[] scores = highscores.Split ('\n');
        string u = PlayerPrefs.GetString ("Username");
        thisScore.SetRank (-1);
        for (int i = 0; i < scores.Length; i++) {
            if (scores[i].Length < 2) continue;
            string[] vals = scores[i].Split ('|');
            GameObject newLBI = Instantiate (leaderboardItemPrefab);
            newLBI.transform.SetParent(scrollContent, false);
            LeaderboardItem lbi = newLBI.GetComponent<LeaderboardItem> ();
            lbi.SetRank (i + 1); lbi.SetUsername (vals[0]); lbi.SetTime (int.Parse (vals[2]) / 1000.0f);
            if (vals[0] == u) thisScore.SetRank (i + 1);
        }
    }

    public void Trigger () {
        SetThisScore ();
    }

    void SetThisScore () {
        thisScore.SetTime (FindObjectOfType<Timer> ().time);
        thisScore.SetUsername (PlayerPrefs.GetString ("Username"));
    }
}
