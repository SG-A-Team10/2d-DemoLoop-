// 2024-09-07 작성자 : 정현우

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
    // 캐릭터의 움직임을 담당하는 스크립트
    PlayerMovement character;

    Rigidbody2D rgdb2d;

    // 도구를 선택하고 사용하는 ToolbarController
    ToolbarController toolbarController;

    Animator animator;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1f;

    // 상호작용할 수 있는 타일과 오브젝트를 표시하는 MarkerManager
    [SerializeField] MarkerManager markerManager;

    // 타일 정보를 읽어오는 TileMapReadController
    [SerializeField] TileMapReadController tileMapReadController;

    // 캐릭터가 상호작용할 수 있는 최대 거리
    [SerializeField] float maxDistance = 1.5f;

    // 타일을 선택했을 때 실행되는 액션
    [SerializeField] ToolAction onTilePickUp;

    // 대화창 (활성화 여부에 따라 다른 동작을 하기 위해 사용)
    [SerializeField] GameObject dialougePanel; 

    [SerializeField] TileData plowableTiles;

    // 캐릭터가 상호작용할 타일의 위치
    Vector3Int selectedTilePosition;

    // 캐릭터가 타일을 선택할 수 있는지 여부 (거리에 따라 결정)
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
        if (Input.GetMouseButtonDown(0) && dialougePanel.activeInHierarchy == false) //왼쪽버튼
        {
            if (UseToolWorld() == true)
            {
                return;
            };
            UseToolGrid();
        }
    }

    // 마우스 위치에 해당하는 타일을 선택하는 함수
    private void SelectTile()
    {
        // 마우스가 위치한 곳의 타일맵 좌표를 가져옴
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }

    // 캐릭터가 타일을 선택할 수 있는지 확인하는 함수
    void CanSelectedCheck()
    {
        // 캐릭터와 마우스 위치 간의 거리를 계산
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;

        // 선택 가능 여부에 따라 마커 표시
        markerManager.Show(selectable); 
    }

    // 마커(타일의 위치를 시각적으로 표시)를 설정하는 함수
    private void Marker()
    {
        markerManager.markedCellPostion = selectedTilePosition;
    }

    // 월드 상에서 도구를 사용하는 함수
    private bool UseToolWorld() 
    {
        // 캐릭터가 마지막으로 움직인 방향에 offsetDistance만큼 떨어진 위치에서 상호작용
        Vector2 position = rgdb2d.position + character.lastmotionVector * offsetDistance;

        // 현재 선택된 도구(아이템)를 가져옴
        item item = toolbarController.GetItem;

        // 선택된 도구가 없으면 종료
        if (item == null) { return false; }

        // 도구에 할당된 액션이 없으면 종료
        if (item.onAction == null) { return false; }

        // 도구를 사용하는 애니메이션 실행
        animator.SetTrigger("act");

        // 도구의 액션을 실행
        bool complete = item.onAction.OnApply(position);

        // 도구 사용이 완료되면
        if (complete == true)
        {
            if (item.onItemUsed != null)
            {
                // 도구가 사용되었음을 인벤토리에 반영
                item.onItemUsed.OnItemUsed(item, GameManager.Instance.inventoryContainer);
            }
        }

        return complete;
    }

    // 타일에서 도구를 사용하는 함수
    private void UseToolGrid()
    {
        if (selectable == true)
        {
            // toolbarController에서 현재 선택된 아이템을 가져옴,
            // 아이템이 null인 경우, 즉 선택된 아이템이 없으면 메서드를 종료
            item item = toolbarController.GetItem;
            if (item == null) {
                PickUptile();
                return; 
            }

            // 아이템의 onTileMapAction이 null인 경우, 즉 타일 맵에 적용할 수 없는 아이템이면 메서드를 종료
            if (item.onTileMapAction == null) { return; }

            animator.SetTrigger("act");

            // 아이템의 onTileMapAction을 호출하여 선택된 타일 위치에 아이템을 적용
            bool complete = item.onTileMapAction.OnApplyToTileMap(selectedTilePosition, tileMapReadController, item);

            // 도구 사용이 완료되면
            if (complete == true)
            {
                if(item.onItemUsed != null)
                {
                    // 도구가 사용되었음을 인벤토리에 반영
                    item.onItemUsed.OnItemUsed(item, GameManager.Instance.inventoryContainer);
                }
                
            }
            
        }
    }

    // 타일을 수집하는 함수
    private void PickUptile()
    {
        if (onTilePickUp == null) { return; }

        onTilePickUp.OnApplyToTileMap(selectedTilePosition, tileMapReadController, null);
    }
}
