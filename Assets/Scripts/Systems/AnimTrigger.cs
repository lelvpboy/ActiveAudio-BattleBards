using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    public Animator animator;
    public string triggerName;

    public void Trigger()
    {
        animator.SetTrigger(triggerName);
    }
}
