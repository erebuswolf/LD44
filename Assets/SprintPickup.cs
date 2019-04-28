using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintPickup : Pickup
{
    protected override void TriggerEndOverlapAnimation() {
    }

    protected override void TriggerOverlapAnimation() {
    }

    protected override void TriggerPickupLogic() {
        if (pickedUp) {
            return;
        }
        if(overlaps[0].GetComponentInParent<MovementComponent>().ActivateSprint()) {
            pickedUp = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ID = "SprintPickup";
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
