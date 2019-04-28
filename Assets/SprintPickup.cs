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
        overlaps[0].GetComponentInParent<MovementComponent>().CanSprint = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
