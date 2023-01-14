using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameScript : MonoBehaviour
{
    public Button returnToMenu;
    public TMP_Text endGameMessage;
    public AudioSource backgroundMusic;

    private string english;

    public void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        endGameMessage.text = (GameManager.Language.Equals("EN"))
            ? "Pan has successfully watered the World Flowers and brought peace to the world." +
            "\n\nWhen will the next threat arise? Who knows. Pan will be ready."
            : "世界之花被帕恩成功拯救，世界也因此恢复平衡" + 
              "谁也不知道下一次的危机会什么时候出现。帕恩已经准备好了迎接下一次的挑战。";
        
        Button returnToMenuBtn = returnToMenu.GetComponent<Button>();
        returnToMenuBtn.onClick.AddListener(ResetGame);

        if (GameManager.soundOn)
        {
            backgroundMusic.Play();
        }
        else
        {
            backgroundMusic.Stop();
        }
    }

    private void ResetGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
