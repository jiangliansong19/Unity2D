using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class BuildMenuUI : MonoBehaviour
{
    private BottomMenuSO bottomMenu;

    [SerializeField] List<BuildingTypeSO> ignoreBuildingList;


    private float padding = 10f;
    private float width = 50f;
    private Transform buttonTemplate;

    private List<Transform> tmpTransforms;
    private List<Transform> architectureTransforms;

    private void Awake()
    {

        bottomMenu = Resources.Load<BottomMenuSO>(typeof(BottomMenuSO).Name);

        tmpTransforms = new List<Transform>();
        architectureTransforms = new List<Transform>();

        buttonTemplate = transform.Find("BuildMenuItem");
        buttonTemplate.gameObject.SetActive(false);

        ShowBuildMenuItems();
    }

    /// <summary>
    /// 展示菜单
    /// </summary>
    private void ShowBuildMenuItems()
    {
        int i = 0;
        transform.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(-bottomMenu.menuList.Count * (width + padding) / 2, width / 2 + padding);

        foreach (BottomMenuSO.BottomMenuItem item in bottomMenu.menuList)
        {
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);
            architectureTransforms.Add(buttonTransform);

            buttonTransform.Find("Icon").GetComponent<Image>().sprite = item.sprite;
            buttonTransform.Find("Selected").gameObject.SetActive(false);

            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * (width + padding), 0);

            buttonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (item.name == "DigHole")
                {
                    BuildingManager.Instance.SetActiveBuildingTypeSO(item.buildingList[0]);
                    return;
                }


                foreach (Transform itemTransform in architectureTransforms)
                {
                    itemTransform.Find("Selected").gameObject.SetActive(false);
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

                OnClickBuildMenuItem(item);
            });

            i++;
        }
    }

    /// <summary>
    /// 点击菜单
    /// </summary>
    /// <param name="item"></param>
    private void OnClickBuildMenuItem(BottomMenuSO.BottomMenuItem item)
    {
        int i = 0;
        foreach (BuildingTypeSO buildingTypeSO in item.buildingList)
        {
            if (ignoreBuildingList.Contains(buildingTypeSO))
            {
                continue;
            }
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);
            tmpTransforms.Add(buttonTransform);

            buttonTransform.Find("Icon").GetComponent<Image>().sprite = buildingTypeSO.prefab.GetComponent<SpriteRenderer>().sprite;
            buttonTransform.Find("Selected").gameObject.SetActive(false);

            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * (width + padding), width + padding);

            buttonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingTypeSO(buildingTypeSO);

                foreach (Transform item in tmpTransforms)
                {
                    item.Find("Selected").gameObject.SetActive(false);
                }
                buttonTransform.Find("Selected").gameObject.SetActive(true);

            });

            buttonTransform.GetComponent<MouseEnterAndExits>().OnMouseEnterEvent += (object sender, System.EventArgs e) =>
            {
                ToolTipsUI.Instance.ShowMessage(buildingTypeSO.GetBuildingDescription(), new ToolTipsUI.ShowTimer { time = 2f });
            };

            i++;
        }
    }
}
