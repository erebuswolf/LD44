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
        if (pickedUp) {
            return;
        }
        var player = overlaps[0].GetComponentInParent<MovementComponent>().gameObject;
        if (player.GetComponentInChildren<WallJump>().ActivateWallJump()) {
            pickedUp = true;
        }

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
