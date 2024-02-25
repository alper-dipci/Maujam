using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.Burst.CompilerServices;
using UnityEngine.SceneManagement;

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

    [SerializeField] LevelManager _levelManager;

    [SerializeField] GameObject[] _hearts;

    public bool _skill1, _skill2, _skill3
    = false;

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

                    if (hit.collider.TryGetComponent(out EnumSkills _skil))
                    {
                        Skills skillType = _skil.skil;

                        // Hangi yetenek olduğunu k ol edebilirsiniz
                        switch (skillType)
                        {
                            case Skills.skill1:

                                _skill1 = true;
                                TMPText.keyCount++;
                                AudioSourceManager.Instance._sounds[11].Play();
                                break;
                            case Skills.skill2:
                                TMPText.keyCount++;
                                AudioSourceManager.Instance._sounds[11].Play();
                                _skill2 = true;
                                break;
                            case Skills.skill3:
                                TMPText.keyCount++;
                                AudioSourceManager.Instance._sounds[11].Play();
                                _skill3 = true;
                                break;
                            default:
                                break;
                        }
                    }

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
        AudioSourceManager.Instance._sounds[4].Play();
        playerAnimator.getHit();
        health -= amount;

        if (health <= 100)
        {
            _hearts[0].SetActive(false);
        }
        if (health <= 50)
        {
            _hearts[1].SetActive(false);
        }
        if (health <= 0)
        {
            _hearts[2].SetActive(false);
            die();
        }




        ShakeCamera(shakeMagnitude, .1f);
        Collider[] enemys = Physics.OverlapSphere(transform.position, 12f, EnemyLayerMask);
        foreach (Collider col in enemys)
        {
            if (Vector3.Distance(col.transform.position, transform.position) < closestEnemy.magnitude)
            {

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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("dieLine"))
        {
            die();
        }

        if (other.gameObject.CompareTag("castleScene") && TMPText.keyCount == 3)
        {
            SceneManager.LoadScene("casteInFight");
        }
    }
    private void die()
    {
        TMPText.keyCount = 0;
        _levelManager.RestartGame();
    }



}

