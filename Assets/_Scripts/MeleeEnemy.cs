using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] GameObject Sword;
    private Collider swordCollider;
    public float animtime;
    protected override void Start()
    {
        base.Start();
        swordCollider = Sword.GetComponent<Collider>();
        swordCollider.enabled = false;
    }
    public override void hit()
    {
        isHitting = true;
        canHit = false;
        timer = timeBetweenHits;
        animator.SetLayerWeight(1, 1);
        animator.SetTrigger("hit");       
        animator.SetBool("isWalk",false);
        swordCollider.enabled=true;
        StartCoroutine(onAnimatonEnd());
    }
    public IEnumerator onAnimatonEnd()
    {
        yield return new WaitForSeconds(animtime);
        animator.SetBool("isWalk", true);
        animator.SetLayerWeight(1, 0);
        swordCollider.enabled = false;
    }
}
