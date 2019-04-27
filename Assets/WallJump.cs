using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    [SerializeField]
    MovementComponent movementComponent;

    [SerializeField]
    TouchingWallDetection touchingWallDetection;

    bool canJump;
    float lastJumpTime = 0;

    [SerializeField]
    float jumpTimeout = .1f;

    [SerializeField]
    float hozVal = .2f;

    bool JumpPressed;
    bool JumpReleased;
    bool ReleasedSinceClinging;

    [SerializeField]
    int maxGravDisableFrames = 5;

    [SerializeField]
    int minGravDisableFrames = 2;
    int currentGraveDisableFrames;

    bool jumpingFromRight;

    [SerializeField]
    float jumpForce = 5;

    [SerializeField]
    float jumpHoldForce = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update() {
        if (movementComponent.WallClinging && ReleasedSinceClinging) {
            canJump = Time.time - lastJumpTime > jumpTimeout;
            if (Input.GetAxis("Jump") != 0 && canJump) {
                JumpPressed = true;
            }
        }

        if (Input.GetAxis("Jump") == 0) { 
            JumpReleased = true;
        }

        if (Input.GetAxis("Jump") == 0 && movementComponent.WallClinging) {
            ReleasedSinceClinging = true;
        } else {
            ReleasedSinceClinging = false;
        }

    }

    // Update is called once per frame
    void FixedUpdate() {
        if (JumpPressed) {
            Jump();
        }
        HandlebonusJump();
    }

    void Jump() {
        JumpReleased = false;
        JumpPressed = false;
        ReleasedSinceClinging = false;
        Vector2 oldVel = GetComponentInParent<Rigidbody2D>().velocity;
        GetComponentInParent<Rigidbody2D>().velocity = new Vector2(oldVel.x, 0);
        jumpingFromRight = touchingWallDetection.TouchingWallOnRight();
        float xComp = jumpingFromRight ? -1 : 1;
        GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(xComp* hozVal, 1) * jumpForce);

        lastJumpTime = Time.time;
        currentGraveDisableFrames = 0;
    }

    void HandlebonusJump() {
        if ((!JumpReleased && currentGraveDisableFrames <= maxGravDisableFrames) || currentGraveDisableFrames < minGravDisableFrames) {
            currentGraveDisableFrames++;
            float xComp = jumpingFromRight ? -1 : 1;
            GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(xComp * hozVal, 1) * jumpHoldForce);
        }
    }
}
