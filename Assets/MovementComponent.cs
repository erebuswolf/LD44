﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    bool disableMovement;
    [SerializeField]
    float xWalkMovementScaling = 1;
    [SerializeField]
    float xSprintMovementScaling = 3;
    
    [SerializeField]
    float xAirWalkMovementScaling = 1;
    [SerializeField]
    float xAirSprintMovementScaling = 3;

    [SerializeField]
    int maxEndWallClingFrames = 3;
    int currentEndWallClingFrames;

    public bool WallClinging { get; private set;}

    LayerMask wallLayerMask;

    [SerializeField]
    TouchingWallDetection touchingWallDetection;

    [SerializeField]
    FallingComponent fallingComponent;

    [SerializeField]
    GroundedComponenet groundedComponent;

    // Start is called before the first frame update
    void Start()
    {
        wallLayerMask = LayerMask.GetMask(new string[] { "Wall" });
    }

    // Update is called once per frame
    void Update() {
        if (disableMovement) {
            return;
        }

        float currentSprintAxis = Input.GetAxis("Sprint");
        bool currentSprintbool = currentSprintAxis != 0;

        float currentX = Input.GetAxis("Horizontal");
        currentX = currentX != 0 ? Mathf.Sign(currentX) : 0;
        Vector2 oldVel = gameObject.GetComponent<Rigidbody2D>().velocity;
        if (touchingWallDetection.TouchingWall && !groundedComponent.Grounded) {
            if (touchingWallDetection.TouchingWallOnRight()) {
                if (currentX > 0) {
                    StartWallcling();
                } else {
                    EndWallcling();
                }
            } else {
                if (currentX < 0) {
                    StartWallcling();
                } else {
                    EndWallcling();
                }
            }
            if (WallClinging) {
                return;
            }
        } else {
            ForceWallClingEnd();
        }
        if (groundedComponent.Grounded) {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(currentX * (currentSprintbool ? xSprintMovementScaling : xWalkMovementScaling), oldVel.y);
        } else {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(currentX * (currentSprintbool ? xAirSprintMovementScaling : xAirWalkMovementScaling), oldVel.y);
        }

        
    }

    void ForceWallClingEnd() {
        WallClinging = false;
        fallingComponent.ShouldFall = true;
    }

    void StartWallcling() {
        fallingComponent.ShouldFall = false;
        WallClinging = true;
        currentEndWallClingFrames = 0;
    }
    
    void EndWallcling() {
        currentEndWallClingFrames++;
        if (currentEndWallClingFrames > maxEndWallClingFrames) {
            WallClinging = false;
            fallingComponent.ShouldFall = true;
        }
    }
}
