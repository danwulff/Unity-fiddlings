using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemyScript : MonoBehaviour
{

    Transform target;
    bool hasTarget = false;
    float attackCooldownSec = 5f;
    float nextAttack = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider[] nearPlayers = Physics.OverlapSphere(transform.position, 20, 1 << 8);
        if (nearPlayers == null || nearPlayers.Length == 0)
        {
            hasTarget = false;
        }
        else
        {
            foreach (Collider col in nearPlayers)
            {
                if (col.name.Contains("PlayerPrefab"))
                {
                    //Debug.Log("Found player at: " + col.gameObject.transform.position);
                    target = col.transform;
                    hasTarget = true;
                    break;
                }
            }
        }

        if (hasTarget)
        {
            // Turn towards target (player) INSTANTLY
            //var targetPosition = target.position;
            //targetPosition.y = transform.position.y;    // This is to force rotation about the Y axis
            //transform.LookAt(targetPosition);

            // Turn towards target player over time
            Quaternion lookPosition = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookPosition, Time.deltaTime * 100f);

            float angleToPlayer = Quaternion.Angle(transform.rotation, lookPosition);
            // Move towards target if we're looking within 45° of target
            if (angleToPlayer < 45f)
                transform.Translate(0, 0, Time.deltaTime * 1f);
        }

        //var movement = 0.1f;
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movement);

    }



}
