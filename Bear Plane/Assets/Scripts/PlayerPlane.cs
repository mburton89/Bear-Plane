using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerPlane : Plane
{
    public float flyingEnergyConsumptionRate;
    public float shootingEnergyConsumptionRate;

    private void Update()
    {
        BearPlaneStateManager.Instance.UseEnergy(flyingEnergyConsumptionRate);

        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Move(direction);

        if (Input.GetMouseButtonDown(0))
        {
            FireProjectile(Vector2.right);
            BearPlaneStateManager.Instance.UseEnergy(shootingEnergyConsumptionRate);
        }

        CreateThrustParticles();
    }
}
