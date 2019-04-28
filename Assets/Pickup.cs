using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Pickup : MonoBehaviour
{
    bool overlapping;

    public string ID { get; protected set; }

    protected bool pickedUp;

    protected List<Collider2D> overlaps = new List<Collider2D>();

    protected void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            TriggerOverlapAnimation();
            overlapping = true;
            overlaps.Add(collision);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            if (overlaps.Remove(collision) && overlaps.Count == 0) {
                TriggerEndOverlapAnimation();
                overlapping = false;
            }
        }
    }

    protected abstract void TriggerOverlapAnimation();

    protected abstract void TriggerEndOverlapAnimation();

    protected abstract void TriggerPickupLogic();

    // Start is called before the first frame update
    void Start()
    {
        ID = "default ID";
    }

    // Update is called once per frame
    protected void Update()
    {
        if (overlapping && Input.GetAxis("Vertical") < 0) {
            TriggerPickupLogic();
        }
    }
}
