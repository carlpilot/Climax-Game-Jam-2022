using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour
{
    bool counting = true;

    float count = 0;

    private void Update () {
        if(counting) count = Time.timeSinceLevelLoad;
        int minutes = Mathf.FloorToInt(count / 60.0f);
        int seconds = Mathf.FloorToInt (count - 60.0f * minutes);
        int hundredths = Mathf.FloorToInt ((count - 60.0f * minutes - (float) seconds) * 100f);
        GetComponent<Text> ().text = string.Format ("{0:D2}:{1:D2}.{2:D2}", minutes, seconds, hundredths);
    }

    public void StopTime() {
        counting = false;
    }
}
