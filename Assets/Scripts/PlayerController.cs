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
    [Header ("General Controls")]
    public KeyCode Key_Switch_Powerups = KeyCode.Space;

    [Header ("Players")]
    public Rigidbody P1;
    public Rigidbody P2;
    public Transform PlayerModel1, PlayerModel2;
    int facing1 = 1, facing2 = 1; // 1 = right, -1 = left
    float startface1, startface2;
    public float rotationToFace = 180; // how much to rotate on change of face

    [Header("Speed and Acceleration")]
    public float maxSpeed = 3.0f; // m/s
    public float acceleration = 5.0f; // m/s^2

    [Header("Jumping and Collisions")]
    public float horizontalDeconflictionDistance = 0.6f; // don't move horizontally when obstacle this close or closer
    public LayerMask raycastLayerMask;
    public float jumpVelocity = 6.0f; // initial, m/s upwards
    public float jumpMaxGroundDist = 1.25f; // Maximum distance from player origin to ground to jump

    [Header("Friction and Righting")]
    public float groundFriction = 1.0f;
    public float restoringMoment = 1.0f; // Torque keeping player upright
    public float basicRestoringMoment = 0.1f; // always on

    [Header ("Crouching")]
    public float crouchFrictionFactor = 10.0f;
    public float crouchGravityFactor = 1.5f;

    [Header ("Powerup Effects")]
    public float SpeedBoostP1 = 1.0f;
    public float SpeedBoostP2 = 1.0f;
    public float JumpBoostP1 = 1.0f;
    public float JumpBoostP2 = 1.0f;

    private void Start () {
        startface1 = PlayerModel1.localEulerAngles.y;
        startface2 = PlayerModel2.localEulerAngles.y;
    }

    private void FixedUpdate () {

        // Horizontal motion
        Vector3 ha1 = Vector3.right * horizontal (1) * Mathf.Clamp01 (maxSpeed * SpeedBoostP1 - P1.velocity.magnitude) * acceleration;
        Vector3 ha2 = Vector3.right * horizontal (2) * Mathf.Clamp01 (maxSpeed * SpeedBoostP2 - P2.velocity.magnitude) * acceleration;
        if (clearLeft (P1) && ha1.x < 0 || clearRight (P1) && ha1.x > 0) {
            P1.velocity += ha1 * Time.fixedDeltaTime;
            facing1 = ha1.x > 0 ? 1 : -1;
        }
        if (clearLeft (P2) && ha2.x < 0 || clearRight (P2) && ha2.x > 0) {
            P2.velocity += ha2 * Time.fixedDeltaTime;
            facing2 = ha2.x > 0 ? 1 : -1;
        }

        // Vertical motion
        if (Input.GetKey (Key_P1_Jump) && canJump (P1))
            P1.velocity += Vector3.up * (jumpVelocity - P1.velocity.y) * JumpBoostP1;
        if (Input.GetKey (Key_P2_Jump) && canJump (P2))
            P2.velocity += Vector3.up * (jumpVelocity - P2.velocity.y) * JumpBoostP2;

        // Crouch gravity
        if (Input.GetKey (Key_P1_Crouch))
            P1.AddForce (Vector3.up * P1.mass * -9.81f * (crouchGravityFactor - 1.0f));
        if (Input.GetKey (Key_P2_Crouch))
            P2.AddForce (Vector3.up * P2.mass * -9.81f * (crouchGravityFactor - 1.0f));

        // Righting moment
        if (groundContact(P1))
            P1.AddTorque (restoringMoment * P1.mass * Vector3.forward * Vector3.SignedAngle (P1.transform.up, Vector3.up, Vector3.forward));
        if (groundContact (P2))
            P2.AddTorque (restoringMoment * P2.mass * Vector3.forward * Vector3.SignedAngle (P2.transform.up, Vector3.up, Vector3.forward));
        P1.AddTorque (basicRestoringMoment * P1.mass * Vector3.forward * Vector3.SignedAngle (P1.transform.up, Vector3.up, Vector3.forward));
        P2.AddTorque (basicRestoringMoment * P2.mass * Vector3.forward * Vector3.SignedAngle (P2.transform.up, Vector3.up, Vector3.forward));

        // Ground friction
        float modfric1 = groundFriction * (Mathf.Abs (ha1.x) > 0.05f ? 0 : 1) * (Input.GetKey (Key_P1_Crouch) ? crouchFrictionFactor : 1.0f);
        float modfric2 = groundFriction * (Mathf.Abs (ha2.x) > 0.05f ? 0 : 1) * (Input.GetKey (Key_P2_Crouch) ? crouchFrictionFactor : 1.0f);
        P1.GetComponent<Collider> ().material.staticFriction = modfric1;
        P1.GetComponent<Collider> ().material.dynamicFriction = modfric1;
        P2.GetComponent<Collider> ().material.staticFriction = modfric2;
        P2.GetComponent<Collider> ().material.dynamicFriction = modfric2;

        // Override rotation direction for high horizontal velocities
        if (P1.velocity.magnitude > maxSpeed / 2f)
            facing1 = P1.velocity.x > 0 ? 1 : -1;
        if (P2.velocity.magnitude > maxSpeed / 2f)
            facing2 = P2.velocity.x > 0 ? 1 : -1;
        if (P1.velocity.magnitude < 0.1f) facing1 = 2;
        if (P2.velocity.magnitude < 0.1f) facing2 = 2;

        // Rotate to face
        Quaternion targetRotation1 = Quaternion.Euler (Vector3.up * (startface1 + (1 - facing1) / 2f * rotationToFace));
        Quaternion targetRotation2 = Quaternion.Euler (Vector3.up * (startface2 + (1 - facing2) / 2f * rotationToFace));
        Quaternion p1rot = Quaternion.Slerp (PlayerModel1.localRotation, targetRotation1, 0.1f);
        Quaternion p2rot = Quaternion.Slerp (PlayerModel2.localRotation, targetRotation2, 0.1f);
        PlayerModel1.localRotation = p1rot;
        PlayerModel2.localRotation = p2rot;

        // Powerup switching
        if(Input.GetKeyDown(Key_Switch_Powerups)) {
            float temp_sb1 = SpeedBoostP1;
            float temp_jb1 = JumpBoostP1;
            SpeedBoostP1 = SpeedBoostP2;
            JumpBoostP1 = JumpBoostP2;
            SpeedBoostP2 = temp_sb1;
            JumpBoostP2 = temp_jb1;
        }
    }

    bool canJump (Rigidbody g) {
        return Physics.Raycast (g.transform.position, -g.transform.up, jumpMaxGroundDist, raycastLayerMask);
    }

    bool groundContact (Rigidbody g) {
        return Physics.Raycast (g.transform.position, Vector3.down, jumpMaxGroundDist, raycastLayerMask);
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
            return maxSpeed * ((Input.GetKey (Key_P1_Left) ? -1f : 0f) + (Input.GetKey (Key_P1_Right) ? 1f : 0f)) * SpeedBoostP1;
        } else {
            return maxSpeed * ((Input.GetKey (Key_P2_Left) ? -1f : 0f) + (Input.GetKey (Key_P2_Right) ? 1f : 0f)) * SpeedBoostP2;
        }
    }
}
