using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Player_Trampoline : Controller_Player
{
    public float trampolineForce;

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(new Vector3(0, jumpForce * trampolineForce, 0), ForceMode.Impulse);
        }
        base.OnCollisionEnter(collision);
    }
}
