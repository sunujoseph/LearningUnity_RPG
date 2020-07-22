using UnityEngine.EventSystems;
using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    public Interactable focus;

    public LayerMask movementMask;

    Camera cam;
    PlayerMotor motor;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {

        //check if we are covering over UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {

                //Debug.Log("We hit" + hit.collider.name + " " + hit.point);

                // move our player to what we hit
                motor.MoveToPoint(hit.point);

                // stop focusing any objects
                RemoveFocus();

            }

        }


        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {

                //Debug.Log("We hit" + hit.collider.name + " " + hit.point);

                //check if we hit an interactable
                //checke for interactable compoent
                Interactable interactable = hit.collider.GetComponent<Interactable>(); 

                //if we did set it as our focus
                //if we hit an interactable, we want to it as our focus
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }

        }
    }

    void SetFocus (Interactable newFocus)
    {
        //check if player is focused on another interactabele focus
        // if so then defocus that previous one
        // then focus on the new one
        if (newFocus != focus)
        {

            if ( focus != null)
            {
                //check if focus is null
                focus.OnDeFocused();
            }
                
            focus = newFocus;

            //motor.MoveToPoint(newFocus.transform.position);
            // this will only our player once. and moves to our new focus
            motor.FollowTarget(newFocus);
        }

        // focused for interact
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDeFocused();
        }


        focus = null;
        motor.StopFollowingTarget();
    }
}
