// 2024-09-07 �ۼ��� : ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GameManager : MonoBehaviour
{
    //�̱����� ����:
    //���� ���� ����: GameManager.Instance�� ��𼭵� ���� ���� ����.
    //�ߺ� ����: �ϳ��� �ν��Ͻ��� �����ϵ��� ����.
    //�߾� ���� ����: �߿��� �ý��� ������ ����.

    public static GameManager Instance; 
    private void Awake() 
    {   
        Instance = this;
    }
    public GameObject player;
    public ItemContainer inventoryContainer;
    public ItemDragAndDropController dragAndDropController;
    //
    public DialogueSystem dialogueSystem;
    public DayTimeController TimeController; 
}
