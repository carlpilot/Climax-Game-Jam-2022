using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    #region KeyVariables
    [Header ("Player 1 Controls")]
    public KeyCode Key_P1_Jump = KeyCode.W;
    public KeyCode Key_P1_Left = KeyCode.A;
    public KeyCode Key_P1_Right = KeyCode.D;
    public KeyCode Key_P1_Crouch = KeyCode.S;
    [Header ("Player 2 Controls")]
    public KeyCode Key_P2_Jump = KeyCode.I;
    public KeyCode Key_P2_Left = KeyCode.J;
    public KeyCode Key_P2_Right = KeyCode.L;
    public KeyCode Key_P2_Crouch = KeyCode.K;
    #endregion

    public Rigidbody P1, P2;

    public float maxSpeed = 1.0f; // m/s
    public float acceleration = 1.0f; // m/s^2

    public float jumpVelocity = 1.0f; // initial, m/s upwards
    public float jumpMaxGroundDist = 1.5f; // Maximum distance from player origin to ground to jump

    public float groundFriction = 1.0f; // Unused for now

    private void Start () {

    }

    private void Update () {
        Vector3 ha1 = Vector3.right * horizontal (1) * Mathf.Clamp01 (maxSpeed - P1.velocity.magnitude) * acceleration;
        Vector3 ha2 = Vector3.right * horizontal (2) * Mathf.Clamp01 (maxSpeed - P2.velocity.magnitude) * acceleration;

        P1.AddForce (ha1 * P1.mass);
        P2.AddForce (ha2 * P2.mass);

        if (Input.GetKeyDown (Key_P1_Jump) && canJump (P1))
            P1.velocity += Vector3.up * jumpVelocity;
        if (Input.GetKeyDown (Key_P2_Jump) && canJump (P2))
            P2.velocity += Vector3.up * jumpVelocity;
    }

    private bool canJump (Rigidbody g) {
        return Physics.Raycast (g.transform.position, -g.transform.up, jumpMaxGroundDist);
    }

    // 1 = right, -1 = left, 0 = none
    float horizontal (int player) {
        if(player == 1) {
            return maxSpeed * ((Input.GetKey (Key_P1_Left) ? -1f : 0f) + (Input.GetKey (Key_P1_Right) ? 1f : 0f));
        } else {
            return maxSpeed * ((Input.GetKey (Key_P2_Left) ? -1f : 0f) + (Input.GetKey (Key_P2_Right) ? 1f : 0f));
        }
    }
}
