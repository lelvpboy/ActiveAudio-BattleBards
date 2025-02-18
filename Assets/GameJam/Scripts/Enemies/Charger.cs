using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : EnemyController
{
    [Header("Charger Variables")]
    [SerializeField] private float chargeRange;
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float chargeCooldown;

    [SerializeField] private Animator animator;

    LineRenderer lineRenderer;
    float chargeCooldownRemaining;

    public override void ChangeState(int _state)
    {
        base.ChangeState(_state);

    }

    private void Start()
    {
        ChangeState(0);
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    public void StartCharge()
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance <= chargeRange && state == 0 && chargeCooldownRemaining <= 0)
        {
            animator.SetTrigger("Charge");

            lineRenderer.enabled = true;
            ChangeState(20);
            pathfindingController.enabled = false;
        }
    }

    public void PrepCharge()
    {
        if(state == 20)
        {
            ChangeState(21);
        }
    }

    public void Charge()
    {
        if(state == 21)
        {
            animator.SetTrigger("Launch");

            lineRenderer.enabled = false;
            ChangeState(22);
        }

        if(state == -1)
        {
            currentHealth += 0.5f;
        }
    }

    public void HitCharge()
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance <= chargeRange && chargeCooldownRemaining <= 0)
        {
            animator.SetTrigger("Reset");
            lineRenderer.enabled = true;
            ChangeState(20);
            pathfindingController.enabled = false;
        }
        else
        {
            animator.SetTrigger("Land");
            ChangeState(23);
        }
    }

    public override void Update()
    {
        chargeCooldownRemaining -= Time.deltaTime;
        base.Update();
        switch (state) 
        {
            case -1:
                if(currentHealth > maxHealth / 3)
                {
                    ChangeState(0);
                }
                Move();
                break;
            case 0:
                if (currentHealth <= maxHealth / 3)
                {
                    ChangeState(-1);
                }
                Move();
                break;
            case 1:
                break;
            default:
                ChangeState(0);
                break;
            case 20:
                lineRenderer.positionCount = 2;
                Vector3 dir = new Vector3(targetPosition.x, transform.position.y, targetPosition.z) - transform.position;
                dir.Normalize();
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + dir * chargeRange);
                transform.forward = dir;
                break;
            case 21:

                break;
            case 22:
                transform.position = Vector3.Lerp(transform.position, lineRenderer.GetPosition(1), chargeSpeed * Time.deltaTime);
                float dist = Vector3.Distance(transform.position, lineRenderer.GetPosition(1));
                if(dist <= 0.1)
                {
                    HitCharge();
                }
                break;
            case 23:
                if(chargeCooldownRemaining <= 0)
                {
                    pathfindingController.enabled = true;
                    ChangeState(0);
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (state) 
        {
            case 22:
                if(other.tag == "Player")
                {
                    chargeCooldownRemaining = chargeCooldown;
                    //HitCharge();
                }
                if(other.tag == "Wall")
                {
                    chargeCooldownRemaining = chargeCooldown;
                    HitCharge();
                }
                break;
        }
    }
}
