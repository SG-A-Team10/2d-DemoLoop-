//2024-09-12 �ۼ��� : ������

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventoryButton : MonoBehaviour, IPointerClickHandler
    // ui�� Ŭ���� ������ �����ϴ� �������̽�
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image highlight;

    int myIndex;

    public void SetIndex(int index)
    { 
        myIndex = index;
    }

    public void Set(ItemSlot slot) //ȭ�鿡�� ǥ���� �׸�
    {
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;

        if(slot.item.stackable == true)  //������ ������ �� �ִ� �������� ����ǥ��
        {
            text.gameObject.SetActive(true);
            text.text = slot.count.ToString();
        }
        else //������ ������ �� ���� �������� ����ǥ�� ���� ����
        {
            text.gameObject.SetActive(false);
        }
    }

    public void Clean() //�������� ��������Ʈ, �ؽ�Ʈ ����
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }

    
    // �� ��ư�� ����� itemSlot�� ������ DragAndDrop���� �����Ͽ�, �׸��� ��ġ
    public void OnPointerClick(PointerEventData eventData)
    {
        ItemPanel itemPanel = transform.parent.GetComponent<ItemPanel>();
        itemPanel.OnClick(myIndex);
    }

    public void HighLight(bool b) 
    { 
        highlight.gameObject.SetActive(b);
    } 
}
