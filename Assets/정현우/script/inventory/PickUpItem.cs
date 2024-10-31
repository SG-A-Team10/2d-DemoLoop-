// 2024-09-07 �ۼ��� : ������

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    Transform player; //�÷��̾� ��ġ
    [SerializeField] float speed = 5f; //�÷��̾ ���� ������ ��ü�� �̵��ϴ� �ӵ�;
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float ttl = 10f; // ���� �ð� �ȿ� ������ �� ������ �Ҹ�

    public item item;
    public int count = 1;

    private void Awake()
    {
        player = GameManager.Instance.player.transform;
    }

    // �׸�� ������ �����ϰ� ���⿡ �����ϴ� Set �޼ҵ�
    public void Set(item item, int count)
    { 
        this.item = item;
        this.count = count; 

        // �������� ��������Ʈ�� ������ ������ ��������Ʈ�� ��ü�ϴ� �뵵
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.icon;
    }




    private void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl < 0) { Destroy(gameObject); }

        // ��ü�� �÷��̾� ������ �Ÿ��� ������ ����
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > pickUpDistance)
        {
            return;
        }
        // �������� �÷��̾ ���� �̵�    
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        // �����۰� �÷��̾� �Ÿ��� 0.1f ���ϸ� ������ �ı�
        if (distance < 0.1f)
        {
            if (GameManager.Instance.inventoryContainer != null)
            {
                GameManager.Instance.inventoryContainer.Add(item, count);
            }
            else
            {
                Debug.LogWarning("No inventory container attached to the GameManager");
            }
            Destroy(gameObject);
        }
    }
}
