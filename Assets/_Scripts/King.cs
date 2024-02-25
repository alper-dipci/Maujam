using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Enemy
{
    [SerializeField] List<GameObject> SkillVfx = new List<GameObject>();
    public override void hit()
    {
        int skillNumber = Random.Range(1, SkillVfx.Count);
        isHitting = true;
        canHit = false;
        timer = timeBetweenHits;
        animator.SetLayerWeight(1, 1);
        animator.SetTrigger("hit"+ skillNumber);
        animator.SetBool("isWalk", false);

        doSkill(skillNumber);
    }
    private void doSkill(int skillNumber)
    {
        switch (skillNumber)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }
}
