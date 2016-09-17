using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    bool isMagicBullet = false;
    bool hasTarget = false;
    Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (isMagicBullet)
        {
            Debug.Log("IsMagic");
            Collider[] nearEnemies = Physics.OverlapSphere(transform.position, 20, 1 << 9);
            if (nearEnemies == null || nearEnemies.Length == 0)
            {
                hasTarget = false;
            }
            else
            {
                foreach (Collider col in nearEnemies)
                {
                    if (col.name.Contains("EnemyPrefab"))
                    {
                        target = col.transform;
                        hasTarget = true;
                        break;
                    }
                }
            }

            if (hasTarget)
            {
                // Turn towards target player over time
                Quaternion lookPosition = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookPosition, Time.deltaTime * 500f);
                GetComponent<Rigidbody>().velocity += transform.forward * Time.deltaTime * 100f;
            }
        }

	}

    void OnCollisionEnter(Collision other)
    {
        // I hit something! Try to grab its health script
        var health = other.gameObject.GetComponent<HealthScript>();
        // If it has health... make it take damage!
        if (health != null)
        {
            health.TakeDamage(10);
        }

        // Destroy this bullet
        Destroy(gameObject);
    }

    public void MakeMagicBullet()
    {
        isMagicBullet = true;
    }
}
