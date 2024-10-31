// 2024-09-09, 10-06, �ۼ��� : ������


using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayTimeController : MonoBehaviour
{
    // �Ϸ翡 �ش��ϴ� �ð�(�� ����, 86400�� = 24�ð�)
    const float secondsInDay = 86400f;

    // 15�и��� �� ���� �߻��ϴ� �ð� ����
    const float phaseLength = 900f; // 15 minutes chunk of time

    // �� �ð����� ���� ����
    [SerializeField] Color nightLightColor;

    // �ð��� ���� ������ ��� ��ȭ�� �����ϴ� �
    [SerializeField] AnimationCurve nightTimeCurve;

    // �� �ð����� ���� ���� (�⺻������ ���)
    [SerializeField] Color dayLightColor = Color.white;

    // ���� �� �ð�
    float time;

    // ���� �ð��� ���� ���� �ð��� ���� �帣���� ���� (1�� = ���� �� 60��)
    [SerializeField] float timeScale = 60f;

    // ���� ���� ���� �ð��� ���� (28800�� = ���� 8��)
    [SerializeField] float startAtTime = 28800f;

    // �ð� ������ ǥ���� �ؽ�Ʈ UI
    [SerializeField] TextMeshProUGUI text;

    // �������� ������ �����ϴ� 2D ����Ʈ
    [SerializeField] Light2D globalLight;

    // ������ �� ���� ����
    private int days;

    // �ð��� �����ϴ� ��ü���� ����Ʈ (�ð��� ���� �����ϴ� �͵�)
    List<TimeAgent> agents;

    // ��ũ��Ʈ�� ó�� ����� �� ȣ��Ǵ� �Լ�
    private void Awake()
    {
        // �ð��� �����ϴ� ��ü���� ������ ����Ʈ�� �ʱ�ȭ
        agents = new List<TimeAgent>();
    }

    // ������ ���۵� �� ȣ��Ǵ� �Լ�
    private void Start()
    {
        // ���� ���� �� ������ �ð����� ����
        time = startAtTime;
    }

    // ���ο� TimeAgent�� ����Ʈ�� �߰��ϴ� �Լ�
    public void Subscribe(TimeAgent timeagent)
    {
        agents.Add(timeagent);
    }

    // ���� TimeAgent�� ����Ʈ���� �����ϴ� �Լ�
    public void UnSubscribe(TimeAgent timeagent)
    {
        agents.Remove(timeagent);
    }

    // ���� �ð��� �ð� ������ ��ȯ (�� -> �ð����� ��ȯ)
    float Hours
    {
        get { return time / 3600f; }
    }

    // ���� �ð��� �� ������ ��ȯ (�� -> ������ ��ȯ)
    float Minutes
    {
        get { return time % 3600f / 60f; }
    }

    // ���� ����� ���õ� �����Ӹ��� ȣ��Ǵ� �Լ�
    private void FixedUpdate()
    {
        // �ð� ����� ���� ���� �� �ð��� ���� (���� �ð��� ������ ����)
        time += Time.deltaTime * timeScale;

        // �ð� ���� ����ϰ� UI�� ǥ��
        TimeValueCalculation();

        // �ð��� ���� ���� ���� ������ ����
        DayLight();

        // �Ϸ簡 ������ ��� ���� ���� �Ѿ
        if (time > secondsInDay)
        {
            NextDay();
        }

        // 15�и��� �ð��� �����ϴ� ��ü���� ����
        TimeAgents();
    }

    // �ð� ��(��, ��)�� ����ϰ� UI �ؽ�Ʈ�� ǥ���ϴ� �Լ�
    private void TimeValueCalculation()
    {
        int hh = (int)Hours; // �ð��� ���������� ��ȯ
        int mm = (int)Minutes; // ���� ���������� ��ȯ
        // �ð��� "00:00" �������� �ؽ�Ʈ�� ǥ��
        text.text = hh.ToString("00") + ":" + mm.ToString("00");
    }

    // �ð��� ���� ���� ���� ���� ������ ����Ͽ� �����ϴ� �Լ�
    private void DayLight()
    {
        // �ð��� ���� ������ ��� ������ ���
        float v = nightTimeCurve.Evaluate(Hours);

        // ���� ���� ������ ��� ���� �ð��뿡 �´� ������ ���
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);

        // ������ ������ ����
        globalLight.color = c;
    }

    // ���� �ð����� ����(15��) ����
    int oldPhase = 0;

    // 15�и��� agents ����Ʈ�� ��� ��ü�鿡�� �ð��� �˷��ִ� �Լ�
    private void TimeAgents()
    {
        // ���� �ð��� 15�� ������ ���� �� ���
        int currentPhase = (int)(time / phaseLength);

        // ���� �ð��� 15���� ������(���ο� ������ �Ѿ��)
        if (oldPhase != currentPhase)
        {
            oldPhase = currentPhase; // ���� �ð��븦 ����
            // ����Ʈ�� �ִ� ��� ��ü���� �ð��� ������Ʈ
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Invoke(); // �ð��� ���� �����ؾ� �� ������ ȣ��
            }
        }
    }

    // �Ϸ簡 ������ �ð��� 0���� �����ϰ� �� ���� ������Ű�� �Լ�
    private void NextDay()
    {
        time = 0; // �ð��� 0���� ����
        days += 1; // ������ �� ���� ����
    }
}