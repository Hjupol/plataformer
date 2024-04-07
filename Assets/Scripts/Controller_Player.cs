using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Player : MonoBehaviour
{
    public float jumpForce = 10;

    public float speed = 5;

    public int playerNumber;

    public Rigidbody rb;

    private BoxCollider col;

    public LayerMask floor;

    internal RaycastHit leftHit,rightHit,downHit;

    public float distanceRay,downDistanceRay;

    private bool canMoveLeft, canMoveRight,canJump;
    internal bool onFloor;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        rb.constraints = RigidbodyConstraints.FreezePositionX| RigidbodyConstraints.FreezePositionZ|RigidbodyConstraints.FreezeRotation;
    }

    public virtual void FixedUpdate()
    {
        if (GameManager.actualPlayer == playerNumber)
        {
            Movement();
        }
    }

    private void Update()
    {
        if (GameManager.actualPlayer == playerNumber)
        {
            Jump();
            if (SomethingLeft())
            {
                canMoveLeft = false;
            }
            else
            {
                canMoveLeft = true;
            }
            if (SomethingRight())
            {
                canMoveRight = false;
            }
            else
            {
                canMoveRight = true;
            }

            if (IsOnSomething())
            {
                canJump = true;
            }
            else
            {
                canJump = false;
            }

        }
        else
        {
            if (onFloor)
            {
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }
            else
            {
                if (IsOnSomething())
                {
                    if (downHit.collider.gameObject.CompareTag("Player"))
                    {
                        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                    }
                }
            }
        }
    }




    public virtual bool IsOnSomething()
    {
        return Physics.BoxCast(transform.position, new Vector3(transform.localScale.x * 0.9f, transform.localScale.y/3,transform.localScale.z*0.9f), Vector3.down, out downHit, Quaternion.identity, downDistanceRay);
    }

    public virtual bool SomethingRight()
    {
        Ray landingRay = new Ray(new Vector3(transform.position.x,transform.position.y-(transform.localScale.y / 2.2f),transform.position.z), Vector3.right);
        Debug.DrawRay(landingRay.origin, landingRay.direction, Color.green);
        return Physics.Raycast(landingRay, out rightHit, transform.localScale.x/1.8f);
    }

    public virtual bool SomethingLeft()
    {
        Ray landingRay = new Ray(new Vector3(transform.position.x, transform.position.y - (transform.localScale.y/2.2f), transform.position.z), Vector3.left);
        Debug.DrawRay(landingRay.origin, landingRay.direction, Color.green);
        return Physics.Raycast(landingRay, out leftHit, transform.localScale.x/1.8f);
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.A) && canMoveLeft)
        {
                rb.velocity = new Vector3(1 * -speed , rb.velocity.y, 0);
        }
        else if (Input.GetKey(KeyCode.D) && canMoveRight)
        {
                rb.velocity = new Vector3(1 * speed, rb.velocity.y, 0);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        //if (!canMoveLeft)
        //    rb.velocity = new Vector3(0, rb.velocity.y, 0);
        //if (!canMoveRight)
        //    rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }

    public virtual void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (canJump)
            {
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }
        }
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            Destroy(this.gameObject);
            GameManager.gameOver = true;
        }
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = false;
        }
    }
}
