using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float positionOffsetY;
    [SerializeField] private bool runOnce;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        float multiple = 5f;
        spriteRenderer.sortingOrder = (int)(-(transform.position.y + positionOffsetY) * multiple);

        if (runOnce)
        {
            Destroy(this);
        }
    }
}
