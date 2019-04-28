using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : Pickup
{
    [SerializeField]
    GameObject RespawnPosition;

    public GameObject GetRespawnPosition() {
        return RespawnPosition;
    }

    protected override void TriggerEndOverlapAnimation() {
    }

    protected override void TriggerOverlapAnimation() {
    }

    protected override void TriggerPickupLogic() {
        var player = overlaps[0].GetComponentInParent<MovementComponent>().gameObject;
        player.GetComponentInChildren<EnergyComponent>().RestoreEnergy();
        player.GetComponentInChildren<RespawningComponent>().SetLastSave(this);
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
