using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardItem : MonoBehaviour
{
    public TMP_Text rank, username, time;

    public void SetRank (int r) => rank.text = "" + r;

    public void SetUsername (string u) => username.text = u;

    public void SetTime (float t) {
        int minutes = Mathf.FloorToInt (t / 60.0f);
        int seconds = Mathf.FloorToInt (t - 60.0f * minutes);
        int hundredths = Mathf.FloorToInt ((t - 60.0f * minutes - (float) seconds) * 100f);
        time.text = string.Format ("{0:D2}:{1:D2}.{2:D2}", minutes, seconds, hundredths);
        time.text = Timer.TimeFormat (t);
    }
}
