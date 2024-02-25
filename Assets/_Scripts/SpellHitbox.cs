using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHitbox : MonoBehaviour
{
    [SerializeField] int damage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            player.takeDamage(damage);
        }
    }
}
