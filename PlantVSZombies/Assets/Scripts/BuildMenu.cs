using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    public Texture sunImage;

    public BuildInfo[] plants;

    public static BuildInfo cur;

    public GameObject zombiePrefab;

    public GameObject grassPrefab;

    private void OnGUI()
    {

        if(GUILayout.Button(new GUIContent("Setting")))
        {

        }


        GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, -7, 200, 100));
        GUILayout.BeginHorizontal("box");

        GUI.enabled = true;
        GUILayout.Box(new GUIContent(SunCollection.score.ToString(), sunImage));

        foreach (BuildInfo info in plants)
        {
            GUI.enabled = SunCollection.score >= info.price;
            if (GUILayout.Button(new GUIContent(info.price.ToString(), info.previewImage)))
            {
                cur = info;
            }
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    private void Start()
    {

        //创建grass
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Instantiate(grassPrefab, new Vector3(i, j, 0), Quaternion.identity);
            }
        }


        InvokeRepeating("CreateZombie", 1, 3);
    }

    private void CreateZombie()
    {

        //在0-5行右侧，制造Zombie
        int i = Random.Range(0, 5);
        Instantiate(zombiePrefab, new Vector3(10, i, 0), Quaternion.identity);
    }
}
