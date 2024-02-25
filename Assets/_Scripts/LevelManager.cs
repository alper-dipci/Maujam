using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int sceneIndex; // sceneIndex değişkenini sınıfın üyesi olarak tanımladık

    private void Start()
    {
        // Şu an aktif olan sahnenin indeksini alıp sceneIndex'e atıyoruz
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void RestartGame()
    {
        Debug.Log("öldü");
        SceneManager.LoadScene(sceneIndex);
    }
}
