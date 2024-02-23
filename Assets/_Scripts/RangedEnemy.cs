using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] GameObject Arrow;
    [SerializeField] Transform ArrowPosition;

    public override void hit()
    {
        isHitting = true;
        canHit = false;
        timer = timeBetweenHits;
        GameObject arrow =  Instantiate(Arrow,ArrowPosition.position,Quaternion.identity);
        Vector3 lookPos = playerGameobject.transform.position - arrow.transform.position;
        arrow.transform.rotation = Quaternion.LookRotation(lookPos);
        Debug.Log("hitRanged");
    }
}
