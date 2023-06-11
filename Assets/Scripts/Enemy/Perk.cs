using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perk : MonoBehaviour
{

    [SerializeField] private float healthMultiplier;
    [SerializeField] private float damageMultiplier;
    [SerializeField] private float speedMultiplier;

    private void Start()
    {
        healthMultiplier = Random.Range(-1.5f, 1.2f);
        damageMultiplier = Random.Range(-1.5f, 1.2f);
        speedMultiplier = Random.Range(-1.5f, 1.2f);
    }

    public Perk(float healthMultiplier, float damageMultiplier, float speedMultiplier)
    {
        this.healthMultiplier = Random.Range(-1.5f, 1.2f);
        this.damageMultiplier = Random.Range(-1.5f, 1.2f);
        this.speedMultiplier = Random.Range(-1.5f, 1.2f);
        
    }

    public Perk getPerk()
    {
        return new Perk(healthMultiplier, damageMultiplier, speedMultiplier);
    }
    public float getDamageMultiplier()
    {
        return damageMultiplier;
    }
    public float getHealthMultiplier()
    {
        return healthMultiplier;
    }   
    public float getSpeedMultiplier()
    {
        return speedMultiplier;
    }
}
