using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// to make sure we have a navMeshAgent
// unity will automatic add a navMeshAgent, whenever will call PlayerMotor compoement 

[RequireComponent(typeof(NavMeshAgent))] 
public class PlayerMotor : MonoBehaviour
{
    Transform target;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // get agent
    }

    //can use Coroutines
    // very performing, only updates the pos a new times a sec
    //https://docs.unity3d.com/Manual/Coroutines.html

    void Update()
    {
        if (target != null)
        {
            // got to target pos
            agent.SetDestination(target.position);

            FaceTarget();
        }
    }

    public void MoveToPoint (Vector3 point)
    {
        agent.SetDestination(point);
    }

    //support to track a target
    public void FollowTarget(Interactable newTarget)
    {
        // so we dont stopp inside the newTarget
        // 0.8f is to make sure inside the radius, not the boarder of the radius
        agent.stoppingDistance = newTarget.radius * 0.8f;

        agent.updateRotation = false;

        target = newTarget.interactionTransform;
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;

        agent.updateRotation = true;

        target = null;
    }

    void FaceTarget()
    {
        //direction towards our target
        Vector3 direction = (target.position - transform.position).normalized;

        // LookRotation takes a vector with a direction and looks at that diretion
        // dont put in just direction cause we want our player to look up and down
        // how we should rotate oursleves to look in that direction, avoid rotations on the y
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        // we can do lookRotation
        // if we want we can use Slerp
        // slerp allows us to sphereical interpolate between 2 points
        //transform.rotion and look rotation are the points
        // 5f at the end is the speed of rotation
        //interpolate towards that rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

}
