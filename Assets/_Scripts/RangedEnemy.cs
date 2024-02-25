using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] GameObject Arrow;
    [SerializeField] Transform ArrowPosition;
    private float playerHeight=2f;

    public override void hit()
    {
        isHitting = true;
        canHit = false;
        timer = timeBetweenHits;
        animator.SetLayerWeight(1, 1);
        animator.SetTrigger("hit");
        GameObject arrow =  Instantiate(Arrow,ArrowPosition.position,Quaternion.identity);
        Vector3 lookPos = playerGameobject.transform.position+Vector3.up* playerHeight - arrow.transform.position;
        arrow.transform.rotation = Quaternion.LookRotation(lookPos);
    }
}
