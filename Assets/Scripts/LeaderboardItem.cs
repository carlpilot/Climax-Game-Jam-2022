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

    public void SetTime (float t) => time.text = Timer.TimeFormat (t);
}
