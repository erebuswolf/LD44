using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    public bool CanSprint { get; private set; }

    public bool CanWallStick { get; set; }


    bool disableMovement;

    [SerializeField]
    float xWalkMovementScaling = 1;
    [SerializeField]
    float xSprintMovementScaling = 3;

    [SerializeField]
    float xGroundAccel = 1;

    [SerializeField]
    float xAirAccel = .5f;

    [SerializeField]
    float xAirWalkMovementScaling = 1;
    [SerializeField]
    float xAirSprintMovementScaling = 3;

    [SerializeField]
    int maxEndWallClingFrames = 3;
    int currentEndWallClingFrames;

    [SerializeField]
    float ActivationCost = 20f;

    public bool WallClinging { get; private set;}

    LayerMask wallLayerMask;

    [SerializeField]
    TouchingWallDetection touchingWallDetection;

    [SerializeField]
    FallingComponent fallingComponent;

    [SerializeField]
    GroundedComponenet groundedComponent;
    
    [SerializeField]
    EnergyComponent energyComponent;

    // Start is called before the first frame update
    void Start()
    {
        wallLayerMask = LayerMask.GetMask(new string[] { "Wall" });
    }

    public bool ActivateSprint() {
        if (energyComponent.SpendEnergy(ActivationCost)) {
            CanSprint = true;
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update() {
        if (disableMovement) {
            return;
        }

        float currentSprintAxis = Input.GetAxis("Sprint");
        bool currentSprintbool = currentSprintAxis != 0 && CanSprint;

        float currentX = Input.GetAxis("Horizontal");
        currentX = currentX != 0 ? Mathf.Sign(currentX) : 0;
        Vector2 oldVel = gameObject.GetComponent<Rigidbody2D>().velocity;
        if(CanWallStick) {
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
        }

        float MoveToUse;
        float accelToUse;

        if (groundedComponent.Grounded) {
            MoveToUse = currentSprintbool ? xSprintMovementScaling : xWalkMovementScaling;
            accelToUse = xGroundAccel;
        } else {
            MoveToUse = currentSprintbool ? xAirSprintMovementScaling : xAirWalkMovementScaling;
            accelToUse = xAirAccel;
        }
        
        Vector2 target = new Vector2(currentX * MoveToUse, oldVel.y);

        if (Mathf.Abs(target.x - oldVel.x) >= accelToUse) {
            target = new Vector2(Mathf.Sign(target.x - oldVel.x) * accelToUse + oldVel.x, target.y);
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = target;
        
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
