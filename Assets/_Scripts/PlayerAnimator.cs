using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

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

        if (Input.GetMouseButtonDown(0))
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
        if (Time.time- lastClickedTime < delay1) return;
        
        lastClickedTime = Time.time;
        if (currentStateCount >= 2)
            currentStateCount = 0;
        currentStateCount++;
        anim.SetTrigger("hit" + currentStateCount);
        //noOfClicks++;
        //if (noOfClicks >= 1)
        //{
        //    Debug.Log("set1treue");
        //    anim.SetBool("hit1", true);
        //}
        //noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        //if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > delay2 && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        //{
        //    Debug.Log("set2treue");
        //    anim.SetBool("hit1", false);
        //    anim.SetBool("hit2", true);
        //}
        //if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > delay2 && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        //{
        //    Debug.Log("set3treue");
        //    anim.SetBool("hit2", false);
        //    anim.SetBool("hit3", true);
        //}
    }
}
