using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour
{
    public List<Sprite> sprites;
    public Image image;
    private int spriteIndex=0;
    [SerializeField] private LevelManager levelManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            spriteIndex++;
            if(spriteIndex >= sprites.Count)
            {
                levelManager.LoadLevel("AlperScene");
            }
            else
                image.sprite = sprites[spriteIndex];
            
        }
    }
}
