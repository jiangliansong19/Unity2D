using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTipsUI : MonoBehaviour
{
    public static ToolTipsUI Instance { private set; get; }

    [SerializeField] RectTransform canvasRectTransform;

    private RectTransform rectTransform;

    private TextMeshProUGUI textMeshPro;

    private RectTransform backgroundRectTransform;

    private ToolTipsUITimer time;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();

        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();

        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }

        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPosition;

        if (time != null)
        {
            time.timer -= Time.deltaTime;
            if (time.timer <= 0)
            {
                Hide();
            }
        }
    }

    private void SetText(string tooltipText)
    {
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = textSize + padding;
    }

    public void Show(string message, ToolTipsUITimer time = null)
    {
        gameObject.SetActive(true);

        SetText(message);

        if (time != null)
        {
            this.time = time;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public class ToolTipsUITimer {
        public float timer;
    }
}
