using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmController : MonoBehaviour
{
    public List<BaseCharacterController> Entities; //All entities in swarm (this is the swarmLeader)
    public float cohesion; //how far apart the entities spread


    //Debug
    public GameObject swarmPrefab;
    public int entityCount;
    public float spawnSpread;

    private void Start()
    {
        //Debug or not???

        //spawns a number of enemies around swarm spawn point, adding them to the swarm

        for(int i = 0; i < entityCount; i++)
        {
            GameObject spawned = Instantiate(swarmPrefab, transform.position + new Vector3(Random.Range(-spawnSpread, spawnSpread), 0, Random.Range(-spawnSpread, spawnSpread)), Quaternion.identity);
            BaseCharacterController entity = spawned.GetComponent<BaseCharacterController>();
            Entities.Add(entity);
        }
    }

    private void Update()
    {
        for (int i = 0; i < Entities.Count; i++)
        {
            float distFromLeader = Vector3.Distance(transform.position, Entities[i].transform.position);
            if (distFromLeader >= cohesion)
            {
                Entities[i].targetPosition = transform.position + new Vector3(Random.Range(-cohesion, cohesion), 0, Random.Range(-cohesion, cohesion));
            }
        }
    }
}
