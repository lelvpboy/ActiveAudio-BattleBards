using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector2 boundary;
    public Transform target;
    public float followSpeed;

    public void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z), followSpeed * Time.fixedDeltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-boundary.x, boundary.x), transform.position.y, Mathf.Clamp(transform.position.z,-boundary.y, boundary.y));
    }
}
