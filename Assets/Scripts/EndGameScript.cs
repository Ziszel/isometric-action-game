using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour
{
    public Button returnToMenu;

    public void Awake()
    {
        Button returnToMenuBtn = returnToMenu.GetComponent<Button>();
        returnToMenuBtn.onClick.AddListener(ResetGame);
    }

    private void ResetGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
