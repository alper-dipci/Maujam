using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float speed;
    private void Start()
    {
        Destroy(gameObject,5f);
    }
    void Update()
    {
        speed += Time.deltaTime*30f;
        transform.Translate(Vector3.forward*Time.deltaTime*speed, Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player player))
        {
            player.takeDamage(damage);
            //instantiate VFX
            Destroy(gameObject);
        }
    }
}
