using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _targetRock;
    public void Interact()
    {
        MoveRock();
    }

    public void MoveRock()
    {
        Debug.Log("Kaya");

        // Hedef rotasyonu belirtmek için örnek bir Quaternion kullanalım
        Quaternion targetRotation = Quaternion.Euler(360f, 0f, 0f);

        // DoTween ile hem konumu hem de rotasyonu değiştirelim
        this.gameObject.transform.DOMove(_targetRock.position, 2f);
        this.gameObject.transform.DORotate(targetRotation.eulerAngles, 2f);
    }


}
