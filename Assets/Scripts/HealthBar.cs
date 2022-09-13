using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Fighter healthSystem;

    public void Setup(Fighter healthSystem)
    {
        this.healthSystem = healthSystem;

        healthSystem.OnHealthChanged += Fighter_OnHealthChanged;
    }

    private void Fighter_OnHealthChanged(object sender, System.EventArgs e)
    {
        transform.Find("Bar").localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
    }
}
