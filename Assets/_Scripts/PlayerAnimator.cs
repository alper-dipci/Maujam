using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    private Animator anim;
    [SerializeField] private float cooldownTime = 2;
    private float nextFireTime = 0;
    public int noOfClicks = 0;
    public int currentStateCount;
    float lastClickedTime = 0;
    float maxComboDelay = 2;
    public float delay1;
    public float delay2;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > delay1 && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        //{
        //    anim.SetBool("hit1", false);

        //}
        //if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > delay1 && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        //{
        //    anim.SetBool("hit2", false);

        //}
        //if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > delay1 && anim.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        //{
        //    anim.SetBool("hit3", false);
        //    noOfClicks = 0;
        //}

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            currentStateCount = 0;
            anim.SetLayerWeight(0, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetLayerWeight(1, 1);
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
        if (currentStateCount >= 3)
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
