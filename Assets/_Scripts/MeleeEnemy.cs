using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] GameObject Sword;
    private Collider swordCollider;
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
        //animator.SetTrigger("hit");
        swordCollider.enabled=true;
    }
    public void onAnimatonEnd()
    {
        swordCollider.enabled = false;
    }
}
