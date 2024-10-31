// 2024-09-12 �ۼ��� : ������

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] //Ŭ������ ����ü ��ü�� ����ȭ ����, [SerializeField] ������ ����ȭ
public class ItemSlot
{
    public item item; // �� ���Կ� ����ִ� ������
    public int count; // �κ��丮 ĭ�� �ִ� �������� ����

    // �ٸ� ItemSlot�� ������ �����ϴ� �޼���
    public void Copy(ItemSlot slot)
    {
        item = slot.item; // ������ ����
        count = slot.count; // ���� ����
    }

    // �����۰� ������ �����ϴ� �޼���
    public void Set(item item, int count)
    {
        this.item = item; // ���Կ� ������ ����
        this.count = count; // ���Կ� ������ ���� ����
    }

    // ������ ���� �޼���
    public void Clear()
    {
        item = null; // �������� null�� ���� (���)
        count = 0; // ������ 0���� ����
    }
}

// ItemContainer Ŭ������ ���� ���� ItemSlot�� �����ϴ� Ŭ�����Դϴ�.
[CreateAssetMenu(menuName = "Data/Item Container")] 
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slots; // ���� ���� ItemSlot�� �����ϴ� ����Ʈ

    // �������� �κ��丮�� �߰��ϴ� �޼���
    public void Add(item item, int count = 1)
    {
        // ���� �� �ִ� �������� ���
        if (item.stackable == true)
        {
            // �̹� ���� �������� �ִ� ������ ã���ϴ�.
            ItemSlot itemSlot = slots.Find(x => x.item == item);
            if (itemSlot != null)
            {
                // ���� �������� ������ ������ ������ŵ�ϴ�.
                itemSlot.count += count;
            }
            else
            {
                // �� ������ ã�Ƽ� �������� �߰��մϴ�.
                itemSlot = slots.Find(x => x.item == null);

                if (itemSlot != null)
                {
                    itemSlot.item = item; // ������ ����
                    itemSlot.count = count; // ���� ����
                }
            }
        }
        else
        {
            // ���� �� ���� �������� ���
            ItemSlot itemSlot = slots.Find(x => x.item == null);

            if (itemSlot != null)
            {
                itemSlot.item = item; // �� ���Կ� ������ ����
            }
        }
    }

    // �������� �κ��丮���� �����ϴ� �޼���
    public void Remove(item itemToRemove, int count = 1)
    {
        if (itemToRemove.stackable)
        {
            // ���� �� �ִ� �������� ���
            ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);

            if (itemSlot == null) { return; } // �������� ������ ����

            itemSlot.count -= count; // ���� ����

            if (itemSlot.count <= 0)
            {
                itemSlot.Clear(); // ������ 0 ���ϰ� �Ǹ� ���� ����
            }
        }
        else
        {
            // ���� �� ���� �������� ���
            while (count > 0)
            {
                count -= 1; // ������ ���� ����

                ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);

                if (itemSlot == null) { break; } // �������� ������ ����
                itemSlot.Clear(); // ���� ����
            }
        }
    }
}



