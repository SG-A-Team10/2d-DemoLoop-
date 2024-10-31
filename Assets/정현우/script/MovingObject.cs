// 2024-9-15, 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어가 움직이는 기능을 처리하는 클래스
public class MovingObject : MonoBehaviour
{
    // 캐릭터가 부딪힐 수 있는 충돌 박스 (콜라이더)
    private BoxCollider2D boxCollider;

    // 캐릭터가 충돌할 레이어를 설정 (벽이나 장애물)
    public LayerMask layerMask;

    // 캐릭터가 움직이는 속도
    public float speed;

    // 캐릭터가 움직일 방향을 저장하는 벡터
    private Vector3 vector;

    // 대쉬할 때의 속도
    public int runSpeed;

    // 실제 적용되는 대쉬 속도
    private int applyRunSpeed;

    // 대쉬 중인지 여부를 저장하는 변수
    private bool applyRunFlag = false;

    // 한 번에 몇 걸음씩 이동할 것인지 결정
    public float walkCount;

    // 현재 걸은 횟수
    private float currentWalkCount;

    // 캐릭터가 움직일 수 있는지 여부를 저장하는 변수
    private bool canMove = true;

    // 캐릭터의 애니메이션을 제어하는 컴포넌트
    private Animator animator;

    // 게임 시작 시 호출되는 함수 (초기화)
    private void Start()
    {
        // 캐릭터의 콜라이더와 애니메이터를 가져옴
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // 캐릭터가 움직이는 동작을 처리하는 함수
    IEnumerator MoveCoroutine()
    {
        // 플레이어가 움직이는 방향키를 누르고 있을 때 실행
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            // 만약 왼쪽 Shift키가 눌리면 대쉬 속도를 적용
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed; // 대쉬 속도 적용
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0; // 대쉬 안함
                applyRunFlag = false;
            }

            // 플레이어의 이동 방향을 가져옴 (x, y 좌표)
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

            // 상하, 좌우 동시에 눌렀을 때 상하 움직임 무시 (이상한 움직임 방지)
            if (vector.x != 0)
                vector.y = 0;

            // 애니메이션에서 방향을 설정 (x축, y축)
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            // 캐릭터의 현재 위치와 이동할 위치를 계산
            RaycastHit2D hit;
            Vector2 start = transform.position; // 캐릭터 현재 위치
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount) / 100;
            // 이동할 목표 위치

            // 캐릭터의 콜라이더를 비활성화 (자기 자신과 충돌하지 않기 위해)
            boxCollider.enabled = false;

            // 레이캐스트로 이동할 경로에 장애물이 있는지 확인
            hit = Physics2D.Linecast(start, end, layerMask);

            // 콜라이더를 다시 활성화
            boxCollider.enabled = true;

            // 만약 충돌하는 객체가 있으면 이동 중단
            if (hit.transform != null)
                break;

            // 이동 중이면 애니메이션 실행
            animator.SetBool("Walking", true);

            // 이동할 거리만큼 걸을 때까지 반복
            while (currentWalkCount < walkCount)
            {
                // x축으로 이동
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed) / 100, 0, 0);
                }
                // y축으로 이동
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed) / 100, 0);
                }

                // 대쉬 중일 경우 더 빠르게 걷도록 카운트 증가
                if (applyRunFlag)
                {
                    currentWalkCount++;
                }
                currentWalkCount++;

                // 잠깐 멈추고 다시 실행 (0.01초마다)
                yield return new WaitForSeconds(0.01f);
            }

            // 이동이 끝나면 현재 걸음 수를 초기화
            currentWalkCount = 0;
        }

        // 이동이 끝나면 걷기 애니메이션 중지
        animator.SetBool("Walking", false);
        // 다시 움직일 수 있게 설정
        canMove = true;
    }

    // 매 프레임마다 호출되는 함수 (키 입력 체크)
    private void Update()
    {
        // 캐릭터가 움직일 수 있는 상태에서
        if (canMove)
        {
            // 플레이어가 이동 키를 누르면 이동 코루틴 실행
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false; // 움직이는 동안은 다른 입력을 받지 않음
                StartCoroutine(MoveCoroutine()); // 이동 시작
            }
        }
    }
}
