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

    private float maxMagn;

    private void Start()
    {
        maxMagn = Vector3.Magnitude((Vector3.up + Vector3.right) * jumpStrength);
    }

    void Update()
    {
        //If the WASD keys are pressed, jump in that direction.
        if (Input.GetKey(KeyCode.W))
        {
            ApplyJumpInput(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            ApplyJumpInput(-Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyJumpInput(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            ApplyJumpInput(-Vector3.right);
        }
    }

    /// <summary>
    /// Once a key is pressed, jumps in the given direction by setting velocity.
    /// </summary>
    /// <param name="forceDir">The direction to jump in.</param>
    private void ApplyJumpInput(Vector3 forceDir)
    {
        if (IsGrounded())
        {
            //Set the velocities to zero, so we can add velocity onto a clean slate.
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            //Set the force vector and make sure it's clamped to the right magnitude.
            Vector3 jumpForce = (forceDir + Vector3.up) * jumpStrength;
            rb.velocity = Vector3.ClampMagnitude(jumpForce, maxMagn);

            //Finally, start rotating in the direction of the jump.
            //Angular velocity uses the vector as the axis and length as the strength. Thus cross is needed to get the
            //perpendicular vector to forceDir.
            Vector3 rotDir = -Vector3.Cross(forceDir, Vector3.up).normalized;
            rb.angularVelocity = rotDir * jumpStrength;
        }
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
