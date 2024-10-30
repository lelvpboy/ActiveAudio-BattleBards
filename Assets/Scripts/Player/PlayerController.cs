using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]

//TODO: I am unable to make animations work. Sprites dont support the old input system. I cannot figure out the new input system.
public class PlayerController : MonoBehaviour
{
    private IPlayerStats _playerStats;
    [SerializeField] private GameObject playerSprite;

    private string targetAnimation = "isIdle";
    private string newTargetAnimation = "isIdle";

    private Animator animator;
    private Vector2 inputDirection;


    private void Start()
    {
        animator = playerSprite.GetComponent<Animator>();

        _playerStats = PlayerManager.Instance;
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, 0, y) * (_playerStats.Speed * Time.deltaTime);

        transform.Translate(movement, Space.World);

        PlayAnimation(x, y);
    }

    private void PlayAnimation(float x, float y)
    {
        //Check the direction and play the animation
        if (y > 0) newTargetAnimation = "isWalkingUp";
        else if (y < 0) newTargetAnimation = "isWalkingDown";
        else if (x > 0) newTargetAnimation = "isWalkingRight";
        else if (x < 0) newTargetAnimation = "isWalkingLeft";
        else newTargetAnimation = "isIdle";
        
        animator.Play(newTargetAnimation, 0, 0f);
        
        // //Reset all directions
        // if (targetAnimation != newTargetAnimation)
        // {
        //     //Reset all directions
        //     animator.SetBool("isWalkingUp", false);
        //     animator.SetBool("isWalkingDown", false);
        //     animator.SetBool("isWalkingLeft", false);
        //     animator.SetBool("isWalkingRight", false);
        //     animator.SetBool("isIdle", false);
        //     
        //     //Set the new animation
        //     targetAnimation = newTargetAnimation;
        //     animator.SetBool(targetAnimation, true);
        // }
        // else
        // {
        //     //Temporary, do nothing
        // }

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
