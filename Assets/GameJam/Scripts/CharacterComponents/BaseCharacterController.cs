using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseCharacterController : MonoBehaviour
{
    [Header("Base Variables")]
    public float maxHealth;
    public NavMeshAgent pathfindingController;
    public float score;
    public float attackingDistance;
    public LayerMask wallLayers;
    //[SerializeField] private BaseWeapon currentWeapon

    [HideInInspector] public Vector3 targetPosition; //target position to move to



    //hiding variables
    //Variables will be replaced by others in future.
    [Header("Hiding Variables")]
    [SerializeField] private int rays = 12;
    [SerializeField] private float maxRayDist = 30;
    [SerializeField] private float checkIncrement = 0.5f;
    [SerializeField] private int maxIncrements = 12;
    [SerializeField] private float size;
    
    
    //nonEditable variables:
    
    [HideInInspector] public GameObject target;
    [HideInInspector] public int state;
    //[HideInInspector] 
    public float currentHealth;


    bool hiding;

    public virtual void ChangeState(int _state)
    {
        state = _state;
    }

    public void Die()
    {
        UIManager manager = FindObjectOfType<UIManager>();
        manager.score += score * manager.combo;
        Destroy(gameObject);
    }


    public virtual void Update()
    {
        //Debug(hiding)

        //if(target != null)
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        hiding = !hiding;
        //    }
        //
        //    if (hiding)
        //    {
        //        targetPosition = Hide(target.transform.position);
        //        pathfindingController.stoppingDistance = 0.5f;
        //    }
        //    else
        //    {
        //        targetPosition = target.transform.position;
        //        pathfindingController.stoppingDistance = 3.5f;
        //    }
        //}

        if (state == -1)
        {
            targetPosition = Hide(target.transform.position);
            pathfindingController.stoppingDistance = 0.5f;
        }
        else
        {
            targetPosition = target.transform.position;
        }

            //TODO: if possible, attack.
    }

    public virtual void Move()
    {
        pathfindingController.SetDestination(targetPosition);
        Vector3 dir = new Vector3(targetPosition.x, transform.position.y, targetPosition.z) - transform.position;
        transform.forward = dir;
    }

    public virtual void Attack()
    {

    }

    public SFXPlayer hitSFX;

    public void ChangeHP(float amount)
    {
        hitSFX.PlayClip();

        currentHealth += amount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void SetHP(float amount)
    {
        currentHealth = amount;
    }

    public virtual bool CanSeeTarget(float range)
    {
        bool cansee = true;
        Vector3 CheckDir = targetPosition - transform.position;
        if(Physics.Raycast(transform.position, CheckDir, range, wallLayers))
        {
            cansee = false;
        }

        return cansee;
    }

    public virtual Vector3 Hide(Vector3 target)
    {
        Vector3 foundPoint = transform.position;

        //Cast rays around target
        //Check all collision points
        //move along the normal of closest collision point until a fillable gap is found
        //if no gap move onto next point

        //if gap found, check if the distance to it is closer than other points (not used anymore)
        //if a point is closer, check that point as well, choosing the closest gap. (not used anymore)


        //Cast rays around target
        List<RaycastHit> hits = new List<RaycastHit>();

        for(int i = 0; i < rays; i++)
        {
            float targetRot = (360 / rays) * i;

            Vector3 targetForward = Quaternion.Euler(0,targetRot,0) * Vector3.forward;

            RaycastHit hit;
            if (Physics.Raycast(target, targetForward, out hit, maxRayDist, wallLayers))
            {
                hits.Add(hit);
            }
        }

        //Check all collision points
        if (hits.Count > 0)
        {
            //sort list from closest point to furthest;
            hits.Sort(delegate (RaycastHit hit, RaycastHit hit2)
            {
                return Vector3.Distance(transform.position, hit.point).CompareTo(Vector3.Distance(transform.position, hit2.point));
            });

            foreach (RaycastHit hit in hits)
            {
                Vector3 checkDir = -hit.normal;

                foundPoint = CheckPoint(checkDir, hit.point);

                if (foundPoint != transform.position)
                {
                    break;
                }
                //if no gap move onto next point
            }
        }

        return foundPoint;
    }

    //move along the normal of closest collision point until a fillable gap is found
    private Vector3 CheckPoint(Vector3 checkDir, Vector3 startPos)
    {
        Vector3 foundPoint = transform.position;
        for (int i = 0; i < maxIncrements; i++)
        {
            Vector3 checkPos = startPos + checkDir * (i + 1) * checkIncrement;
            Collider[] cols = Physics.OverlapSphere(checkPos, size, wallLayers);
            if (cols.Length <= 0)
            {
                foundPoint = checkPos;
                break;
            }
        }
        return foundPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hitbox"))
        {
            ChangeHP(-1f);
        }
    }
}
