using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public Button playBtn;
    //public Button infoBtn;
    public Button langBtn;
    public Sprite enLangSprite;
    public Sprite cnLangSprite;
    public Button soundBtn;
    public Image langImg;
    // private
    private static bool soundOn;
    public static string Language;

    // Start is called before the first frame update
    void Start()
    {
        Button playButton = playBtn.GetComponent<Button>();
        playButton.onClick.AddListener(StartGame);
        langBtn.onClick.AddListener(OnLanguageClick);
        Language = "EN";
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Language);
    }

    public void OnLanguageClick()
    {
        if (Language.Equals("EN"))
        {
            Language = "CN";
            langImg.sprite = cnLangSprite;
        }
        else if (Language.Equals("CN"))
        {
            Language = "EN";
            langImg.sprite = enLangSprite;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LevelOne");
    }
}
