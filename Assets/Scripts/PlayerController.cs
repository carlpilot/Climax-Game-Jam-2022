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

    public float horizontalDeconflictionDistance = 0.6f; // don't move horizontally when obstacle this close or closer

    public LayerMask raycastLayerMask;

    public float jumpVelocity = 6.0f; // initial, m/s upwards
    public float jumpMaxGroundDist = 1.25f; // Maximum distance from player origin to ground to jump

    public float groundFriction = 1.0f; // Unused for now

    public float restoringMoment = 1.0f; // Torque keeping player upright

    private void Start () {

    }

    private void FixedUpdate () {

        // Horizontal motion
        Vector3 ha1 = Vector3.right * horizontal (1) * Mathf.Clamp01 (maxSpeed - P1.velocity.magnitude) * acceleration;
        Vector3 ha2 = Vector3.right * horizontal (2) * Mathf.Clamp01 (maxSpeed - P2.velocity.magnitude) * acceleration;
        if(clearLeft(P1) && ha1.x < 0 || clearRight(P1) && ha1.x > 0)
            P1.velocity += ha1 * Time.fixedDeltaTime;
        if (clearLeft (P2) && ha2.x < 0 || clearRight (P2) && ha2.x > 0)
            P2.velocity += ha2 * Time.fixedDeltaTime;

        // Vertical motion
        if (Input.GetKey (Key_P1_Jump) && canJump (P1))
            P1.velocity += Vector3.up * (jumpVelocity - P1.velocity.y);
        if (Input.GetKey (Key_P2_Jump) && canJump (P2))
            P2.velocity += Vector3.up * (jumpVelocity - P2.velocity.y);

        // Righting moment
        P1.AddTorque (restoringMoment * P1.mass * Vector3.forward * Vector3.SignedAngle (P1.transform.up, Vector3.up, Vector3.forward));
        P2.AddTorque (restoringMoment * P2.mass * Vector3.forward * Vector3.SignedAngle (P2.transform.up, Vector3.up, Vector3.forward));

        // Ground friction
        float modfric1 = groundFriction * (Mathf.Abs(ha1.x) > 0 ? 1 : 0);
        float modfric2 = groundFriction * (Mathf.Abs (ha2.x) > 0 ? 1 : 0);
    }

    bool canJump (Rigidbody g) {
        return Physics.Raycast (g.transform.position, -g.transform.up, jumpMaxGroundDist, raycastLayerMask);
    }

    bool clearLeft (Rigidbody g) {
        return !Physics.Raycast (g.transform.position, -g.transform.right, horizontalDeconflictionDistance, raycastLayerMask);
    }

    bool clearRight (Rigidbody g) {
        return !Physics.Raycast (g.transform.position, g.transform.right, horizontalDeconflictionDistance, raycastLayerMask);
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
