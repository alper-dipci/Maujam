using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private Animator anim;
    public int noOfClicks = 0;
    public int currentStateCount;
    float lastClickedTime = 0;
    float maxComboDelay = 2;
    public float delay1;
    private Rigidbody rb;
    [SerializeField] BoxCollider swordCollider;
    [SerializeField] TrailRenderer swordTrail;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetFloat("Speed", rb.velocity.magnitude);


        if (Time.time - lastClickedTime > maxComboDelay)
        {
            currentStateCount = 0;
            anim.SetLayerWeight(1, 0);
            swordTrail.enabled = false;
            swordCollider.enabled = false;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetLayerWeight(1, 1);
            swordTrail.enabled = true;
            swordCollider.enabled = true;
            onAttackClick();
        }


    }
    public void getHit()
    {
        ResetAllTriggers();
        anim.SetTrigger("getHit");
    }
    private void ResetAllTriggers()
    {
        foreach (var param in anim.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
            {
                anim.ResetTrigger(param.name);
            }
        }
    }
    void onAttackClick()
    {
        if (Time.time - lastClickedTime < delay1) return;

        lastClickedTime = Time.time;
        if (currentStateCount >= 2)
            currentStateCount = 0;
        currentStateCount++;
        anim.SetTrigger("hit" + currentStateCount);

        AudioSourceManager.Instance._sounds[currentStateCount].Play();
    }
}
