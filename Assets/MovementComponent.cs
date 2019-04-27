using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    bool disableMovement;
    [SerializeField]
    float xWalkMovementScaling = 1;
    [SerializeField]
    float xSprintMovementScaling = 3;

    LayerMask wallLayerMask;

    [SerializeField]
    TouchingWallDetection touchingWallDetection;

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
        bool  currentSprintbool = currentSprintAxis != 0;

        float currentX = Input.GetAxis("Horizontal");
        currentX = currentX != 0 ? Mathf.Sign(currentX) : 0;
        Vector2 oldVel = gameObject.GetComponent<Rigidbody2D>().velocity;
        if (touchingWallDetection.TouchingWall) {
            if(touchingWallDetection.TouchingWallOnRight()) {
                currentX = Mathf.Clamp(currentX, -1, 0);
            } else {
                currentX = Mathf.Clamp(currentX, 0, 1);
            }
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(currentX * (currentSprintbool ? xSprintMovementScaling : xWalkMovementScaling), oldVel.y);
    }
    
}
