using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class BuildMenuUI : MonoBehaviour
{
    [SerializeField] List<BuildingTypeSO> ignoreBuildingList;
    private ArchitectureListSO architectureList;

    private float padding = 10f;
    private float width = 50f;
    private Transform buttonTemplate;

    private List<Transform> tmpTransforms;
    private List<Transform> architectureTransforms;

    private void Awake()
    {
        architectureList = Resources.Load<ArchitectureListSO>(typeof(ArchitectureListSO).Name);
        tmpTransforms = new List<Transform>();
        architectureTransforms = new List<Transform>();

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
            architectureTransforms.Add(buttonTransform);

            buttonTransform.Find("Icon").GetComponent<Image>().sprite = aso.prefab.GetComponent<SpriteRenderer>().sprite;
            buttonTransform.Find("Selected").gameObject.SetActive(false);

            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * (width + padding), 0);

            buttonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                foreach (Transform item in architectureTransforms)
                {
                    item.Find("Selected").gameObject.SetActive(false);
                }
                buttonTransform.Find("Selected").gameObject.SetActive(true);

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
            if (ignoreBuildingList.Contains(aso))
            {
                continue;
            }
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);
            tmpTransforms.Add(buttonTransform);

            buttonTransform.Find("Icon").GetComponent<Image>().sprite = aso.prefab.GetComponent<SpriteRenderer>().sprite;
            buttonTransform.Find("Selected").gameObject.SetActive(false);

            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * (width + padding), width + padding);

            buttonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingTypeSO(aso);

                foreach (Transform item in tmpTransforms)
                {
                    item.Find("Selected").gameObject.SetActive(false);
                }
                buttonTransform.Find("Selected").gameObject.SetActive(true);

            });

            buttonTransform.GetComponent<MouseEnterAndExits>().OnMouseEnterEvent += (object sender, System.EventArgs e) =>
            {
                ToolTipsUI.Instance.ShowMessage(aso.GetBuildingDescription());
            };

            i++;
        }
    }

}
