using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class TrapSelectionMenu : MonoBehaviour
{

    public GameObject selectedTrap; 
    public InputActionReference leftClickAction, rightClickAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedTrap != null)
        {
          
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedTrap.transform.position = new Vector3(mousePos.x, 5, mousePos.z);
        }
        
    }

    private void OnEnable()
    {
        leftClickAction.action.performed += SelectTrap;
        rightClickAction.action.performed += RotateTrap;
    }

    private void OnDisable()
    {
        leftClickAction.action.performed -= SelectTrap;
        rightClickAction.action.performed -= RotateTrap;
    }
    
    public void RotateTrap(InputAction.CallbackContext context)
    {
        if (selectedTrap)
        {
            selectedTrap.transform.Rotate(Vector3.up, 90f);
        }
    }

    public void SelectTrap(InputAction.CallbackContext context)
    {
        Debug.Log("SelectTrap");
        Ray ray;
        if (selectedTrap)
        {
            ray = new Ray(selectedTrap.transform.position, Vector3.down);
        }
        else
        {
            ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        }

        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 5f);
        if (Physics.Raycast(ray, out RaycastHit hit,  Mathf.Infinity))
        {
            Debug.Log("heree");
            if (hit.collider.gameObject.tag == "Trap")
            {
                if (selectedTrap == null) 
                {
                    selectedTrap = hit.collider.gameObject;
                    selectedTrap.transform.SetParent(null);
                    selectedTrap.transform.localScale = new Vector3(10f, 10f, 10f);
                }
            }
            Debug.Log("imhereee");
            if (hit.collider.gameObject.tag == "Plane")
            {
                if (selectedTrap != null)
                {
                    Debug.Log("Position is on navmesh.");
                    selectedTrap = null;
                } 
            }
        /*   NavMeshHit navHit;
            if (NavMesh.SamplePosition(hit.point, out navHit, 0.1f, NavMesh.AllAreas))
            {
                if (selectedTrap != null)
                {
                    Debug.Log("Position is on navmesh.");
                    selectedTrap = null;
                }
            }*/
            else
            {
                Debug.Log("Position is not on navmesh.");
            }

            

     
        }
    }


}
