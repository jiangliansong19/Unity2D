using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishBtn : MonoBehaviour
{
    [SerializeField] private BuildingTypeHolder typeHolder;

    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(ClickOnDemolishButton);
    }

    private void ClickOnDemolishButton()
    {
        Debug.Log("ClickOnDemolishButton");
    }
}
