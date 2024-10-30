using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IPlayerStats
{
    //Note: Should I turn this into an interface?
    public static PlayerManager Instance { get; private set; }
    
    [SerializeField] private float speed = 10f;
    [SerializeField] private float health = 100f;
    [SerializeField] private float strength;
    
    private string _currentWeapon;
    
    public float Speed => speed;
    public float Health => health;
    public float Strength => strength;
    
    public event Action OnStatsChanged;

    void Awake()
    {
        //Generic instance setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Heal(float amount)
    {
        if (health + amount > 100)
        {
            //If the health will be greater than 100
            health = 100;
        }
        else
        {
            //Add the health
            health += amount;
        }

        OnStatsChanged?.Invoke();
    }

    public void Damage(float amount)
    {
        //TODO: If health <= 0, KillPlayer()
        health -= amount;
        OnStatsChanged?.Invoke();
    }

    public void KillPlayer()
    {
        //TODO: Kill the player
        Debug.LogWarning("KillPlayer() not setup yet.");
    }
    
    public void SetWeapon(string newWeapon)
    {
        _currentWeapon = newWeapon;
        OnStatsChanged?.Invoke();
        
        //TODO: Need to check if the weapon exists
    }
}
