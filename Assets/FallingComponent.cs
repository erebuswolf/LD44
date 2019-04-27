using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingComponent : MonoBehaviour {
    BoxCollider2D groundCollision;
    bool grounded;

    [SerializeField]
    float fallAccel = 1f;

    public bool ShouldFall { get; set; }

    List<Collider2D> trackedGroundObjects = new List<Collider2D>();

    // Start is called before the first frame update
    void Start() {
        ShouldFall = true;
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

    // Update is called once per frame
    void FixedUpdate() {
        if (!grounded) {
            Fall();
        }
    }
    
    void Fall() {
        var startVel = GetComponentInParent<Rigidbody2D>().velocity;
        startVel = startVel + (new Vector2(0, -1) * fallAccel);
        if (!ShouldFall && startVel.y < 0) {
            startVel = new Vector2(startVel.x, 0);
        }
        GetComponentInParent<Rigidbody2D>().velocity = startVel;
    }
}
