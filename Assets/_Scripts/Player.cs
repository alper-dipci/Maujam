using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 100;

    [SerializeField] private CinemachineVirtualCamera CMVCam;
    [SerializeField] private LayerMask EnemyLayerMask;
    Vector3 closestEnemy;

    private float _shakeTimer;
    private float _startingInstentitiy;

    [SerializeField] private float shakeMagnitude;
    PlayerAnimator playerAnimator;


    private void Start()
    {
        closestEnemy = Vector3.one * 100;
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    public void ShakeCamera(float intensitiy, float timer)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = CMVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensitiy;
        _startingInstentitiy = intensitiy;

        _shakeTimer = timer;
    }

    private void Update()
    {


        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;

            if (_shakeTimer <= 0f)
            {

                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = CMVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;

            }

        }


    }


    public void takeDamage(int amount)
    {
        playerAnimator.getHit();
        health -= amount;
        ShakeCamera(shakeMagnitude, .1f);
        Collider[] enemys = Physics.OverlapSphere(transform.position,12f,EnemyLayerMask);
        foreach(Collider col in enemys)
        {
            if (Vector3.Distance(col.transform.position, transform.position) < closestEnemy.magnitude)
            {
                Debug.Log("cchangesd");
                closestEnemy = col.transform.position;
            }
        }
        Instantiate(deneme, closestEnemy, Quaternion.identity);
        Vector3 direction = closestEnemy - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);

        if (health <= 0)
            die();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(closestEnemy, Vector3.one);
    }
    private void die()
    {
    }

}

