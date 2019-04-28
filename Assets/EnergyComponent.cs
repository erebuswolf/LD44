using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyComponent : MonoBehaviour
{
    [SerializeField]
    float CurrentEnergy;

    [SerializeField]
    float MaxEnergy = 100;

    bool ShouldDrainEnergy = true;

    [SerializeField]
    float baseDrainAmount;

    float currentDrainAmount;

    // Start is called before the first frame update
    void Start()
    {
        CurrentEnergy = MaxEnergy;
    }

    public float GetCurrentEnergy() {
        return CurrentEnergy;
    }

    public void RestoreEnergy() {
        CurrentEnergy = MaxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateDrainAmount() {
        currentDrainAmount =+ baseDrainAmount;
    }

    public bool SpendEnergy(float drainAmount, bool force = false) {
        if(!ShouldDrainEnergy) {
            return true;
        }
        if (CurrentEnergy < drainAmount && !force) {
            return false;
        }
        CurrentEnergy -= drainAmount;
        return true;
    }

    private void CheckDeath() {
        if (CurrentEnergy < 0) {
            GetComponent<DeathComponent>().PlayerDies();
        }
    }

    public void RespawnPlayer() {
        RestoreEnergy();
    }

    private void FixedUpdate() {
        UpdateDrainAmount();
        if (ShouldDrainEnergy) {
            CurrentEnergy -= currentDrainAmount;
        }
        CheckDeath();
    }
}
