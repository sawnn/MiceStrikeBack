using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class TrapSelectionMenu : MonoSingleton<TrapSelectionMenu>
{

    public GameObject selectedTrap; 
    public InputActionReference leftClickAction, rightClickAction;

    private TMPro.TMP_Text timerText;
    private void Start()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Timer");
        timerText = temp[0].GetComponent<TMPro.TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedTrap != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedTrap.transform.position = new Vector3(mousePos.x, 11, mousePos.z);
        }
    }

    public void DisplayTime(float timeRemaining)
    {
        timerText.text = string.Format("{0:#0.0}", timeRemaining);
    }

    public void DesactiveManager()
    {
        gameObject.SetActive(false);
    }

    public void ActiveManager()
    {
        gameObject.SetActive(true);
    }

    public void SelectTrap(GameObject trap)
    {
        selectedTrap = trap;
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
            selectedTrap.transform.Rotate(Vector3.forward, 45f);
        }
    }

    public void SelectTrap(InputAction.CallbackContext context)
    {
        Debug.Log("SelectTrap");
        Ray ray;
        RaycastHit hit;

        if (selectedTrap)
        {
            NavMeshHit hitNav;
            if (NavMesh.SamplePosition(selectedTrap.transform.position, out hitNav, 100f, NavMesh.AllAreas))
            {
                Debug.Log("Navmesh found under selected object");
                selectedTrap = null;
            }
        }
        else
        {
            ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log("heree");
                if (hit.collider.gameObject.tag == "Trap")
                {
                        selectedTrap = hit.collider.gameObject;
                        //selectedTrap.transform.localScale = new Vector3(10f, 10f, 10f);
                }
            }
        }

        //Debug.DrawRay(position, Vector3.down, Color.red, 5f);
      /*  if (Physics.Raycast(position, Vector3.down, out RaycastHit hit, 100f))
        {
            Debug.Log("heree");
            if (hit.collider.gameObject.tag == "Trap")
            {
                if (selectedTrap == null) 
                {
                    selectedTrap = hit.collider.gameObject;
                    selectedTrap.transform.SetParent(null);
                    //selectedTrap.transform.localScale = new Vector3(10f, 10f, 10f);
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
          

            
        
    }


}
