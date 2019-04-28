using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpComponent : MonoBehaviour
{
    float lastJumpTime = 0;

    [SerializeField]
    float jumpTimeout = .1f;

    bool JumpPressed = false;
    bool JumpReleasedForJumpheight = true;
    bool JumpReleasedForJumpStart = true;

    bool disableMovement;

    [SerializeField]
    int maxGravDisableFrames = 5;

    [SerializeField]
    int minGravDisableFrames = 2;

    int currentGraveDisableFrames;
    
    [SerializeField]
    float jumpForce = 5;

    [SerializeField]
    float jumpHoldForce = 5;

    GroundedComponenet groundedComponenet;

    // Start is called before the first frame update
    void Start()
    {
        groundedComponenet = GetComponent<GroundedComponenet>();
    }

    private bool CanJump() {
        return (groundedComponenet.Grounded) && Time.time - lastJumpTime > jumpTimeout && JumpReleasedForJumpStart;
    }
    
    public void KillPlayer() {
        disableMovement = true;
    }

    public void RespawnPlayer() {
        disableMovement = false;
    }

    private void Update() {
        if (disableMovement) {
            return;
        }

        if (Input.GetAxis("Jump") == 0) {
            JumpReleasedForJumpheight = true;
            if (groundedComponenet.Grounded) {
                JumpReleasedForJumpStart = true;
            }
        }

        if (groundedComponenet.Grounded && JumpReleasedForJumpheight) {
            if (Input.GetAxis("Jump") != 0 && CanJump()) {
                JumpPressed = true;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (disableMovement) {
            return;
        }

        if (JumpPressed) {
            Jump();
        }
        if (!groundedComponenet.Grounded) {
            HandleBonusJump();
        }
    }

    void Jump() {
        JumpReleasedForJumpheight = false;
        JumpPressed = false;
        JumpReleasedForJumpStart = false;
        Vector2 oldVel = GetComponentInParent<Rigidbody2D>().velocity;
        GetComponentInParent<Rigidbody2D>().velocity = new Vector2(oldVel.x, 0);
        GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0, 1) * jumpForce);
        lastJumpTime = Time.time;
        currentGraveDisableFrames = 0;
    }

    void HandleBonusJump() {
        if ((!JumpReleasedForJumpheight && currentGraveDisableFrames <= maxGravDisableFrames) || currentGraveDisableFrames < minGravDisableFrames) {
            currentGraveDisableFrames++;
            GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0, 1) * jumpHoldForce);
        }
    }
}
