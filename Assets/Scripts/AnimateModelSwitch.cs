using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateModelSwitch : MonoBehaviour
{
    public GameObject[] models;
    public float timeBetween = 1.2f;

    int currentState = 0;
    bool countUp = true;

    private void Start () {
        InvokeRepeating ("SwitchModel", timeBetween, timeBetween);
    }

    void SwitchModel () {
        if (countUp) {
            currentState++;
            if (currentState == models.Length - 1) countUp = false;
        } else {
            currentState--;
            if (currentState == 0) countUp = true;
        }
        for(int i = 0; i < models.Length; i++)
            models[i].SetActive (i == currentState);
    }
}
