using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public float bulletSpeed;
    public float spread;
    public GameObject bulletPrefab;
    public GameObject[] attacks;
    public AnimTrigger trigger;
    public Transform orient;

    Vector3 mousePos;

    [SerializeField] private SFXPlayer[] sfx;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hitData))
        {
            mousePos = hitData.point;
        }

        if (Input.GetMouseButtonDown(1))
        {
            sfx[1].PlayClip();

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

        if (Input.GetMouseButtonDown(0))
        {
            Melee();
        }
        Vector3 dir = mousePos - new Vector3(transform.position.x, mousePos.y, transform.position.z);
        orient.forward = dir;
    }

    public void Melee()
    {
        if (FindObjectOfType<MusicManager>().WithinBeatRange(0.35f))
        {
            attacks[0].SetActive(true);
            attacks[1].SetActive(true);
            FindObjectOfType<UIManager>().combo++;
        }
        else
        {
            attacks[0].SetActive(false);
            attacks[1].SetActive(false);
            FindObjectOfType<UIManager>().combo = 1;
        }

        trigger.Trigger();
        sfx[0].PlayClip();
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
        Vector3 dir = mousePos - new Vector3(spawned.transform.position.x, mousePos.y, spawned.transform.position.z);
        dir = Quaternion.Euler(new Vector3(0, angle, 0)) * dir;
        spawned.transform.forward = dir;
        spawned.GetComponent<Rigidbody>().velocity = spawned.transform.forward * bulletSpeed;
        Destroy(spawned, 6f);
    }
}
