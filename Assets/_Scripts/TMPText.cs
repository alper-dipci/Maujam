using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TMPText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _keyText;

    public static int keyCount = 0;

    private void Start()
    {
        keyCount = 0;
    }

    private void Update()
    {
        _keyText.text = keyCount + "/3".ToString();
    }

}
