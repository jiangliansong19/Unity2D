using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class BuildMenuUI : MonoBehaviour
{
    private ArchitectureListSO architectureList;

    private float padding = 10f;
    private float width = 40f;
    private Transform buttonTemplate;

    private List<Transform> tmpTransforms;

    private void Awake()
    {
        architectureList = Resources.Load<ArchitectureListSO>(typeof(ArchitectureListSO).Name);
        tmpTransforms = new List<Transform>();

        buttonTemplate = transform.Find("BuildMenuItem");
        buttonTemplate.gameObject.SetActive(false);

        ShowArchitectureListPanel();
    }

    private void ShowArchitectureListPanel()
    {
        int i = 0;
        transform.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(-architectureList.list.Count * (width + padding) / 2, width / 2 + padding);

        foreach (ArchitectureSO aso in architectureList.list)
        {
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);

            buttonTransform.Find("Icon").GetComponent<Image>().sprite = aso.prefab.GetComponent<SpriteRenderer>().sprite;


            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * (width + padding), 0);

            buttonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (tmpTransforms.Count > 0)
                {
                    foreach (Transform item in tmpTransforms)
                    {
                        Destroy(item.gameObject);
                    }
                    tmpTransforms = new List<Transform>();
                }

                OnClickOnArchitecture(aso);
            });

            i++;
        }
    }

    private void OnClickOnArchitecture(ArchitectureSO so)
    {
        int i = 0;
        foreach (BuildingTypeSO aso in so.list.list)
        {
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);
            tmpTransforms.Add(buttonTransform);

            buttonTransform.Find("Icon").GetComponent<Image>().sprite = aso.prefab.GetComponent<SpriteRenderer>().sprite;


            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * (width + padding), width + padding);

            buttonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingTypeSO(aso);
            });

            buttonTransform.GetComponent<MouseEnterAndExits>().OnMouseEnterEvent += (object sender, System.EventArgs e) =>
            {
                Debug.Log("OnMouseEnterEvent");
                ToolTipsUI.Instance.ShowMessage(aso.GetBuildingDescription());
            };

            i++;
        }
    }

}
