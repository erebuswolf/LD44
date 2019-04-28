using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    [SerializeField]
    Image image;

    EnergyComponent energyComponent;

    // Start is called before the first frame update
    void Start()
    {
        energyComponent = FindObjectOfType<EnergyComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        float energy = energyComponent.GetCurrentEnergy();
        image.rectTransform.localScale = new Vector3(Mathf.Clamp(energy, 0, 100) / 100, 1, 1);
    }
}
