using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;

public class PlayerController : NetworkBehaviour
{
    public GameObject head;
    public GameObject gun;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    void Update()
    {
        // Don't do anything if this isn't the local player
        if (!isLocalPlayer)
        {
            return;
        }

        // Movement of player character
        //var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var yRotate = Input.GetAxis("Mouse X") * Time.deltaTime * 700;
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 6.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 6.0f;

        transform.Rotate(0, yRotate, 0);
        transform.Translate(x, 0, 0);
        transform.Translate(0, 0, z);

        // Move visor with vertical mouse movement
        var xRotate = -1 * Input.GetAxis("Mouse Y") * Time.deltaTime * 200;
        head.transform.Rotate(xRotate, 0, 0);


        // Movement of the gun
        gun.transform.Rotate(xRotate, 0, 0);


        // Fire gun?
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CmdFire();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            CmdShotgun();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            CmdFunGun();
        }

    }

    // Make the local player appear blue (to the local player only)
    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    [Command]
    void CmdFire()
    {
        // Create bullet from bulletPrefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        //bullet.GetComponent<BulletScript>().MakeMagicBullet();

        // Add velocity to bullet
        bullet.GetComponent<Rigidbody>().velocity = gun.transform.up * 15;

        NetworkServer.Spawn(bullet);

        // Destroy bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }

    [Command]
    void CmdShotgun()
    {
        List<GameObject> bulletStorm = new List<GameObject>();

        for (int i = 0; i < 50; i++)
        {
            var bullet = (GameObject)Instantiate(
                bulletPrefab,
                bulletSpawn.position,
                bulletSpawn.rotation);

            bulletStorm.Add(bullet);
        }

        foreach (GameObject bullet in bulletStorm)
        {
            bullet.GetComponent<Rigidbody>().velocity = gun.transform.up * Random.Range(50f, 100f) + gun.transform.right * Random.Range(-10f, 10f) + gun.transform.forward * Random.Range(-10f, 10f);
            //bullet.GetComponent<Rigidbody>().velocity += new Vector3(6 * Random.Range(-2f,2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            NetworkServer.Spawn(bullet);
            Destroy(bullet, 2.0f);
        }
    }

    [Command]
    void CmdFunGun()
    {
        List<GameObject> bulletStorm = new List<GameObject>();

        for (int j = 0; j < 10; j++)
        {
            var bullet = (GameObject)Instantiate(
                bulletPrefab,
                bulletSpawn.position,
                bulletSpawn.rotation);

            bullet.GetComponent<BulletScript>().MakeMagicBullet();
            bulletStorm.Add(bullet);
        }

        foreach (GameObject bullet in bulletStorm)
        {
            bullet.GetComponent<Rigidbody>().velocity = gun.transform.up * Random.Range(5f, 10f) + gun.transform.right * Random.Range(-6f, 6f) + gun.transform.forward * Random.Range(-6f, 6f);
            //bullet.GetComponent<Rigidbody>().velocity += new Vector3(6 * Random.Range(-2f,2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            NetworkServer.Spawn(bullet);
            Destroy(bullet, 5.0f);
        }
    }
}
