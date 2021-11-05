using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public GameDataManager Instance { private set; get; }

    public ulong IncomePerHour;




    private void Awake()
    {
        Instance = this;
    }


}
