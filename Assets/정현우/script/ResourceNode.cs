// 2024-09-07 �ۼ��� : ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
// [RequireComponent(typeof(BoxCollider2D))]�� ����Ƽ���� ����ϴ� Ư��(Attribute)����,
// Ư�� ��ũ��Ʈ�� �ݵ�� BoxCollider2D ������Ʈ�� �ʿ�� �Ѵٴ� �� �ǹ�
public class ResourceNode : ToolHit
{
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] float spread = 0.5f;

    [SerializeField] item item;
    [SerializeField] int itemCountInOneDrop;
    [SerializeField] int dropCount = 5; // �����Ǵ� ������ ����; 
    [SerializeField] ResourceNodeType nodeType;
    public override void Hit()
    {
        StartCoroutine(HandleHit());
    }

    private IEnumerator HandleHit()
    {
        yield return new WaitForSeconds(0.5f); // 0.5�� ����

        while (dropCount > 0)
        {
            dropCount -= 1;
            // ������ ��ġ�� ������ ����;
            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;

            itemSpawnManager.instance.SpawnItem(position, item, itemCountInOneDrop);
        }

        Destroy(gameObject);
    }

    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }
}
