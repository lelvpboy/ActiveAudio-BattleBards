using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float strength;
    [SerializeField] private string currentWeapon;

    void Awake()
    {
        //Generic instance setup
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    //Speed
    public float GetSpeed()
    {
        return speed;
    }

    //Health
    public float GetHealth()
    {
        return health;
    }
    
    public void HealPlayer(float amount)
    {
        health += amount;
    }

    public void DamagePlayer(float amount)
    {
        health -= amount;
    }

    public void KillPlayer()
    {
        //TODO: Kill the player
        Debug.LogWarning("KillPlayer() not setup yet.");
    }
    
    //Weapon + Health
    public float GetStrenght()
    {
        return strength;
    }
    
    public void SetWeapon(string newWeapon)
    {
        currentWeapon = newWeapon;
        
        //TODO: Need to check if the weapon exists
    }
}
