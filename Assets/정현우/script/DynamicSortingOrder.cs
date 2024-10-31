using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �� �ڵ��� ���� : ������Ʈ�� ��ġ�� ���� ȭ�鿡�� �յڷ� ǥ�õǴ� ������ �ڵ����� ����
public class DynamicSortingOrder : MonoBehaviour
{
    // ��������Ʈ�� ������ ������ �����ϴ� ����
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // �� ������Ʈ�� ����� SpriteRenderer ������Ʈ�� ������
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // �� �����Ӹ��� ����Ǵ� �Լ�
    void Update()
    {
        // ������Ʈ�� Y ��ǥ�� ���� ��������Ʈ�� ������ ������ ����
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }
}