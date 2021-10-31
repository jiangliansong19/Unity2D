using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Button startButton;
    private GameObject startPanel;

    private GameObject tipsPanel;
    private Text gameLevelText;
    private Text targetFractionText;
    private Text currentFractionText;
    private Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        startPanel = transform.Find("StartPanel").gameObject;
        startPanel.SetActive(true);
        startButton = transform.Find("StartPanel/Start").GetComponent<Button>();
        startButton.onClick.AddListener(StartPlayGame);


        tipsPanel = transform.Find("TipsPanel").gameObject;
        gameLevelText = transform.Find("TipsPanel/GameLevel").GetComponent<Text>();
        targetFractionText = transform.Find("TipsPanel/TargetFraction").GetComponent<Text>();
        currentFractionText = transform.Find("TipsPanel/CurrentFraction").GetComponent<Text>();
        timeText = transform.Find("TipsPanel/TimeText").GetComponent<Text>();
        tipsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        updateCurrentFraction(GameMode.Instance.curFraction);
        updateTime(GameMode.Instance.timeSeconds[GameMode.Instance.gameLevel] - Time.deltaTime);
    }

    void StartPlayGame()
    {
        

        startPanel.SetActive(false);
        tipsPanel.SetActive(true);

        GameMode.Instance.isPlaying = true;
        GameMode.Instance.StartPlaying();

        updateGameLevel(GameMode.Instance.gameLevel);
        updateTargeFraction(GameMode.Instance.targetFraction[GameMode.Instance.gameLevel]);
        

    }

    public void updateTargeFraction(int value)
    {
        targetFractionText.text = "目标分数: " + value.ToString();
    }

    public void updateCurrentFraction(int value)
    {
        currentFractionText.text = "当前分数: " + value.ToString();
    }

    public void updateGameLevel(int value)
    {
        gameLevelText.text = "关卡: " + value.ToString();
    }

    public void updateTime(float value)
    {
        timeText.text = "时间: " + ((int)value).ToString();
    }
}
