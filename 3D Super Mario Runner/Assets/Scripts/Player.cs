using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public float acceleration = 2.5f;
    public float movementSpeed = 4f;
    public float jumpingSpeed = 6f;
    public float jumpDuration = 0.4f;
    public float verticalWallJumpingSpeed = 5f;
    public float horizontalWallJumpingSpeed = 3.5f;

    public Action onCollectCoin;

    private float speed = 0f;
    private float jumpingTimer = 0f;

    private bool canJump = false;
    private bool jumping = false;
    private bool canWallJump = false;
    private bool wallJumpLeft = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Accelerate the player
        speed += acceleration * Time.deltaTime;
        if(speed > movementSpeed)
        {
            speed = movementSpeed;
        }
        
        // Move horizontally
        GetComponent<Rigidbody>().velocity = new Vector3(speed, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);

        // Check for input
        bool pressingJumpButton = Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space);
        if (pressingJumpButton)
        {
            if(canJump)
            {
                jumping = true;
            }
        }
        // Make the player jump
        if (jumping)
        {
            jumpingTimer += Time.deltaTime;

            if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space) && jumpingTimer < jumpDuration)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, jumpingSpeed, GetComponent<Rigidbody>().velocity.z);
            }
        }
        // Make wall Jump
        if(canWallJump)
        {
            speed = 0;

            if(pressingJumpButton)
            {
                canWallJump = false;
                speed = wallJumpLeft ? -horizontalWallJumpingSpeed : horizontalWallJumpingSpeed;
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, verticalWallJumpingSpeed, GetComponent<Rigidbody>().velocity.z);
            }
        }
    }

    // Destroy Coin when touched
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetComponent<Coin>() != null)
        {
            Destroy(other.gameObject);
            onCollectCoin ();
        }
    }

    // Eligible to jump & Wall Jump verification
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("JumpingArea"))
        {
            canJump = true;
            jumping = false;
            jumpingTimer = 0f;
        }
        else if(other.CompareTag("WallJumpingArea"))
        {
            canWallJump = true;
            wallJumpLeft = transform.position.x < other.transform.position.x;
        }
    }

    // Exit wall Jump
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("WallJumpingArea"))
        {
            canWallJump = false;
        }
    }
}
