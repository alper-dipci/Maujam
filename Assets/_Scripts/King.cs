using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Enemy
{
    [SerializeField] List<GameObject> SkillVfx = new List<GameObject>();
    [SerializeField] List<GameObject> skillIndicator = new List<GameObject>();
    [SerializeField] private float indicatorTime;
    [SerializeField] private float skillLifeTime;
    [SerializeField] LevelManager levelManager;

    Vector3 skillPos;
    public override void hit()
    {
        // EGER KACIRIRSA UPDATE ICINDEN AL POZÝSYONU
        skillPos = playerGameobject.transform.position;

        int skillNumber = Random.Range(1, SkillVfx.Count + 1);
        isHitting = true;
        canHit = false;
        timer = timeBetweenHits;
        animator.SetLayerWeight(1, 1);
        animator.SetTrigger("hit" + skillNumber);
        animator.SetBool("isWalk", false);
        Debug.Log(skillNumber);
        doSkill(skillNumber);
    }
    public override void die()
    {
        GetComponent<Collider>().enabled = false;
        dead = true;
        agent.SetDestination(transform.position);
        animator.SetTrigger("death");
        StartCoroutine(loadlevel());

    }
    private IEnumerator loadlevel()
    {
        yield return new WaitForSeconds(2.2f);
        levelManager.LoadLevel("InCastleDialog");
    }
    private void doSkill(int skillNumber)
    {
        switch (skillNumber)
        {
            case 1:
                StartCoroutine(skill1());
                break;
            case 2:
                StartCoroutine(skill2());
                break;
            case 3:
                StartCoroutine(skill3());
                break;
        }
    }
    private IEnumerator skill1()
    {
        GameObject indicator = Instantiate(skillIndicator[0], skillPos, Quaternion.identity);
        yield return new WaitForSeconds(indicatorTime);
        Destroy(indicator);
        AudioSourceManager.Instance._sounds[8].Play();
        GameObject skill = Instantiate(SkillVfx[0], skillPos, Quaternion.identity);
        Destroy(skill, skillLifeTime);
    }
    private IEnumerator skill2()
    {
        GameObject indicator = Instantiate(skillIndicator[1], skillPos, Quaternion.identity);
        yield return new WaitForSeconds(indicatorTime);
        Destroy(indicator);
        AudioSourceManager.Instance._sounds[9].Play();
        GameObject skill = Instantiate(SkillVfx[1], skillPos, Quaternion.identity);
        Destroy(skill, skillLifeTime);
    }
    private IEnumerator skill3()
    {
        skillPos = transform.position;
        GameObject indicator = Instantiate(skillIndicator[2], skillPos, Quaternion.identity);
        yield return new WaitForSeconds(indicatorTime);
        AudioSourceManager.Instance._sounds[10].Play();
        Destroy(indicator);
        GameObject skill = Instantiate(SkillVfx[2], skillPos, Quaternion.identity);
    }
}
