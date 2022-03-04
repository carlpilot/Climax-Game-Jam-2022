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

    public float maxSpeed = 3.0f; // m/s
    public float acceleration = 5.0f; // m/s^2

    public float jumpVelocity = 6.0f; // initial, m/s upwards
    public float jumpMaxGroundDist = 1.25f; // Maximum distance from player origin to ground to jump

    public float groundFriction = 1.0f; // Unused for now

    public bool j1, j2;

    private void Start () {

    }

    private void FixedUpdate () {
        j1 = canJump (P1); j2 = canJump (P2);

        Vector3 ha1 = Vector3.right * horizontal (1) * Mathf.Clamp01 (maxSpeed - P1.velocity.magnitude) * acceleration;
        Vector3 ha2 = Vector3.right * horizontal (2) * Mathf.Clamp01 (maxSpeed - P2.velocity.magnitude) * acceleration;

        if (Input.GetKey (Key_P1_Jump) && canJump (P1))
            P1.velocity += Vector3.up * (jumpVelocity - P1.velocity.y);
        if (Input.GetKey (Key_P2_Jump) && canJump (P2))
            P2.velocity += Vector3.up * (jumpVelocity - P2.velocity.y);

        //P1.AddForce (ha1 * P1.mass);
        //P2.AddForce (ha2 * P2.mass);
        P1.velocity += ha1 * Time.fixedDeltaTime;
        P2.velocity += ha2 * Time.fixedDeltaTime;
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
