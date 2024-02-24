using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

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
    [SerializeField] ParticleSystem skill1;
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("skil1");
            anim.SetLayerWeight(1, 1);
            swordTrail.enabled = true;
            swordCollider.enabled = true;
            lastClickedTime = Time.time;
            anim.SetTrigger("skill1");
            skill1.Play();
            transform.DORotate(Vector3.up * 720, 1f, RotateMode.FastBeyond360);
        }

    }

    private object stopVFX()
    {
        throw new NotImplementedException();
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
    }
}
