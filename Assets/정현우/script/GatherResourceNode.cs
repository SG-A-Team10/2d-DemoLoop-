// 2024-09-30, �ۼ��� : ������

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


// enum: �������� ���õ� ��� ���� �׷�ȭ�Ͽ� ����ϱ� ���� ������ Ÿ��
public enum ResourceNodeType
{ 
    Undefind,
    Tree,
    Ore
}

// GatherResourceNode Ŭ������ �ڿ� ���� ����� �����ϴ� Unity ��ũ��Ʈ�Դϴ�.

// �÷��̾ Ư�� ��ġ���� �ڿ��� ������ �� �ֵ��� �����ִ� ������ �����մϴ�. 
[CreateAssetMenu(menuName = "Data/Tool Action/Gather Resource Node")]  
public class GatherResourceNode : ToolAction
{
    // �÷��̾ ��ȣ�ۿ��� �� �ִ� ������ ũ��
    [SerializeField] float sizeOfInteractableArea = 1.0f;
    [SerializeField] List<ResourceNodeType> canHitNodesOfType;

    // OnApply �޼���� ������ ��ġ���� �ڿ� ������ �õ��մϴ�.
    public override bool OnApply(Vector2 worldPoint)
    {
        // ������ ��ġ�� ũ�� ���� �ִ� ��� 2D �浹ü(�ݶ��̴�)�� �����մϴ�.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, sizeOfInteractableArea);

        // ������ ��� �浹ü�� �ݺ��մϴ�.
        foreach (Collider2D c in colliders)
        {
            // �� �浹ü���� ToolHit ������Ʈ�� �����ɴϴ�.
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null) // ToolHit ������Ʈ�� �����ϴ� ���
            {
                if (hit.CanBeHit(canHitNodesOfType) == true)
                {
                    // Hit �޼��带 ȣ���Ͽ� �ڿ��� �����մϴ�.
                    hit.Hit();
                    return true; // �۾��� ���������� ����Ǿ����� ��ȯ�մϴ�.    
                }
            

            }
        }
        return false; // ������ ToolHit ��ü�� ������ false�� ��ȯ�մϴ�.
    }
}
