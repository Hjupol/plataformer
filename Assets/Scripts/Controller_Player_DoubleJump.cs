using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Player_DoubleJump : Controller_Player
{
    private int jumpCounter=0;

    public override bool IsOnSomething()
    {
        return Physics.BoxCast(transform.position, new Vector3(transform.localScale.x * 0.9f, transform.localScale.y / 3, transform.localScale.z * 0.9f), Vector3.down, out downHit, Quaternion.identity, downDistanceRay);
    }

    public override void Jump()
    {
        if (IsOnSomething())
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                jumpCounter = 1;
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (jumpCounter > 0)
                {
                    rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                    jumpCounter--;
                }
            }
        }
    }
}
