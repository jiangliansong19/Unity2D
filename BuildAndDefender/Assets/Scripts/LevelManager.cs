using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ¿ØÖÆÄÑ¶È
/// </summary>
public class LevelManager : MonoBehaviour
{
    private LevelTypeListSO levelList;

    private void Awake()
    {
        levelList = Resources.Load<LevelTypeListSO>(typeof(LevelTypeListSO).Name);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //todo--forTest
        ResourceManager.Instance.AddResourcesAmount(levelList.list[0].originResources);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
