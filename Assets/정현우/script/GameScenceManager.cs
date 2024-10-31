using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScenceManager : MonoBehaviour
{
    public static GameScenceManager instance; //���� ����, Ŭ������ ��� �ν��Ͻ��� �����ϴ� ����

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] ScreenTint screenTint;
    string currentScence;

    public void InitSwitchScene(string to, Vector3 targetPosition)
    {
        StopAllCoroutines();
        StartCoroutine(Transition(to, targetPosition));
    }

    IEnumerator Transition(string to, Vector3 targetPosition)
    {
        screenTint.Tint();
        yield return new WaitForSeconds(1f / screenTint.speed + 0.1f);

        SwitchScene(to, targetPosition);
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.1f);
        screenTint.UnTint();
    }

    void Start()
    {
        currentScence = SceneManager.GetActiveScene().name;
    }


    public void SwitchScene(string to, Vector3 targetPosition)
    {
        // �ε��� ���� ���� ���� ������ Ȯ��
        if (currentScence == to)
        {
            Debug.Log("�̹� ���� �ε�Ǿ� �ֽ��ϴ�: " + to);
            return; // ���� �̹� �ε�Ǿ� ������ �޼��带 ����
        }

        // ������ ��(to)�� Additive ���� �ε�
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);

        // �� �ε尡 �Ϸ�� ������ ���
        loadOperation.completed += (AsyncOperation op) =>
        {
            // ���� Ȱ��ȭ�� ��(currentScence)�� �񵿱������� ��ε�(����)
            SceneManager.UnloadSceneAsync(currentScence);

            // currentScence ������ ������Ʈ�Ͽ�,
            // ���� ���� �̸��� ���� ��ȯ�� ���� �̸����� ����
            currentScence = to;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScence));

            // �÷��̾ Ÿ�� ��ġ�� �̵�
            Transform playerTransform = GameManager.Instance.player.transform;
            playerTransform.position = new Vector3(targetPosition.x, targetPosition.y, playerTransform.position.z);
        };
    }
}
