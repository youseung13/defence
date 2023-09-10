using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    //레벨에 따른 필요 골드,재화를 관리
    public int requiredgold;   //필요 골드
    public int requiredgold2;   //필요 골드
    public int requiredgem;    //필요 재화
    public int requiredgem2;    //필요 재화
    public int requiredlevel;  //필요 레벨


    public int Setrequiredgold(int level)
    {
        requiredgold = 100 * level;

        return requiredgold;
    }

     public int Setrequiredgold2(int level)
    {
        requiredgold2 = 250 * level;

        return requiredgold2;
    }

    public int Setrequiredgem(int level)
    {
        requiredgem = 10 * level;

        return requiredgem;
    }

    public int Setrequiredgem2(int level)
    {
        requiredgem2 = 15 * level;

        return requiredgem2;
    }
}