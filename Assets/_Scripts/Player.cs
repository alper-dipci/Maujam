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


    private void Awake()
    {
        CMVCam = GetComponent<CinemachineVirtualCamera>();
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("shake");
            ShakeCamera(5f, .1f);
        }

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
        if (health <= 0)
            die();
    }
    private void die()
    {
        Debug.Log("PlayerDEAD");
    }


}

