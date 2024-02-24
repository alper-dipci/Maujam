using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour, IInteractable
{
    public Transform _targetPos;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void Interact()
    {
        FlyBird();

    }

    public void FlyBird()
    {
        gameObject.transform.DOMove(_targetPos.position, 9f).OnComplete(() =>
        {
            _animator.SetBool("flying", false);
        });
        _animator.SetBool("flying", true);



    }
}
