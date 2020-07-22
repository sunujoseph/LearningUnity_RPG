using UnityEngine;

public class Interactable : MonoBehaviour
{

    public float radius = 3f;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    // main purpose of this script is to have all objects, items and eneimes derive from it
    // we inherited all the fields and methods

    // what we dont want to be uniform is how we interact with said object
    // different interacts have differecnt interactions

    // virtual - we can call it from our interactable script
    // but withim our items or enemy script we can overwrite it
    // we can put our own functionaitly for each type of interactable.

    public virtual void Interact ()
    {
        // this method is meant to be overwritten
        //Debug.Log("Interaction with " + transform.name);
    }

    void Update()
    {
        //check if iteracble is being focued
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <=radius )
            {
                //Debug.Log("INTERACT");
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDeFocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    void OnDrawGizmosSelected()
    {

        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

}
