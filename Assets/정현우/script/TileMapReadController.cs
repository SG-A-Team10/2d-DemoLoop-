// 2024-09-24 �ۼ��� : ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    [SerializeField] Tilemap tileMap;
    public CropsManager cropsManager;


    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition)
    {
        if (tileMap == null) 
        {
            //"BaseTilemap"�̶�� �̸��� ���� ���� ������Ʈ�� ã�Ƽ� �� ������Ʈ�� Tilemap ������Ʈ�� tileMap ������ �Ҵ��ϴ� ����
            tileMap = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        }

        // Ÿ�ϸ��� �������� ���� ��� �⺻������ (0, 0, 0)�� ��ȯ�Ͽ� ������ �ڵ忡�� ������ �����ϴ� ����
        if (tileMap == null) { return Vector3Int.zero; }

        Vector3 worldPostion;

        if (mousePosition)
        {
            worldPostion = Camera.main.ScreenToWorldPoint(position);
        }
        else
        {
            worldPostion = position;
        }

        // ȭ�� ��ǥ(2D)�� ���� ��ǥ(3D)�� ��ȯ�ϴ� �޼���
        // WorldToCell: ���� ��ǥ�� Ÿ�ϸ��� �׸��� ��ǥ�� ��ȯ�ϴ� �޼���
        Vector3Int gridPosition = tileMap.WorldToCell(worldPostion);
       
        return gridPosition;
    }

    public TileBase GetTileBase(Vector3Int gridPosition, bool mousePosition = false)
    {
        if (tileMap == null)
        {
            
            tileMap = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        }

        if (tileMap == null) { return null; }


        TileBase tile = tileMap.GetTile(gridPosition);
        // �� �ڵ�� tileMap���� Ư�� ��ġ�� �ִ� Ÿ���� �������� ������ �մϴ�.

        //Debug.Log("Tile in position =" + gridPosition + " is " + tile); �׽�Ʈ�� 

        if (tile == null)
        {
            return null;  // Ÿ���� ������ null�� ��ȯ
        }

        return tile;  // Ÿ���� ������ �� Ÿ���� ��ȯ
    }
}