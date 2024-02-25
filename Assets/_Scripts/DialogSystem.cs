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
    [SerializeField] UiFadeOut uiFadeOut;

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
                StartCoroutine(gameOver());
            else
            {
                text.text = texts[countIndex].text;
                _kingCam.SetActive(!_kingCam.active);
            }
        }


    }
    private IEnumerator gameOver()
    {
        uiFadeOut.FadeOut();
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }
}
