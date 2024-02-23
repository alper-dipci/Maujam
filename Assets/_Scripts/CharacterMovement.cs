using UnityEngine;
using UnityEngine.Experimental.AI;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f;

    private Rigidbody rb;

    Vector3 inputVector;


    void Start()
    {
        //animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {


        //CharacterAnimation();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");


        inputVector = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 movement = inputVector * moveSpeed;

        Quaternion cameraRotation = Quaternion.Euler(0, -45, 0);
        movement = cameraRotation * movement;



        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);

        if (inputVector != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputVector, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

    }
    //private void flip()
    //{

    //    if (inputVector.magnitude > 0 && Input.GetKeyDown(KeyCode.Space))
    //    {
    //        animator.SetBool("flip", true);
    //        rb.AddForce(inputVector * Time.fixedDeltaTime);
    //    }
    //    else if (Input.GetKeyUp(KeyCode.Space))
    //    {
    //        animator.SetBool("flip", false);
    //    }
    //}
    //public void CharacterAnimation()
    //{
    //    if (inputVector != Vector3.zero)
    //    {
    //        animator.SetBool("walk", true);
    //    }

    //    else
    //    {
    //        animator.SetBool("walk", false);
    //    }
    //}

}
