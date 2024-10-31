// 2024-09-19 �ۼ��� : ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// �������� �巡�� �� ����ϴ� ����� ó���ϴ� Ŭ����
public class ItemDragAndDropController : MonoBehaviour
{
    // �巡���ϴ� ������ ����
    [SerializeField] ItemSlot itemSlot;

    // ������ �������� ��Ÿ���� ���� ������Ʈ
    [SerializeField] GameObject itemIcon;

    // UI�� �׷����� ĵ����
    [SerializeField] Canvas canvas;

    // �������� ��ġ�� ũ�⸦ �����ϴ� RectTransform
    RectTransform iconTransform;

    // �������� �̹����� ��Ÿ���� Image ������Ʈ
    Image itemIconImage;

    // ó���� ����Ǵ� �Լ� (�ʱ�ȭ)
    private void Start()
    {
        // ���ο� ������ ������ ���� (GameManager���� �����ϴ� ���� �ٶ�����)
        itemSlot = new ItemSlot();

        // �������� RectTransform�� ������ (ũ��� ��ġ�� �����ϱ� ���� �ʿ�)
        iconTransform = itemIcon.GetComponent<RectTransform>();

        // �������� �̹����� ������ (�����ۿ� ���� �ٸ� �̹����� ����)
        itemIconImage = itemIcon.GetComponent<Image>();
    }

    // �� �����Ӹ��� ����Ǵ� �Լ�
    private void Update()
    {
        // ������ �������� Ȱ��ȭ�Ǿ� ���� ���� ����
        if (itemIcon.activeInHierarchy == true)
        {
            // ���콺 ��ǥ�� ĵ���� ��ǥ�� ��ȯ (�������� ���콺�� ����ٴϵ���)
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                canvas.GetComponent<RectTransform>(),  // ĵ������ RectTransform
                Input.mousePosition,                   // ���콺�� ��ũ�� ��ǥ
                canvas.worldCamera,                    // ĵ������ �Ҵ�� ī�޶�
                out localPoint                         // ��ȯ�� ���� ��ǥ
            );

            // ��ȯ�� ��ǥ�� �������� ��ġ�� ����
            iconTransform.localPosition = localPoint;

            // ���콺 ���� ��ư�� ������ ��
            if (Input.GetMouseButton(0))
            {
                // �κ��丮 �ȿ��� Ŭ���� ������ Ȯ��
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    // �κ��丮 �ܺθ� Ŭ���ϸ� �������� ����
                    Vector3 worldPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    worldPostion.z = 0;

                    // �������� ���� ���콺 ��ġ�� ����
                    itemSpawnManager.instance.SpawnItem(worldPostion, itemSlot.item, itemSlot.count);

                    // ������ ������ ���� �������� ��Ȱ��ȭ
                    itemSlot.Clear();
                    itemIcon.SetActive(false);
                }
            }
        }
    }

    // ������ ������ Ŭ������ �� ȣ��Ǵ� �Լ�
    internal void OnClick(ItemSlot itemSlot)
    {
        // ���� ������ ���Կ� �������� ������, ������ ������ ������ ������ ����
        if (this.itemSlot.item == null)
        {
            this.itemSlot.Copy(itemSlot);
            itemSlot.Clear();  // ������ ������ �����
        }
        else // ���� ���Կ� �������� ������, �� ������ �������� ��ü
        {
            item item = itemSlot.item;
            int count = itemSlot.count;

            // ���� ������ ������ ������ ����
            itemSlot.Copy(this.itemSlot);
            // ������ ���Կ� ���� �ִ� �������� �ٽ� ����
            this.itemSlot.Set(item, count);
        }
        // �������� ������Ʈ
        UpdateIcon();
    }

    // ������ �������� ������Ʈ�ϴ� �Լ�
    private void UpdateIcon()
    {
        // ���Կ� �������� ������ �������� ����
        if (itemSlot.item == null)
        {
            itemIcon.SetActive(false);
        }
        else // ���Կ� �������� ������ �������� ǥ���ϰ� �̹��� ����
        {
            itemIcon.SetActive(true);
            itemIconImage.sprite = itemSlot.item.icon;  // �����ۿ� �´� �̹����� ����
        }
    }
}