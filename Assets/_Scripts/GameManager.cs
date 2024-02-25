using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Image[] _images;

    private void Update()
    {
        if (Player.Instance._skill1 || Player.Instance._skill2 || Player.Instance._skill3)
        {
            Color currentColor = _images[0].color;
            currentColor.a = 255f; // Set alpha to 1 (fully opaque)
            _images[0].color = currentColor;
        }
    }
}
