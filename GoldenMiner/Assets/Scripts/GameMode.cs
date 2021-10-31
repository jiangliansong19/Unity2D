using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameMode: MonoBehaviour {

    public static GameMode Instance;

    public bool isPlaying = false; //是否开始/暂停

    public Dictionary<string, GameObject> templates; //<名称：道具>

    public List<GameObject> levelObjs;

    public string[] objNames; //分数道具的名称

    public int[] fractionDatas; //分数道具的分数

    public int[] targetFraction; //每一关的目标分数

    public string[] propsNames; //特殊道具名称

    public float minX, maxX, minY, maxY; //挖矿的范围

    public float[] scaleData; //道具的缩放比例

    public int curFraction; // 当前的总分数

    public bool isDouble = false; // 是否为双倍分

    public int gameLevel = 0; // 游戏关卡，默认为0


    private void Awake() {

        Instance = this;

        targetFraction = new int[] {3000, 4000, 5000, 6000};
        objNames = new string[] {"Diamend", "GoldOne", "GoldTwo", "StoneOne", "StoneTwo"};
        propsNames = new string[] {"Explosive", "Potion"};
        scaleData = new float[] {1.0f, 1.2f, 1.5f, 1.8f, 2.0f};
        fractionDatas = new int[] {1000, 300, 500, 100, 150};

        templates = new Dictionary<string, GameObject>();
        levelObjs = new List<GameObject>();
    }

    private void Start() {

        StartPlaying();
    }

    public void StartPlaying()
    {
        if (isPlaying)
        {
            foreach (string item in objNames)
            {
                GameObject obj = GetGameObjectWithPrefabName(item);
                templates.Add(item, obj);
            }

            foreach (string item in propsNames)
            {
                GameObject obj = GetGameObjectWithPrefabName(item);
                templates.Add(item, obj);
            }

            switchLevel();


        }
    }

    public void switchLevel() {
        int overFraction = 1000; // 道具总分数比通关分数多1000
        int allCreatedFraction = 0;
        int tmpFraction = targetFraction[gameLevel];
        tmpFraction += overFraction;
        //当总分数<目标分数时，创建更多的分数道具
        while (allCreatedFraction < tmpFraction) {
            //随机分数道具
            int objIndex = Random.Range(0, objNames.Length);
            string tmpName = objNames[objIndex];
            GameObject tmpObj = CreateRandomProps(tmpName);

            float tmpScale = 1;
            int scaleLevel = 1;
            //处理钻石无缩放旋转，其它的随机缩放旋转
            if (objIndex != 0) { 
                Quaternion tmpQuat = GetRandomRotation();
                tmpObj.transform.rotation = tmpQuat;
                tmpScale = GetRandomScale(out scaleLevel);
                tmpObj.transform.localScale *= tmpScale;
            }

            var tmpScript = tmpObj.AddComponent<PropsScript>();
            tmpScript.nowType = PropsType.Fraction;
            int fraction = fractionDatas[objIndex];
            fraction = (int)(fraction * tmpScale);
            allCreatedFraction += fraction;
            tmpScript.fraction = fraction;
            tmpScript.scaleLevel = scaleLevel;
            levelObjs.Add(tmpObj);
        }

        //特殊道具，最多2个
        int propsCount = Random.Range(0,3); 
        while (propsCount > 0) {
            propsCount--;
            GameObject tmpObject = GetRandomSpecialProps();
            levelObjs.Add(tmpObject);
        }
    }

    public void AddFraction(int fraction) {
        curFraction += fraction;
    }

    public void AddBoomProps() {

    }



    /// <summary>
    /// 根据prefab的名称，创建分数道具
    /// </summary>
    /// <param name="name"></param>
    /// <returns>分数道具</returns>
    private GameObject CreateRandomProps(string name) {
        GameObject obj = templates[name];
        Vector3 tmpPoint = GetRandomPosition();
        GameObject tmpObj = Instantiate(obj, tmpPoint, Quaternion.identity);
        return tmpObj;
    }

    private GameObject GetRandomSpecialProps() {
        int randomIndex = Random.Range(0, 2);
        string name = propsNames[randomIndex];
        GameObject obj = templates[name];
        return obj;
    }

    public Vector3 GetRandomPosition() {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        Vector3 tmpPoint = new Vector3(x, y, 0);
        return tmpPoint;
    }

    private Quaternion GetRandomRotation() {
        float angle = Random.Range(0, 360);
        Quaternion tmp = Quaternion.AngleAxis(angle, Vector3.forward);
        return tmp;
    }

    public float GetRandomScale(out int scale) {
        int index = Random.Range(0, scaleData.Length);
        scale = index + 1;
        float scaleValue = scaleData[index];
        return scaleValue;
    }

    private GameObject GetGameObjectWithPrefabName(string name) {
        GameObject obj = Resources.Load<GameObject>("Prefabs/" + name);
        return obj;
    }
}