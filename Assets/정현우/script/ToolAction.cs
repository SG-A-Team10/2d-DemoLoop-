using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// // ToolAction Ŭ������ ScriptableObject�� ��ӹ޾� ���� �ൿ�� �����ϴ� �⺻ Ŭ����
public class ToolAction : ScriptableObject
{

    // ���� Ư�� ���� ����Ʈ�� ����� �� ȣ��Ǵ� �޼���
    public virtual bool OnApply(Vector2 worldPoint)
    {
        Debug.LogWarning("OnApply is not implemented");
        return true;
    }

    // ���� Ÿ�� ���� Ư�� ��ġ�� ����� �� ȣ��Ǵ� �޼���
    public virtual bool OnApplyToTileMap(Vector3Int tileMapPosition, TileMapReadController tileMapReadController, item item)
    {
        Debug.LogWarning("OnApplyToTileMap is not implemented");
        return true;
    }

    // �������� ���Ǿ��� �� ȣ��Ǵ� �޼���
    public virtual void OnItemUsed(item usedItem, ItemContainer inventory)
    {
        
    }
}
