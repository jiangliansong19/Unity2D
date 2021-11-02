using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMouse : MonoBehaviour
{
    public static GameMouse Instance;

    /// <summary>
    /// 替代鼠标的图片
    /// </summary>
    public Sprite mouseSprite {
        set
        {
            private_mousePrite = value;
            SpriteRenderer render = GetComponent<SpriteRenderer>();
            render.sprite = private_mousePrite;
        }
        get
        {
            return private_mousePrite;
        }
    }
    private Sprite private_mousePrite;

    /// <summary>
    /// 鼠标原始图片
    /// </summary>
    [SerializeField]
    private Sprite originMousePrite;

    /// <summary>
    /// 鼠标重置为原始图片
    /// </summary>
    public void MousePriteReset()
    {
        this.mouseSprite = originMousePrite;
    }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        this.transform.position = GetWorldPointFromScreenPoint();
    }

    private Vector3 GetWorldPointFromScreenPoint()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        v.z = 0;
        return v;
    }
}
