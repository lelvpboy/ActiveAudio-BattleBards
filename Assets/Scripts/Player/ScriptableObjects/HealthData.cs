using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "Colliders/Health Data")]
public class HealthData : ScriptableObject
{
    public float amount;
    public bool damagePlayer;
    public bool destroyOnCollision = true;
}