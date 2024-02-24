using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _chestKapak;
    [SerializeField] ParticleSystem _chestParticle;

    public void Interact()
    {
        OpenChestKapak();
    }

    public void OpenChestKapak()
    {
        _chestKapak.transform.DORotate(new Vector3(-90f, 90, -90), 2f);
        _chestParticle.Play();
        

    }

}
