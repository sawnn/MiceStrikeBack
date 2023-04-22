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


    public InputActionReference moveAction, pointerAction;

    public Vector2 move;
    public Vector2 pointer;
    public float maxSpeed = 500, acceleration = 500, deceleration = 500;

    Vector2 oldMove;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   

        move = moveAction.action.ReadValue<Vector2>();
        //move = new Vector3(value.x, 0, value.y);
        pointer = PointerMouse();

    
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


}
