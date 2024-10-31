// 2024-10-06, �ۼ��� : ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeAgent : MonoBehaviour
{
    public Action onTimeTick; // ��������Ʈ, invoke�� ���� �޼��带 �����Ͽ� ���߿� ȣ���� �� �ִ� ���

    private void Start()
    {
        Init();  
    }

    public void Init()
    {
        //TimeAgent�� ������ �ð� �ý��ۿ� ����Ǿ� ���� ��ȭ�� ������ �� �ֵ��� �����ϴ� �ʱ�ȭ
        GameManager.Instance.TimeController.Subscribe(this);
    }

    public void Invoke()
    {
        onTimeTick?.Invoke();
        // onTimeTick�� null�� �ƴ� ���� Invoke()�� ȣ��
        // ��, �����ڰ� ���� ��쿡�� �ƹ� �͵� ȣ������ �ʰ� �����ϰ� �ѱ�
        // onTimeTick�� �ð��� ��ȭ�� �����ϴ� �޼��带 �����Ͽ�, �ð� ���� �̺�Ʈ�� ó���ϴ� �� ���
    }

    private void OnDestroy()
    {
        GameManager.Instance.TimeController.UnSubscribe(this);
    }
}
