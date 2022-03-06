using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Transform[] platforms;

    public float SpeedRPM;

    private void FixedUpdate () {
        transform.Rotate (0, 0, SpeedRPM / 60f * 360f * Time.fixedDeltaTime);
        foreach (Transform p in platforms) p.rotation = Quaternion.identity;
    }
}
