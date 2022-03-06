using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour
{
    public RectTransform TimerBackground;
    public RectTransform TimerDropdownArrow;

    bool isExtended = true;
    bool counting = false;

    float count = 0;
    float startTime = 0;

    private void Start () {
        if (!PlayerPrefs.HasKey ("TimerExtended")) PlayerPrefs.SetInt ("TimerExtended", 1);
        if (PlayerPrefs.GetInt ("TimerExtended") == 0) ToggleTimerExtended (); // retract timer if the playerprefs say so
    }

    private void Update () {
        if(counting) count = Time.timeSinceLevelLoad - startTime;
        GetComponent<Text> ().text = TimeFormat (count);
    }

    public void StartTime () {
        counting = true;
        startTime = Time.timeSinceLevelLoad;
    }

    public void StopTime() => counting = false;

    public bool isCounting { get => counting; }

    public void ToggleTimerExtended () => ToggleTimerExtended (true);

    public void ToggleTimerExtended (bool save) {
        isExtended = !isExtended;
        if(save) PlayerPrefs.SetInt ("TimerExtended", isExtended ? 1 : 0);
        int sign = isExtended ? 1 : -1;
        TimerBackground.anchoredPosition += -Vector2.up * TimerBackground.rect.height * sign;
        TimerDropdownArrow.Rotate (0, 0, 180);
    }

    public bool extended { get => isExtended; }

    public float time { get => count; }

    public static string TimeFormat (float t) {
        int minutes = Mathf.FloorToInt (t / 60.0f);
        int seconds = Mathf.FloorToInt (t - 60.0f * minutes);
        int hundredths = Mathf.FloorToInt ((t - 60.0f * minutes - (float) seconds) * 100f);
        return string.Format ("{0:D2}:{1:D2}.{2:D2}", minutes, seconds, hundredths);
    }
}
