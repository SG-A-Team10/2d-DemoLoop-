// -----------------------------------------------------------------------------------------
// using classes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// -----------------------------------------------------------------------------------------
// player movement class
public class PlayerMovement : MonoBehaviour
{
    Vector2 motionVector;
    public Vector2 lastmotionVector;
    Animator animator;
    public bool moving; // ĳ���Ͱ� �����̴� ���θ� ������ ��Ƽ� ��, ����

    // static public members
    public static PlayerMovement instance;

    // -----------------------------------------------------------------------------------------
    // public members
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    // -----------------------------------------------------------------------------------------
    // private members
    private Vector2 movement;

    // -----------------------------------------------------------------------------------------
    // awake method to initialisation
    void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    // -----------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        float horizontal = movement.x;
        float vertical = movement.y;

        // update members
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        moving = horizontal != 0 || vertical != 0;
        animator.SetBool("moving", moving);

        

        if (horizontal != 0 || vertical != 0) 
        {
            lastmotionVector = new Vector2(horizontal, vertical).normalized;
            // ����ȭ(normalization)�� ������ ���̸� 1�� ����� ������ ���մϴ�.
            // ������ ������ �״�� �����ϸ鼭 ���̸� 1�� ����
            animator.SetFloat("lastHorizontal", horizontal);
            animator.SetFloat("lastVertical", vertical);
        }
    }
    // -----------------------------------------------------------------------------------------
    // fixed update methode
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
