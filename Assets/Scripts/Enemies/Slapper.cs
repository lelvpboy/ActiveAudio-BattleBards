using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slapper : EnemyController
{
    [SerializeField] private AnimTrigger trigger;

    private void Start()
    {
        ChangeState(0);
    }

    public override void Update()
    {
        base.Update();
        switch (state) 
        {
            case -1:
                Move();
                break;
            case 0:
                Move();
                break;
            default:
                ChangeState(0);
                break;
        }
    }

    public void TryAttack()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if(dist <= attackingDistance)
        {
            trigger.Trigger();
        }
    }
}
