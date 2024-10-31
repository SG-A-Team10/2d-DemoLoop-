// 2024-9-15, ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾ �����̴� ����� ó���ϴ� Ŭ����
public class MovingObject : MonoBehaviour
{
    // ĳ���Ͱ� �ε��� �� �ִ� �浹 �ڽ� (�ݶ��̴�)
    private BoxCollider2D boxCollider;

    // ĳ���Ͱ� �浹�� ���̾ ���� (���̳� ��ֹ�)
    public LayerMask layerMask;

    // ĳ���Ͱ� �����̴� �ӵ�
    public float speed;

    // ĳ���Ͱ� ������ ������ �����ϴ� ����
    private Vector3 vector;

    // �뽬�� ���� �ӵ�
    public int runSpeed;

    // ���� ����Ǵ� �뽬 �ӵ�
    private int applyRunSpeed;

    // �뽬 ������ ���θ� �����ϴ� ����
    private bool applyRunFlag = false;

    // �� ���� �� ������ �̵��� ������ ����
    public float walkCount;

    // ���� ���� Ƚ��
    private float currentWalkCount;

    // ĳ���Ͱ� ������ �� �ִ��� ���θ� �����ϴ� ����
    private bool canMove = true;

    // ĳ������ �ִϸ��̼��� �����ϴ� ������Ʈ
    private Animator animator;

    // ���� ���� �� ȣ��Ǵ� �Լ� (�ʱ�ȭ)
    private void Start()
    {
        // ĳ������ �ݶ��̴��� �ִϸ����͸� ������
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // ĳ���Ͱ� �����̴� ������ ó���ϴ� �Լ�
    IEnumerator MoveCoroutine()
    {
        // �÷��̾ �����̴� ����Ű�� ������ ���� �� ����
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            // ���� ���� ShiftŰ�� ������ �뽬 �ӵ��� ����
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed; // �뽬 �ӵ� ����
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0; // �뽬 ����
                applyRunFlag = false;
            }

            // �÷��̾��� �̵� ������ ������ (x, y ��ǥ)
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

            // ����, �¿� ���ÿ� ������ �� ���� ������ ���� (�̻��� ������ ����)
            if (vector.x != 0)
                vector.y = 0;

            // �ִϸ��̼ǿ��� ������ ���� (x��, y��)
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            // ĳ������ ���� ��ġ�� �̵��� ��ġ�� ���
            RaycastHit2D hit;
            Vector2 start = transform.position; // ĳ���� ���� ��ġ
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount) / 100;
            // �̵��� ��ǥ ��ġ

            // ĳ������ �ݶ��̴��� ��Ȱ��ȭ (�ڱ� �ڽŰ� �浹���� �ʱ� ����)
            boxCollider.enabled = false;

            // ����ĳ��Ʈ�� �̵��� ��ο� ��ֹ��� �ִ��� Ȯ��
            hit = Physics2D.Linecast(start, end, layerMask);

            // �ݶ��̴��� �ٽ� Ȱ��ȭ
            boxCollider.enabled = true;

            // ���� �浹�ϴ� ��ü�� ������ �̵� �ߴ�
            if (hit.transform != null)
                break;

            // �̵� ���̸� �ִϸ��̼� ����
            animator.SetBool("Walking", true);

            // �̵��� �Ÿ���ŭ ���� ������ �ݺ�
            while (currentWalkCount < walkCount)
            {
                // x������ �̵�
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed) / 100, 0, 0);
                }
                // y������ �̵�
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed) / 100, 0);
                }

                // �뽬 ���� ��� �� ������ �ȵ��� ī��Ʈ ����
                if (applyRunFlag)
                {
                    currentWalkCount++;
                }
                currentWalkCount++;

                // ��� ���߰� �ٽ� ���� (0.01�ʸ���)
                yield return new WaitForSeconds(0.01f);
            }

            // �̵��� ������ ���� ���� ���� �ʱ�ȭ
            currentWalkCount = 0;
        }

        // �̵��� ������ �ȱ� �ִϸ��̼� ����
        animator.SetBool("Walking", false);
        // �ٽ� ������ �� �ְ� ����
        canMove = true;
    }

    // �� �����Ӹ��� ȣ��Ǵ� �Լ� (Ű �Է� üũ)
    private void Update()
    {
        // ĳ���Ͱ� ������ �� �ִ� ���¿���
        if (canMove)
        {
            // �÷��̾ �̵� Ű�� ������ �̵� �ڷ�ƾ ����
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false; // �����̴� ������ �ٸ� �Է��� ���� ����
                StartCoroutine(MoveCoroutine()); // �̵� ����
            }
        }
    }
}
