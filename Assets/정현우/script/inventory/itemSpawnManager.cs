// 2024-09-19 �ۼ��� : ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSpawnManager : MonoBehaviour
{
    public static itemSpawnManager instance; // �̱��� ������ ���� �ν��Ͻ� ����

    private void Awake()
    {
        instance = this; // ���� �ν��Ͻ��� ���� ������ �Ҵ�, �̱��� ���� ����
    }

    [SerializeField] GameObject pickUpItemPrefab; // ������ �Ⱦ� �������� ������

    // ��ġ, ������, ������ ���� �޾Ƽ� �������� �����ϴ� �Լ�
    public void SpawnItem(Vector3 position, item item, int count)
    {
        // ������ ��ġ�� �Ⱦ� ������ �������� �ν��Ͻ�ȭ
        GameObject o = Instantiate(pickUpItemPrefab, position, Quaternion.identity);

        // ������ ������Ʈ�� �����۰� ������ ����
        o.GetComponent<PickUpItem>().Set(item, count);
    }
}