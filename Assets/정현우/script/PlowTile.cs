//2024-10-03, �ۼ��� : ������
//�� �ڵ�� Unity�� Tilemap �ý����� ����Ͽ� Ư�� Ÿ���� �����ϴ� ���� �ൿ�� �����ϴ� ��ũ��Ʈ�Դϴ�.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Plow")]
public class PlowTile : ToolAction
{
    // canPlow��� ����Ʈ�� ����Ͽ� ������ �� �ִ� Ÿ�� Ÿ���� ����
    [SerializeField]List<TileBase> canPlow;
    
    public override bool OnApplyToTileMap(Vector3Int gridPostion, TileMapReadController tileMapReadController, item item)
    {
        TileBase tileToPlow = tileMapReadController.GetTileBase(gridPostion);

        if(canPlow.Contains(tileToPlow) == false)
        {
            // canPlow ����Ʈ�� ���ԵǾ� ���� ������ false�� ��ȯ�Ͽ� ������ �Ұ�����
            return false;
        }

        //Ÿ���� ���� ������ ���, cropsManager�� ���� ���� �۾��� �����ϰ� true
        tileMapReadController.cropsManager.Plow(gridPostion);
        return true;
    }
}
