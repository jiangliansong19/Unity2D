using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;

    private Transform barTransform;

    private void Awake()
    {
        Debug.Log("ResourceGeneratorOverlay awake");
    }

    // Start is called before the first frame update
    void Start()
    {
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();

        barTransform = transform.Find("Bar");

        transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;

        TextMeshPro textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
        textMeshPro.SetText(resourceGenerator.GetAmountGeneratorPerSecond().ToString("F1"));
    }

    // Update is called once per frame
    void Update()
    {
        barTransform.localScale = new Vector3(resourceGenerator.GetTimerNormalized(), 1, 1);
    }
}
