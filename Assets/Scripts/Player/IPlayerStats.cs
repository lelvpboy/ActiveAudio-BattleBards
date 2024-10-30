using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerStats
{
    float Speed
    {
        get;
    }

    float Health
    {
        get;
    }
    void Heal(float amount);
    void Damage(float amount);
}

