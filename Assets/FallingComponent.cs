using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingComponent : MonoBehaviour {
    BoxCollider2D groundCollision;
    bool grounded;

    [SerializeField]
    float fallAccel = 1f;

    bool ShouldFall = true;

    List<Collider2D> trackedGroundObjects = new List<Collider2D>();

    // Start is called before the first frame update
    void Start() {
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

    }

    public void SetShouldFall(bool shouldFall) {
        ShouldFall = shouldFall;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!grounded && ShouldFall) {
            Fall();
        }
    }
    
    void Fall() {
        GetComponentInParent<Rigidbody2D>().velocity = GetComponentInParent<Rigidbody2D>().velocity + (new Vector2(0, -1) * fallAccel);
    }
}
