using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiFadeOut : MonoBehaviour
{
    [SerializeField] CanvasGroup canvas;

    public float speed = 2;
    public bool fadeOut;

    private void Update()
    {
        if (fadeOut)
            canvas.alpha += Time.deltaTime * speed;
    }
    public void FadeOut()
    {
        fadeOut = true;
    }

}
