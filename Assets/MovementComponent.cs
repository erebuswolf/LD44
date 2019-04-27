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

    // Start is called before the first frame update
    void Start()
    {
        
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
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(currentX * (currentSprintbool ? xSprintMovementScaling : xWalkMovementScaling), oldVel.y);


    }
}
