using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{

    public GameObject _kingCam;

    [SerializeField] private List<TextMeshProUGUI> texts;
    [SerializeField] private TextMeshProUGUI text;

    int countIndex = 0;

    private void Start()
    {
        text.text = texts[countIndex].text;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) { 
            countIndex++;
            if (countIndex >= texts.Count)
                Debug.Log("oyun bitti");
            else
            {
                
                text.text = texts[countIndex].text;
                _kingCam.SetActive(!_kingCam.active);
            }

        }


    }
}
