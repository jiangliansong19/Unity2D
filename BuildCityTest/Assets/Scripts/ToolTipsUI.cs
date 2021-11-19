using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTipsUI : MonoBehaviour
{
    public static ToolTipsUI Instance { private set; get; }

    [SerializeField] private Transform parentCanvas;

    private TextMeshProUGUI textGUI;
    private RectTransform backgroundTransform;
    private RectTransform rectTransform;

    private ShowTimer showTimer;

    private void Awake()
    {
        Instance = this;
        textGUI = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        backgroundTransform = transform.Find("Background").GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();

        Hidden();
    }

    private void Update()
    {
        rectTransform.anchoredPosition = Input.mousePosition / parentCanvas.localScale.x;


        if (showTimer != null)
        {
            showTimer.time -= Time.deltaTime;
            if (showTimer.time <= 0)
            {
                Hidden();
            }
        }


    }

    public void Hidden()
    {
        gameObject.SetActive(false);
    }

    public void ShowMessage(string message, ShowTimer showTimer = null)
    {
        gameObject.SetActive(true);
        textGUI.text = message;

        Vector2 size = textGUI.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        backgroundTransform.sizeDelta = size + padding;

        this.showTimer = showTimer;
    }

    public class ShowTimer
    {
        public float time = 2f;
    }
}
