using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{

    public Button playBtn;
    public Button infoBtn;
    public Button langBtn;
    public Button backBtn;
    public Sprite enLangSprite;
    public Sprite cnLangSprite;
    public Image langImg;
    public TMP_Text playBtnText;
    public TMP_Text infoBtnText;
    public TMP_Text InfoScreenText;
    public Button AudioBtn;
    public Sprite audOnSprite;
    public Sprite audOffSprite;
    public Image audImg;
    public static bool soundOn;
    public static string Language;
    public GameObject ghost;
    // private


    // Start is called before the first frame update
    private void Start()
    {
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
    }

    public void OnLanguageClick()
    {
        if (Language.Equals("EN"))
        {
            Language = "CN";
            langImg.sprite = cnLangSprite;
            playBtnText.text = "��ʼ";
            infoBtnText.text = "����";
            InfoScreenText.text = "�����ϵĻ����Ѿ��ɿ�... ���ڻ���ʧȥ��ƽ�⣬��ҹ�Ͱ��첻�ϵ��໥������˭����Ҫ��Ϊ�ƿ��������һ����ʹ��ҹѭ����ʧȥ�����塣" +
                "ֻ�д�˵�е�����԰���������������������磬Ϊ����Ļ����������֮ˮ���ָ�ƽ�⡣\n\n" +
                "���Ʒ���:\nW��A��S��D - \n�ƶ���� : ��ת�����\n�ո�� : ��Ծ���ڿ����ٰ�һ�Σ��Ϳ��������Σ���";

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
        }
        else if (!soundOn)
        {
            soundOn = true;
            audImg.sprite = audOnSprite;
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("LevelOne");
    }

    private void ShowInfo()
    {
        // hide main menu elements
        playBtn.gameObject.SetActive(false);
        infoBtn.gameObject.SetActive(false);
        ghost.gameObject.SetActive(false);
        backBtn.gameObject.SetActive(true);
        InfoScreenText.gameObject.SetActive(true);
        // show info elements
    }

    private void HideInfo()
    {
        // hide info elements
        playBtn.gameObject.SetActive(true);
        infoBtn.gameObject.SetActive(true);
        ghost.gameObject.SetActive(true);
        backBtn.gameObject.SetActive(false);
        InfoScreenText.gameObject.SetActive(false);
        // show main menu elements
    }
}
