using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform target;

    NavMeshAgent agent;

    CharacterCombat combat;

    // Start is called before the first frame update
    void Start()
    {
        // we would use GameObject.FindGameWithTag
        // but we wwould have to search thru the objects in our scene
        // instead we would have a script to keep track with the player

        target = PlayerManager.instance.player.transform;

        agent = GetComponent<NavMeshAgent>();

        combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        // distance to the target
        float distance = Vector3.Distance(target.position, transform.position);

        // if inside the look radius
        if (distance <= lookRadius)
        {
            // move towards target
            agent.SetDestination(target.position);

            // if within attack range
            if (distance <= agent.stoppingDistance)
            {
                // attack the target
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    combat.Attack(targetStats);
                }

                // face the target
                FaceTarget();

            }

        }

    }

    // rotate to face the target
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
