using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpPickup : Pickup
{

    protected override void TriggerEndOverlapAnimation() {
    }

    protected override void TriggerOverlapAnimation() {
    }

    protected override void TriggerPickupLogic() {
        overlaps[0].GetComponentInParent<MovementComponent>().CanWallStick = true;
        var player = overlaps[0].GetComponentInParent<MovementComponent>().gameObject;
        player.GetComponentInChildren<WallJump>().enabled = true;
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
