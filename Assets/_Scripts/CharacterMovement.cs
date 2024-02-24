using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class CharacterMovement : MonoBehaviour {
    [SerializeField] private float moveSpeed = 1.5f;

    private Animator animator;
    private Rigidbody rb;

    bool isDodging;
    float dodgeTimer, dodge_coolDown;
    AudioSource audioSource;
    [SerializeField] AudioClip dodgeAudio;
    [SerializeField] private float dodgeForce;

    Vector3 inputVector;
    public Vector3 movement;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera vcam;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RecordControls();

        CharacterAnimation();
        if (dodge_coolDown > 0) dodge_coolDown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (dodge_coolDown > 0) return;
            if (inputVector.magnitude != 0)
            {
                StartCoroutine(Dodge()); //Only if the character is moving, dodging is allowed.
            }

        }
    }
    private IEnumerator  Dodge()
    {
        
        //animator.SetTrigger("Dodge");

        isDodging = true;
        rb.AddForce(movement.normalized*dodgeForce, ForceMode.Force);
        Debug.Log(movement.normalized * dodgeForce);

        yield return new WaitForSeconds(.1f);

        isDodging = false;

    }

    void RecordControls()
    {

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");


        inputVector = new Vector3(horizontalInput, 0f, verticalInput).normalized;

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
        rb.velocity = movement;
    }
    void lookRotation()
    {
        float rotspeed;
        if (isDodging)
            rotspeed = 3f;
        else
            rotspeed = 10f;
        if (inputVector != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotspeed);
        }
    }
    private void flip()
    {

        if (inputVector.magnitude > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("flip", true);
            rb.AddForce(inputVector * Time.fixedDeltaTime);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("flip", false);
        }
    }
    public void CharacterAnimation()
    {
        if (inputVector != Vector3.zero)
        {
            animator.SetBool("walk", true);
        }

        else
        {
            animator.SetBool("walk", false);
        }
    }

}
