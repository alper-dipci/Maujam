using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.AI;
using DG.Tweening;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f;

    private Animator animator;
    private Rigidbody rb;

    bool isDodging;
    [SerializeField] float dodge_coolDown;
    [SerializeField] float dodgeTimer;
    AudioSource audioSource;
    [SerializeField] AudioClip dodgeAudio;
    [SerializeField] private float dodgeForce;

    Vector3 inputVector;
    public Vector3 movement;
    private bool canRope;
    public bool onRope;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera vcam;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RecordControls();
        if (dodgeTimer >= 0) dodgeTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (dodgeTimer > 0) return;
            if (inputVector.magnitude != 0 && dodgeTimer<=0)
            {
                Debug.Log("dodge");
                dodgeTimer += dodge_coolDown;
                StartCoroutine(Dodge()); //Only if the character is moving, dodging is allowed.
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && canRope)
        {
            onRope = !onRope;
            
        }
            
    }
    private IEnumerator Dodge()
    {

        animator.SetTrigger("Dodge");
        isDodging = true;
        dodgeTimer = .8f;
        transform.DOMove(transform.position + movement.normalized * dodgeForce, dodgeTimer);
        yield return new WaitForSeconds(dodgeTimer-.1f);
        isDodging = false;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("rope"))
        {
            Vector3 direction = other.gameObject.transform.position - transform.position ;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            canRope = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("rope"))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            canRope = false;
            onRope = false;
        }
    }




    void RecordControls()
    {

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        inputVector = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (onRope)
        {
            animator.SetBool("climb", true);
            inputVector = new Vector3(0, verticalInput, 0).normalized;
        }
        else
        {
            animator.SetBool("climb", false);
        }

    }

    private void FixedUpdate()
    {
        Move();
        lookRotation();
    }

    private void Move()
    {
        if (isDodging) return;
        movement = inputVector * moveSpeed;
        Quaternion cameraRotation = Quaternion.Euler(0, -33, 0);
        movement = cameraRotation * movement;
        if (!onRope)
            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        else
            rb.velocity = movement;
    }
    void lookRotation()
    {
        if (onRope) return;
        float rotspeed;
        if (isDodging)
            rotspeed = 2f;
        else
            rotspeed = 10f;
        if (inputVector != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotspeed);
        }
    }



}
