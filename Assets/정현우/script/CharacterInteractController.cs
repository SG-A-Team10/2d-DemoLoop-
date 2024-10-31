using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractController : MonoBehaviour
{
    // ĳ������ �������� ����ϴ� ��ũ��Ʈ
    PlayerMovement characterController;
    // ĳ������ ������ �������� �����ϴ� Rigidbody2D
    Rigidbody2D rgdb2d;
    // ĳ���Ͱ� ��ȣ�ۿ��� ������ �Ÿ�
    [SerializeField] float offsetDistance = 1f;
    // ��ȣ�ۿ��� �� �ִ� ������ ũ��
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    // ĳ������ ���� (�̸�, ���� ��)
    Character character;

    // ��ȣ�ۿ��� �� �ִ� ��� ���콺�� �÷��� �� ���� ǥ�ø� ���ִ� ��ũ��Ʈ
    [SerializeField] HighlightController highlightController;

    // ��ũ��Ʈ�� ����Ǳ� ���� ó������ ����Ǵ� �Լ�
    private void Awake()
    {
        // PlayerMovement ������Ʈ�� ������ (ĳ���� �̵� ����)
        characterController = GetComponent<PlayerMovement>();

        // Rigidbody2D ������Ʈ�� ������ (ĳ������ ������ �̵� ����)
        rgdb2d = GetComponent<Rigidbody2D>();

        // Character ������Ʈ�� ������ (ĳ������ ����)
        character = GetComponent<Character>();
    }

    private void Update()
    {
        // ��ȣ�ۿ� ������ ����� üũ
        Check();

        // ������ ���콺 ��ư�� ������ �� ��ȣ�ۿ� �õ�
        if (Input.GetMouseButton(1)) 
        {
            Interact();
        }
    }

    // ��ȣ�ۿ��� ����� ã�Ƽ� ���� ǥ���ϴ� �Լ�
    private void Check()
    {
        // ĳ���� ���ʿ� ��ȣ�ۿ��� �� �ִ� ������ ����
        Vector2 position = rgdb2d.position + characterController.lastmotionVector * offsetDistance;

        // ������ ���� �ȿ� �ִ� ��� Collider2D�� ������
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        // ���� ���� �ִ� ��� ������Ʈ�� �ϳ��� Ȯ��
        foreach (Collider2D c in colliders)
        {
            // ��ȣ�ۿ��� �� �ִ� Interactable ������Ʈ�� ���� ������Ʈ���� Ȯ��
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                // ��ȣ�ۿ� ������ ������Ʈ��� ���� ǥ��
                highlightController.Highlight(hit.gameObject);
                return; // ��ȣ�ۿ� ������ ����� ������ �Լ� ����
            }
        }
        // ��ȣ�ۿ� ������ ����� ������ ���� ǥ�� ����
        highlightController.Hide();
    }

    // ������ ��ȣ�ۿ��� �ϴ� �Լ�
    private void Interact()
    {
        // ĳ���� ���ʿ� ��ȣ�ۿ��� �� �ִ� ������ ����
        Vector2 position = rgdb2d.position + characterController.lastmotionVector * offsetDistance;

        // ������ ���� �ȿ� �ִ� ��� Collider2D�� ������
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        // ���� ���� �ִ� ��� ������Ʈ�� �ϳ��� Ȯ��
        foreach (Collider2D c in colliders)
        {
            // ��ȣ�ۿ��� �� �ִ� Interactable ������Ʈ�� ���� ������Ʈ���� Ȯ��
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                // ��ȣ�ۿ� ������ ������Ʈ��� ��ȣ�ۿ� ����
                hit.Interact(character);
                break; // ù ��°�� ���� ��ȣ�ۿ� ������ ��ȣ�ۿ��ϰ� �Լ� ����
            }
        }
    }
}
