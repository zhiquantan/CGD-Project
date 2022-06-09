
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{


    public int bulletNumber = 0;
    public GameObject theBullet;
    public Transform barrelEnd;

    public int bulletSpeed;
    public float despawnTime = 3.0f;

    public bool shootAble = true;
    public float waitBeforeNextShot = 0.25f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (shootAble && bulletNumber != 0)
            {
                bulletNumber--;
                shootAble = false;
                Shoot();
                StartCoroutine(ShootingYield());
            }
        }
    }

    IEnumerator ShootingYield()
    {
        yield return new WaitForSeconds(waitBeforeNextShot);
        shootAble = true;
    }
    void Shoot()
    {
        Debug.Log("shoot");
        var bullet = Instantiate(theBullet, barrelEnd.position, barrelEnd.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        bullet.GetComponent<AudioSource>().Play();

        Destroy(bullet, despawnTime);
    }
}
