using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Personnage : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Transform target;

    public float currentSpeed = 0;


    public InputActionReference moveAction, pointerAction, catchAction;

    public Vector3 move;
    public Vector2 pointer;
    public float maxSpeed = 500, acceleration = 500, deceleration = 500;


    public List<GameObject> sourisInRadius = new List<GameObject>();

    Vector3 oldMove;

        private bool facingRight = true;

    Rigidbody rb;

    float yRotation = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   

        move = moveAction.action.ReadValue<Vector3>();
        

        if (move.x > 0 && !facingRight)
        {
            yRotation = 180;
            facingRight = true;
        }
        else if (move.x < 0 && facingRight)
        {
            yRotation = 0;
            facingRight = false;
        }
        transform.rotation = Quaternion.Euler(90, yRotation, 0);

    
    }


    private void OnEnable() {
        catchAction.action.performed += CatchAction;
    }

    private void OnDisable() {
        catchAction.action.performed -= CatchAction;
    }

    public void CatchAction(InputAction.CallbackContext context)
    {
        Debug.Log("Catch");
    }

    public Vector2 PointerMouse()
    {
        Vector3 mousePosition = pointerAction.action.ReadValue<Vector2>();
        mousePosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

     private void FixedUpdate() {
        if (move.magnitude > 0 && currentSpeed >= 0)
        {
            oldMove = move;
            currentSpeed += acceleration * Time.deltaTime * maxSpeed;

        }
        else {
            currentSpeed -= deceleration * Time.deltaTime * maxSpeed;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        rb.velocity = oldMove * currentSpeed;
        
        
    }

    private void OnTriggerEnter(Collider other) {
        
    }


}
