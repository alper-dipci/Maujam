using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 100;

    [SerializeField] private CinemachineVirtualCamera CMVCam;

    private float _shakeTimer;
    private float _startingInstentitiy;

    [SerializeField] private float shakeMagnitude;




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
        health -= amount;
        ShakeCamera(shakeMagnitude, .1f);
        if (health <= 0)
            die();
    }
    private void die()
    {
        Debug.Log("PlayerDEAD");
    }


}

