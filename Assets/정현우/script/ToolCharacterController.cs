// 2024-09-07 �ۼ��� : ������

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GridBrushBase;

public class ToolCharacterController : MonoBehaviour
{
    // ĳ������ �������� ����ϴ� ��ũ��Ʈ
    PlayerMovement character;

    Rigidbody2D rgdb2d;

    // ������ �����ϰ� ����ϴ� ToolbarController
    ToolbarController toolbarController;

    Animator animator;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1f;

    // ��ȣ�ۿ��� �� �ִ� Ÿ�ϰ� ������Ʈ�� ǥ���ϴ� MarkerManager
    [SerializeField] MarkerManager markerManager;

    // Ÿ�� ������ �о���� TileMapReadController
    [SerializeField] TileMapReadController tileMapReadController;

    // ĳ���Ͱ� ��ȣ�ۿ��� �� �ִ� �ִ� �Ÿ�
    [SerializeField] float maxDistance = 1.5f;

    // Ÿ���� �������� �� ����Ǵ� �׼�
    [SerializeField] ToolAction onTilePickUp;

    // ��ȭâ (Ȱ��ȭ ���ο� ���� �ٸ� ������ �ϱ� ���� ���)
    [SerializeField] GameObject dialougePanel; 

    [SerializeField] TileData plowableTiles;

    // ĳ���Ͱ� ��ȣ�ۿ��� Ÿ���� ��ġ
    Vector3Int selectedTilePosition;

    // ĳ���Ͱ� Ÿ���� ������ �� �ִ��� ���� (�Ÿ��� ���� ����)
    bool selectable;

    private void Awake()
    {
        character = GetComponent<PlayerMovement>(); 
        rgdb2d = GetComponent<Rigidbody2D>();    
        toolbarController = GetComponent<ToolbarController>();
        animator = GetComponent<Animator>();
        //characterAppearance = GetComponent<CharacterAppearance>();
    }
    private void Update()
    {
        SelectTile();
        CanSelectedCheck();
        Marker();
        if (Input.GetMouseButtonDown(0) && dialougePanel.activeInHierarchy == false) //���ʹ�ư
        {
            if (UseToolWorld() == true)
            {
                return;
            };
            UseToolGrid();
        }
    }

    // ���콺 ��ġ�� �ش��ϴ� Ÿ���� �����ϴ� �Լ�
    private void SelectTile()
    {
        // ���콺�� ��ġ�� ���� Ÿ�ϸ� ��ǥ�� ������
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }

    // ĳ���Ͱ� Ÿ���� ������ �� �ִ��� Ȯ���ϴ� �Լ�
    void CanSelectedCheck()
    {
        // ĳ���Ϳ� ���콺 ��ġ ���� �Ÿ��� ���
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;

        // ���� ���� ���ο� ���� ��Ŀ ǥ��
        markerManager.Show(selectable); 
    }

    // ��Ŀ(Ÿ���� ��ġ�� �ð������� ǥ��)�� �����ϴ� �Լ�
    private void Marker()
    {
        markerManager.markedCellPostion = selectedTilePosition;
    }

    // ���� �󿡼� ������ ����ϴ� �Լ�
    private bool UseToolWorld() 
    {
        // ĳ���Ͱ� ���������� ������ ���⿡ offsetDistance��ŭ ������ ��ġ���� ��ȣ�ۿ�
        Vector2 position = rgdb2d.position + character.lastmotionVector * offsetDistance;

        // ���� ���õ� ����(������)�� ������
        item item = toolbarController.GetItem;

        // ���õ� ������ ������ ����
        if (item == null) { return false; }

        // ������ �Ҵ�� �׼��� ������ ����
        if (item.onAction == null) { return false; }

        // ������ ����ϴ� �ִϸ��̼� ����
        animator.SetTrigger("act");

        // ������ �׼��� ����
        bool complete = item.onAction.OnApply(position);

        // ���� ����� �Ϸ�Ǹ�
        if (complete == true)
        {
            if (item.onItemUsed != null)
            {
                // ������ ���Ǿ����� �κ��丮�� �ݿ�
                item.onItemUsed.OnItemUsed(item, GameManager.Instance.inventoryContainer);
            }
        }

        return complete;
    }

    // Ÿ�Ͽ��� ������ ����ϴ� �Լ�
    private void UseToolGrid()
    {
        if (selectable == true)
        {
            // toolbarController���� ���� ���õ� �������� ������,
            // �������� null�� ���, �� ���õ� �������� ������ �޼��带 ����
            item item = toolbarController.GetItem;
            if (item == null) {
                PickUptile();
                return; 
            }

            // �������� onTileMapAction�� null�� ���, �� Ÿ�� �ʿ� ������ �� ���� �������̸� �޼��带 ����
            if (item.onTileMapAction == null) { return; }

            animator.SetTrigger("act");

            // �������� onTileMapAction�� ȣ���Ͽ� ���õ� Ÿ�� ��ġ�� �������� ����
            bool complete = item.onTileMapAction.OnApplyToTileMap(selectedTilePosition, tileMapReadController, item);

            // ���� ����� �Ϸ�Ǹ�
            if (complete == true)
            {
                if(item.onItemUsed != null)
                {
                    // ������ ���Ǿ����� �κ��丮�� �ݿ�
                    item.onItemUsed.OnItemUsed(item, GameManager.Instance.inventoryContainer);
                }
                
            }
            
        }
    }

    // Ÿ���� �����ϴ� �Լ�
    private void PickUptile()
    {
        if (onTilePickUp == null) { return; }

        onTilePickUp.OnApplyToTileMap(selectedTilePosition, tileMapReadController, null);
    }
}
