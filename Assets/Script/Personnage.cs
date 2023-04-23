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

    public Animator animator;


    public List<GameObject> sourisInRadius = new List<GameObject>();

    Vector3 oldMove;

    private bool facingRight = true;

    Rigidbody rb;

    float xRotation = 90;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   
        Vector3 tmpMove = moveAction.action.ReadValue<Vector3>();
        move = new Vector3(tmpMove.x, 0, tmpMove.z);


        

        if (move.x > 0 && !facingRight)
        {
            xRotation = 90;
            facingRight = true;
        }
        else if (move.x < 0 && facingRight)
        {
            xRotation = -90;
            facingRight = false;
        }
        transform.rotation = Quaternion.Euler(xRotation, 90, 90);
        if (move.magnitude > 0)
        {
            animator.SetBool("isMoving", true);
        }
        else {
            animator.SetBool("isMoving", false);
        }

    
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
        if (sourisInRadius.Count > 0)
        {
            foreach (GameObject souris in sourisInRadius)
            {
<<<<<<< HEAD
                Debug.Log("Mouse caught");
=======
                SoundManager.Instance.PlaySound("catchSound");
>>>>>>> 358f46bcd5026ab7952873ef2c93ab4a79273c1e
                Destroy(souris);
                ScoreController.Instance.MouseCaught();
            }
            sourisInRadius.Clear();
        }
        else {
            ScoreController.Instance.CatchFail();
            Debug.Log("No mouse in radius");
        }
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
        if (other.gameObject.tag == "Souris")
        {
            if (!sourisInRadius.Contains(other.gameObject))
                sourisInRadius.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Souris")
        {
            sourisInRadius.Remove(other.gameObject);
        }
    }


}
