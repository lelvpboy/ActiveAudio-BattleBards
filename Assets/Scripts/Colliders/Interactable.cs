using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public HealthData healthData;
    
    PlayerManager playerManager;

    void Start()
    {
        playerManager = PlayerManager.Instance;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (playerManager != null)
        {
            ManageHealth();
        }
        else
        {
            Debug.LogError("No playerManager on other");
        }
    }
    
    //Health
    void ManageHealth()
    {
        //Deal with the health
        if (healthData.damagePlayer)
        {
            //Damage player
            if (playerManager.Health - healthData.amount <= 0)
            {
                //Player has no health left
                Debug.Log("Player has no health, killing");
                playerManager.KillPlayer();
            }
            else
            {
                //Damage player
                playerManager.Damage(healthData.amount);
                
                Destroy(this.gameObject);
            }
        }
        else if (!healthData.damagePlayer)
        {
            //Heal player
            if (playerManager.Health < 100)
            {
                playerManager.Heal(healthData.amount);
                
                Destroy(this.gameObject);
            }
        }
        else
        {
            Debug.LogError("No data found on this object.");
        }
    }
}
