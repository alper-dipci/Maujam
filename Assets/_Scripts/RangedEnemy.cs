using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public override void hit()
    {
        isHitting = true;
        canHit = false;
        timer = timeBetweenHits;
        Debug.Log("hitRanged");
    }
}
