using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseCharacterController
{
    public override void ChangeState(int _state)
    {
        base.ChangeState(_state);
        switch (_state) 
        {
            case -1:
                break;
            case 0:
                Attack();
                break;
            default:
                break;
        }
    }

    public override void Attack()
    {
        base.Attack();
        target = GameObject.FindGameObjectWithTag("Player");
        pathfindingController.stoppingDistance = attackingDistance;
    }
}
