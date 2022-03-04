using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header ("Image References")]
    public Image RH1;
    public Image RH2, RH3, BH1, BH2, BH3;
    [Header ("Sprites")]
    public Sprite empty;
    public Sprite red, blue, halfRed, halfBlue;

    public void SetHealth (int healthR, int healthB) {
        RH1.sprite = healthR > 1 ? red : healthR > 0 ? halfRed : empty;
        RH2.sprite = healthR > 3 ? red : healthR > 2 ? halfRed : empty;
        RH3.sprite = healthR > 5 ? red : healthR > 4 ? halfRed : empty;
        BH1.sprite = healthB > 1 ? blue : healthB > 0 ? halfBlue : empty;
        BH2.sprite = healthB > 3 ? blue : healthB > 2 ? halfBlue : empty;
        BH3.sprite = healthB > 5 ? blue : healthB > 4 ? halfBlue : empty;
    }
}
