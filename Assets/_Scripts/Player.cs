using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.Burst.CompilerServices;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }


    [SerializeField] private int health = 100;

    [SerializeField] private CinemachineVirtualCamera CMVCam;
    [SerializeField] private LayerMask EnemyLayerMask;
    Vector3 closestEnemy;

    private float _shakeTimer;
    private float _startingInstentitiy;

    [SerializeField] private float shakeMagnitude;
    PlayerAnimator playerAnimator;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

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

        if (Input.GetKeyDown(KeyCode.E))
        {
            Transform characterTransform = transform;

            Vector3 forward = characterTransform.forward;


            Vector3 rayOrigin = characterTransform.position;


            Ray ray = new Ray(rayOrigin, forward);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 3f))
            {

                if (hit.collider.TryGetComponent(out IInteractable interactObject))
                {
                    interactObject.Interact();
                }


                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1f);
            }
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
        playerAnimator.getHit();
        health -= amount;
        ShakeCamera(shakeMagnitude, .1f);
        Collider[] enemys = Physics.OverlapSphere(transform.position, 12f, EnemyLayerMask);
        foreach (Collider col in enemys)
        {
            if (Vector3.Distance(col.transform.position, transform.position) < closestEnemy.magnitude)
            {
                Debug.Log("cchangesd");
                closestEnemy = col.transform.position;
            }
        }
        //Instantiate(deneme, closestEnemy, Quaternion.identity);
        Vector3 direction = closestEnemy - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);

        if (health <= 0)
            die();
    }
    private void die()
    {
    }



}

