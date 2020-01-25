using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Collider coll;

    public float jumpStrength = 250;
    public float aerialMobility = 50;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ApplyJumpInput(Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ApplyJumpInput(-Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ApplyJumpInput(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            ApplyJumpInput(-Vector3.right);
        }
    }

    /// <summary>
    /// Once a key is pressed, jumps in the given direction using force, or applies force in the given direction while
    /// aerial.
    /// </summary>
    /// <param name="facingDir">The direction to add force in.</param>
    private void ApplyJumpInput(Vector3 facingDir)
    {
        transform.forward = facingDir;

        rb.AddForce((transform.TransformDirection(Vector3.forward) + Vector3.up) * jumpStrength);
    }

    private bool IsGrounded()
    {
        //Thanks to http://answers.unity.com/answers/196395/view.html for this!
        return Physics.Raycast(transform.position, -Vector3.up, coll.bounds.extents.y + 0.05f);
    }
}
