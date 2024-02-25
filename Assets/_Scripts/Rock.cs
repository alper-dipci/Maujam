using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _targetRock;
    [SerializeField] private Transform _birdTarget;

    [SerializeField] private GameObject _bird;
    [SerializeField] private Animator _birdAnimator;


    private void Start()
    {
        _birdAnimator.SetBool("die", true);
    }
    public void Interact()
    {
        MoveRock();
        FlyBird();
    }
    private void Update()
    {
        if (_bird.transform.position == _birdTarget.position)
        {
            _birdAnimator.SetTrigger("idle");
        }
    }
    public void MoveRock()
    {
        Debug.Log("Kaya");


        Quaternion targetRotation = Quaternion.Euler(360f, 0f, 0f);

        this.gameObject.transform.DOMove(_targetRock.position, 1f);
        this.gameObject.transform.DORotate(targetRotation.eulerAngles, 2f);
    }

    public void FlyBird()
    {
        _birdAnimator.SetBool("die", false);

        _bird.transform.DOMove(_birdTarget.position, 15f).OnComplete(() =>
        {
            _birdAnimator.SetBool("flying", false);
        });
        _birdAnimator.SetBool("flying", true);

        AudioSourceManager.Instance._sounds[6].Play();

    }


}
