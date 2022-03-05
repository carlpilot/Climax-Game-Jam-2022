using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardItem : MonoBehaviour
{
    public Text rank, username, time;

    public void SetRank (int r) => rank.text = "" + r;
}
