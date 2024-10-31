// 2024-10-09 작성자 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Crop")]
public class Crop : ScriptableObject
{
    public int TimeToGrow = 10;
    public item yield;
    public int count = 1;

    public List<Sprite> sprtes;
    public List<int> growthStageTime;

}
