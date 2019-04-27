using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedComponenet : MonoBehaviour {
    BoxCollider2D groundCollision;
    public bool Grounded { get; private set; }
    
    List<Collider2D> trackedGroundObjects = new List<Collider2D>();

    // Start is called before the first frame update
    void Start()
    {
        groundCollision = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            Grounded = true;
            trackedGroundObjects.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (trackedGroundObjects.Remove(collision) && trackedGroundObjects.Count == 0) {
            Grounded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
