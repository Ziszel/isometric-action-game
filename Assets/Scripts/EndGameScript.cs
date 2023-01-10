using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour
{
    public Button returnToMenu;

    public void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        Button returnToMenuBtn = returnToMenu.GetComponent<Button>();
        returnToMenuBtn.onClick.AddListener(ResetGame);
    }

    private void ResetGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
