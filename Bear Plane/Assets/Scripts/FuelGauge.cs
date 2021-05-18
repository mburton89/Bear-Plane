using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelGauge : MonoBehaviour
{
    public static FuelGauge Instance;
    float unitsOfFuel;
    public float maxUnitsOfFuel;
    public float minUnitsOfFuel;
    [SerializeField] Transform needlePivot;
    [SerializeField] float fuelConsumptionRate;

    void Awake()
    {
        unitsOfFuel = 116;
        Instance = this;
    }

    void Update()
    {
        unitsOfFuel -= fuelConsumptionRate;
        needlePivot.eulerAngles = new Vector3(0, 0, -unitsOfFuel);

        if (unitsOfFuel < minUnitsOfFuel)
        {
            if (FindObjectOfType<PlayerPlane>())
            {
                FindObjectOfType<PlayerPlane>().Splode();
                fuelConsumptionRate = 0;
            }
        }
    }

    public void AddUnitsOfFuel(float unitsOfFuelToAdd)
    {
        unitsOfFuel += unitsOfFuelToAdd;
        if (unitsOfFuel > maxUnitsOfFuel)
        {
            unitsOfFuel = maxUnitsOfFuel;
        }
    }
}
