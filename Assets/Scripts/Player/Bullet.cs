using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<BaseCharacterController>().ChangeHP(-1);
            Destroy(gameObject);
        }
    }
}
