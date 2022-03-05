using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class LevelProfile : MonoBehaviour
{
    public LeaderboardItem[] lbItems;
    public int level;
    public TMP_Text levelTitle;

    private void Awake () {
        levelTitle.text = "Level " + level;
        StartCoroutine(LoadLeaderboard());
    }

    IEnumerator LoadLeaderboard () {
        UnityWebRequest uwr = UnityWebRequest.Get ("http://dreamlo.com/lb/" + SecretCode.Public (level) + "/pipe-seconds-asc");
        yield return uwr.SendWebRequest ();
        string[] entries = uwr.downloadHandler.text.Split ('\n');
        for (int i = 0; i < lbItems.Length; i++) {
            if(entries.Length <= i || entries[i].Length < 2) {
                lbItems[i].SetRank (-1); lbItems[i].SetUsername ("-"); lbItems[i].SetTime (5999.999f);
            } else {
                string[] vals = entries[i].Split ('|');
                lbItems[i].SetRank (i + 1); lbItems[i].SetUsername (vals[0]); lbItems[i].SetTime (int.Parse (vals[2]) / 1000.0f);
            }
        }
    }

    public void GoToLevel () {
        print ("Going to level " + level);
        FindObjectOfType<LevelSelect> ().SwitchScene (level);
    }
}
