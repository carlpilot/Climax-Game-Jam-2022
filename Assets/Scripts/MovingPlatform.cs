using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    Vector3 lastPos;
    Vector3 velocity;

    private void Awake () {
        lastPos = transform.position;
    }

    void FixedUpdate () {
        velocity = (transform.position - lastPos) / Time.fixedDeltaTime;
        lastPos = transform.position;
    }

    public Vector3 Velocity { get => velocity; }
}
