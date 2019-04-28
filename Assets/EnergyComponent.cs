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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateDrainAmount() {
        currentDrainAmount =+ baseDrainAmount;
    }

    public bool SpendEnergy(float drainAmount) {
        if(!ShouldDrainEnergy) {
            return true;
        }
        if (CurrentEnergy > drainAmount) {
            CurrentEnergy -= drainAmount;
            return true;
        }
        return false;
    }

    private void FixedUpdate() {
        UpdateDrainAmount();
        if (ShouldDrainEnergy) {
            CurrentEnergy -= currentDrainAmount;
        }
    }
}
