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
        //If grounded, unfreeze rotation, if it was frozen.
        if (IsGrounded())
        {
            rb.freezeRotation = false;
        }

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
    /// Once a key is pressed, jumps in the given direction using force, or applies force in the given direction while
    /// aerial.
    /// </summary>
    /// <param name="forceDir">The direction to add force in.</param>
    private void ApplyJumpInput(Vector3 forceDir)
    {
        if (IsGrounded())
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.freezeRotation = true;

            Vector3 jumpForce = (forceDir + Vector3.up) * jumpStrength;
            rb.velocity = Vector3.ClampMagnitude(jumpForce, maxMagn);
        }
    }

    private bool IsGrounded()
    {
        //Thanks to http://answers.unity.com/answers/196395/view.html for this!
        return Physics.Raycast(transform.position, -Vector3.up, coll.bounds.extents.y + 0.001f);
    }

    //private void OnGUI()
    //{
    //    GUI.Box(new Rect(10, 10, 175, 175), "Velocity: " + rb.velocity + "\nAng. Velocity: " + rb.angularVelocity +
    //        "\nVel Magnitude: " + rb.velocity.magnitude + "\nAngVel Magnitude: " + rb.angularVelocity.magnitude);
    //}
}
