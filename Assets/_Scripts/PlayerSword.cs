using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    [SerializeField] private GameObject ArrowVFX;
    [SerializeField] private GameObject hitEnemyVFX;
    [SerializeField] private int damage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Arrow arrow))
        {
            Instantiate(ArrowVFX, transform.position, Quaternion.identity);
            Destroy(arrow.gameObject);
        }
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.getHit(damage);
            Instantiate(hitEnemyVFX, transform.position, Quaternion.identity);
        }

    }

}
