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

    private float dismissTimer = 2f;

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



        dismissTimer -= Time.deltaTime;
        if (dismissTimer <= 0)
        {
            Hidden();
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

        if (showTimer != null)
        {
            dismissTimer = showTimer.time;
        }
        else
        {
            dismissTimer = 2f;
        }
    }

    public class ShowTimer
    {
        public float time = 2f;
    }
}
