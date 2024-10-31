using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ToolbarController: ������ ������ �����ϴ� Ŭ����
public class ToolbarController : MonoBehaviour
{
    [SerializeField] int toolbarSize = 12; // ������ ũ�� (������ ����)
    int selectedTool; // ���� ���õ� ������ �ε���

    public Action<int> onChange; // ������ ����� �� ȣ��Ǵ� ��������Ʈ

    // ���� ���õ� ������ �������� �������� ������Ƽ
    public item GetItem
    {
        get
        {
            // ���� ���õ� ������ �������� ��ȯ
            return GameManager.Instance.inventoryContainer.slots[selectedTool].item;
        }
    }

    // �� �����Ӹ��� ȣ��Ǵ� �Լ�
    private void Update()
    {
        // ���콺 ��ũ���� ��ȭ���� ������
        float delta = Input.mouseScrollDelta.y;

        // ��ũ���� �������� ��
        if (delta != 0)
        {
            // ��ũ���� ���� �����̸�
            if (delta > 0)
            {
                selectedTool += 1; // ���õ� ������ �ϳ� ����
                // ���õ� ������ ���� ũ�⸦ �ʰ��ϸ� ó������ ���ư�
                selectedTool = (selectedTool >= toolbarSize ? 0 : selectedTool);
            }
            // ��ũ���� �Ʒ��� �����̸�
            else
            {
                selectedTool -= 1; // ���õ� ������ �ϳ� ����
                // ���õ� ������ 0���� ������ ������ ������ ���ư�
                selectedTool = (selectedTool < 0 ? toolbarSize - 1 : selectedTool);
            }
            // ������ ����Ǿ����� �˸�
            onChange?.Invoke(selectedTool);
        }
    }

    // �ܺο��� ���õ� ������ �ε����� �����ϴ� �Լ�
    internal void Set(int id)
    {
        selectedTool = id; // ���õ� ������ �־��� id�� ����
    }
}