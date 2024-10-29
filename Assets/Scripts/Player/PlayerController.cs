using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    PlayerManager _playerManager;

    void Start()
    {
        _playerManager = PlayerManager.instance;
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float x;
        float y;
        
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        float speed = _playerManager.GetSpeed();
        
        Vector3 movement = new Vector3(x, 0, y) * (speed * Time.deltaTime);

        transform.Translate(movement, Space.World);
    }

    void PlayAnimation()
    {
        //TODO: Make this work
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("EnemyWeapon"))
        {
            //TODO: Make this work
            Debug.Log("Collided with EnemyWeapon");
        }
        else if (other.gameObject.CompareTag("Health"))
        {
            Debug.Log("Collided with Health");
            //TODO: playerManager.AddHealth(other.GetComponent<HealthControllerThing>().increaseAmount);
        }
    }
}
