using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Player_Floating : Controller_Player
{
    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = true;
        }
        //This makes the player invulnerable to water.
    }
}
