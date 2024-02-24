using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _chestKapak;
    public void Interact()
    {
        OpenChestKapak();
    }

    public void OpenChestKapak()
    {
        _chestKapak.transform.DORotate(new Vector3(_chestKapak.transform.rotation.x, _chestKapak.transform.rotation.y, 45), .2f);

    }

}
