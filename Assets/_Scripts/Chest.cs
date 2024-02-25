using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
        _chestKapak.transform.DORotate(new Vector3(_chestKapak.transform.rotation.x, -180, -60), 2f);
        _chestParticle.Play();
        AudioSourceManager.Instance._sounds[5].Play();

    }

}
