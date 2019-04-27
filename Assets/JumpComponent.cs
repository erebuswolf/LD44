﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpComponent : MonoBehaviour
{
    BoxCollider2D groundCollision;
    bool grounded;
    bool canJump;
    float lastJumpTime = 0;

    [SerializeField]
    float jumpTimeout = .1f;

    bool JumpPressed;
    bool JumpReleased;

    [SerializeField]
    int maxGravDisableFrames = 5;

    [SerializeField]
    int minGravDisableFrames = 2;

    int currentGraveDisableFrames;

    [SerializeField]
    float fallAccel = 1f;

    [SerializeField]
    float jumpForce = 5;

    [SerializeField]
    float jumpHoldForce = 5;

    List<Collider2D> trackedGroundObjects = new List<Collider2D>();

    // Start is called before the first frame update
    void Start()
    {
        groundCollision = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            grounded = true;
            trackedGroundObjects.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (trackedGroundObjects.Remove(collision) && trackedGroundObjects.Count == 0) {
            grounded = false;
        }
    }

    private void Update() {
        if (Input.GetAxis("Jump") == 0) {
            JumpReleased = true;
        }
        if (grounded && JumpReleased) {
            grounded = true;
            canJump = Time.time - lastJumpTime > jumpTimeout;
            if (Input.GetAxis("Jump") != 0) {
                JumpPressed = true;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (JumpPressed) {
            Jump();
        }
        if(!grounded) {
            Fall();
        }
    }

    void Jump() {
        JumpReleased = false;
        JumpPressed = false;
        Vector2 oldVel = GetComponentInParent<Rigidbody2D>().velocity;
        GetComponentInParent<Rigidbody2D>().velocity = new Vector2(oldVel.x, 0);
        GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0, 1) * jumpForce);
        lastJumpTime = Time.time;
        currentGraveDisableFrames = 0;
    }

    void Fall() {
        if ((!JumpReleased && currentGraveDisableFrames <= maxGravDisableFrames) || currentGraveDisableFrames < minGravDisableFrames) {
            currentGraveDisableFrames++;
            GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0, 1) * jumpHoldForce);
            
        }
        GetComponentInParent<Rigidbody2D>().velocity = GetComponentInParent<Rigidbody2D>().velocity + (new Vector2(0, -1) * fallAccel);
    }
}