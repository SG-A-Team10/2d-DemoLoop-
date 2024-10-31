using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Data/Tool Action/Harvest")]
public class TilePickUpAction : ToolAction
{
    // override Ű����� C#���� ��ӹ��� Ŭ�������� �θ� Ŭ������ �޼��带 ������
    // OnApplyToTileMap �޼���� ToolAction Ŭ�������� ���ǵ� �⺻ �޼��带 ������
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, item item)
    {
        tileMapReadController.cropsManager.PickUp(gridPosition);

        return true;
    }
}
