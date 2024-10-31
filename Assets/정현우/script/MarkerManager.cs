using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

// MarkerManager Ŭ����: Ÿ�ϸʿ��� ��Ŀ�� �����ϴ� ��ũ��Ʈ
public class MarkerManager : MonoBehaviour
{
    // Ÿ�ϸʰ� ��Ŀ�� ����� Ÿ���� �����մϴ�

    [SerializeField] Tilemap targetTilemap; // ��Ŀ�� ǥ���� Ÿ�ϸ�
    [SerializeField] TileBase tile; // ����� Ÿ�� (��Ŀ)

    public Vector3Int markedCellPostion; // ���� ��Ŀ�� �ִ� ���� ��ġ
    Vector3Int oldCellPostion; // ���� ��Ŀ ��ġ
    bool show; // ��Ŀ ǥ�� ����

    // �� ������ ������Ʈ�Ǵ� �Լ�
    private void Update()
    {
        // ��Ŀ�� ǥ�õ��� ������ �ƹ��͵� ���� �ʽ��ϴ�
        if (show == false)
        {
            return;
        }

        // ���� ��ġ�� Ÿ���� ����ϴ�
        targetTilemap.SetTile(oldCellPostion, null);
        // ���� ��ġ�� ��Ŀ Ÿ���� �����մϴ�
        targetTilemap.SetTile(markedCellPostion, tile);
        // ���� ��ġ�� ���� ��ġ�� ������Ʈ�մϴ�
        oldCellPostion = markedCellPostion;
    }

    // ��Ŀ ǥ�� ���θ� �����ϴ� �Լ�
    internal void Show(bool selectable)
    {
        show = selectable; // ���� �������� ���θ� �����մϴ�
        targetTilemap.gameObject.SetActive(show); // Ÿ�ϸ��� Ȱ��ȭ ���¸� �����մϴ�
    }
}