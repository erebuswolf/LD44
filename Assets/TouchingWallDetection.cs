using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingWallDetection : MonoBehaviour {
    public bool TouchingWall{get; private set;}

    List<Collider2D> trackedObjects = new List<Collider2D>();
    

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Wall")) {
            trackedObjects.Add(collision.collider);
            TouchingWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (trackedObjects.Remove(collision.collider) && trackedObjects.Count == 0) {
            TouchingWall = false;
        }
    }

    public bool TouchingWallOnRight() {
        if (trackedObjects.Count > 0) {
            if (trackedObjects[0].gameObject.transform.position.x > this.gameObject.transform.position.x) {
                return true;
            }
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
