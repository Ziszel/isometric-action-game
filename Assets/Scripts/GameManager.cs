using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Buttons")]
    public Button playBtn;
    public Button infoBtn;
    public Button langBtn;
    public Button backBtn;
    public Button AudioBtn;
    [Header("Sprites & Image")]
    public Sprite enLangSprite;
    public Sprite cnLangSprite;
    public Sprite audOnSprite;
    public Sprite audOffSprite;
    public Image langImg;
    public Image audImg;
    [Header("Text")]
    public TMP_Text playBtnText;
    public TMP_Text infoBtnText;
    public TMP_Text InfoScreenText;
    public AudioSource music;
    private AudioSource playBtnAudio;
    private AudioSource infoBtnAudio; 

    // public variables not set in inspector
    public static bool soundOn;
    public static string Language;
    public GameObject ghost;
    // private

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        soundOn = true;
        Language = "EN";
        var text = playBtn.GetComponentInChildren<Text>();
        Button playButton = playBtn.GetComponent<Button>();
        Button infoButton = infoBtn.GetComponent<Button>();
        Button backButton = backBtn.GetComponent<Button>();
        playButton.onClick.AddListener(StartGame);
        langBtn.onClick.AddListener(OnLanguageClick);
        AudioBtn.onClick.AddListener(OnAudioClick);
        infoBtn.onClick.AddListener(ShowInfo);
        backBtn.onClick.AddListener(HideInfo);
        music = GetComponent<AudioSource>();
        playBtnAudio = playBtn.GetComponent<AudioSource>();
        infoBtnAudio = infoBtn.GetComponent<AudioSource>();
    }

    public void OnLanguageClick()
    {
        if (Language.Equals("EN"))
        {
            Language = "CN";
            langImg.sprite = cnLangSprite;
            playBtnText.text = "开始";
            infoBtnText.text = "介绍";
            InfoScreenText.text = "世界上的花朵已经干枯... 现在环境失去了平衡，黑夜和白天不断地相互争斗，谁都想要成为掌控世界的那一个，使日夜循环都失去了意义。\n\n" +
                "只有传说中的亡灵园丁“帕恩”可以拯救世界，为世界的花朵带来生命之水，恢复平衡。" + 
                "控制方法:\n" +
                "W、A、S、D - 移动\n" +
                "鼠标: 旋转摄像机\n" +
                "空格键: 跳跃（在空中再按一次，就可以跳两次！)"; 

        }
        else if (Language.Equals("CN"))
        {
            Language = "EN";
            langImg.sprite = enLangSprite;
            playBtnText.text = "PLAY";
            infoBtnText.text = "INFO";
            InfoScreenText.text = "The flowers of the world have dried up... The environment is now out of balance, night and day are constantly fighting each other, " +
                "and whoever wants to be the one in control of the world has rendered both the day and night cycles meaningless. Only Pan, the legendary undead gardener, " +
                "can save the world, bring the water of life to its flowers and restore balance. \n\n" +
                "Control methods.\nW, A, S, D - Move \nMouse - Rotate the camera\nSpacebar - Jump(press again in the air to jump twice!).";
        }
    }

    public void OnAudioClick()
    {
        if (soundOn)
        {
            soundOn = false;
            audImg.sprite = audOffSprite;
            music.Stop();
        }
        else if (!soundOn)
        {
            soundOn = true;
            audImg.sprite = audOnSprite;
            music.Play();
        }
    }

    private void StartGame()
    {
        infoBtnAudio.Play();
        SceneManager.LoadScene("LevelTwo");
    }

    private void ShowInfo()
    {
        infoBtnAudio.Play();
        // hide main menu and show info elements
        playBtn.gameObject.SetActive(false);
        infoBtn.gameObject.SetActive(false);
        ghost.gameObject.SetActive(false);
        backBtn.gameObject.SetActive(true);
        InfoScreenText.gameObject.SetActive(true);
    }

    private void HideInfo()
    {
        // hide info and show main menu elements
        playBtn.gameObject.SetActive(true);
        infoBtn.gameObject.SetActive(true);
        ghost.gameObject.SetActive(true);
        backBtn.gameObject.SetActive(false);
        InfoScreenText.gameObject.SetActive(false);
        infoBtnAudio.Play();
    }
}
