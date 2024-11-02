using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public float bulletSpeed;
    public float spread;
    public GameObject bulletPrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (FindObjectOfType<MusicManager>().WithinBeatRange(0.35f))
            {
                SpreadShot();
                FindObjectOfType<UIManager>().combo++;
            }
            else
            {
                Shoot(0);
                FindObjectOfType<UIManager>().combo = 1;
            }
        }
    }

    public void SpreadShot()
    {
        Shoot(-spread);
        Shoot(spread);
        Shoot(0);
    }

    public void Shoot(float angle)
    {
        GameObject spawned = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mousePosition);
        Vector3 dir = mousePosition - new Vector3(spawned.transform.position.x, mousePosition.y, spawned.transform.position.z);
        dir = Quaternion.Euler(new Vector3(0, angle, 0)) * dir;
        spawned.transform.forward = dir;
        spawned.GetComponent<Rigidbody>().velocity = spawned.transform.forward * bulletSpeed;
        Destroy(spawned, 6f);
    }
}
