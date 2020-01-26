using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Collider coll;

    public float jumpStrength = 5;
    public float secondsToCharge = 0.5f;

    private float maxMagn;
    private int chargeLevel = 1;
    private float chargeTimer = 0;

    private void Start()
    {
        maxMagn = Vector3.Magnitude((Vector3.up + Vector3.right) * jumpStrength);
    }

    void Update()
    {
        //Only do the following when on the ground.
        if (IsGrounded())
        {
            //If any of the WASD keys are held, start charging a jump.
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                ChargeJump();
            }

            //If the WASD keys are released, jump in the corresponding direction.
            else if (Input.GetKeyUp(KeyCode.W))
            {
                ApplyJumpInput(Vector3.forward);
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                ApplyJumpInput(-Vector3.forward);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                ApplyJumpInput(Vector3.right);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                ApplyJumpInput(-Vector3.right);
            }
        }
    }

    /// <summary>
    /// Begin charging a jump. Moves up in stages.
    /// </summary>
    private void ChargeJump()
    {
        //Increment the charge timer when this function is called.
        chargeTimer += Time.deltaTime;

        //If not at max charge and the timer's been ticking long enough, reset the timer and move up a charge level.
        if (chargeLevel < 3 && chargeTimer >= secondsToCharge)
        {
            chargeTimer = 0;
            chargeLevel++;
        }
    }

    /// <summary>
    /// Jumps in the given direction by setting velocity.
    /// </summary>
    /// <param name="forceDir">The direction to jump in.</param>
    private void ApplyJumpInput(Vector3 forceDir)
    {
        //Set the velocities to zero, so we can add velocity onto a clean slate.
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        //Set the force vector.
        Vector3 jumpForce = forceDir + Vector3.up;
        rb.velocity = Vector3.ClampMagnitude(jumpForce, maxMagn);

        //Finally, start rotating in the direction of the jump.
        //Angular velocity uses the vector as the axis and length as the strength. Thus cross is needed to get the
        //perpendicular vector to forceDir.
        Vector3 rotDir = -Vector3.Cross(forceDir, Vector3.up).normalized;
        rb.angularVelocity = rotDir * jumpStrength;
    }

    private bool IsGrounded()
    {
        //Thanks to http://answers.unity.com/answers/196395/view.html for this!
        //Cast a ray downward to the extent of the object, plus a tiny bit of leeway.
        return Physics.Raycast(transform.position, -Vector3.up, coll.bounds.extents.y + 0.001f);
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 15;
        style.normal.textColor = Color.white;
        GUI.Box
            (
                new Rect(10, 10, 175, 175),
                "Velocity: " + rb.velocity
                    + "\nAng. Velocity: " + rb.angularVelocity
                    + "\nVel Magnitude: " + rb.velocity.magnitude
                    + "\nAng. Vel. Magnitude: " + rb.angularVelocity.magnitude,
                style
            );
    }
}
