using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    [SerializeField] private Trigger[] triggers;

    public void Trigger()
    {
        foreach(Trigger trigger in triggers)
        {
            trigger.animator.SetTrigger(trigger.triggerName);
        }
    }
}

[System.Serializable]
public class Trigger
{
    public Animator animator;
    public string triggerName;
}
