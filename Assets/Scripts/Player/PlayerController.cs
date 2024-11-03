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
    public float speed;
    public int hp;
    public float invince;
    public float dodgeSpeed;
    public float dashTime;

    float dashLeft;

    private string targetAnimation = "isIdle";
    private string newTargetAnimation = "isIdle";

    private Animator animator;
    private Vector2 inputDirection;
    private Rigidbody rb;

    float x;
    float y;

    [SerializeField] private SFXPlayer[] sfx;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        hp = 3;
    }

    void Update()
    {
        dashLeft -= Time.deltaTime;

        invince -= Time.deltaTime;

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && dashLeft <= 0)
        {
            rb.velocity = new Vector3(x, 0, y) * (dodgeSpeed) * Time.fixedDeltaTime;
            dashLeft = dashTime;

            invince = 0.5f;

            //if (FindObjectOfType<MusicManager>().WithinBeatRange(0.5f))
            //{
            //    invince = 1f;
            //}
        }
    }

    private void FixedUpdate()
    {
        if(dashLeft <= 0)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        Vector3 movement = new Vector3(x, 0, y) * (speed * Time.fixedDeltaTime);

        if(movement.magnitude >= 0.1f)
        {
            sfx[0].PlayClip();
        }

        rb.velocity = movement;
    }

    public void Die()
    {
        FindObjectOfType<UIManager>().End();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EHitbox") && invince <= 0)
        {
            //TODO: Make this work
            invince = 2f;
            hp--;
            sfx[1].PlayClip();
            FindObjectOfType<UIManager>().TakeDamage(hp);
            if (hp <= 0)
            {
                Die();
            }
        }
    }
}
