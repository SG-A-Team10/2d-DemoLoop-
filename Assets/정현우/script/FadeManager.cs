// �ۼ��� : ������, ��¥ : 2024-09-04 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public SpriteRenderer white;
    public SpriteRenderer black;
    private Color color;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    public void FadeOut(float _speed = 0.02f) {
        StopAllCoroutines(); // ���� �Դٰ����ϸ鼭 �ڷ�ƾ ��ø �����ϴ°� ���� //������ ����Ȱ� �켱���� �ο�
        StartCoroutine(FadeOutCoroutine(_speed));
    }


    IEnumerator FadeOutCoroutine(float _speed) { 

        color = black.color;
        while (color.a < 1f) { //�÷� ���� ���İ��� 1�� �� ������
            color.a += _speed;
            black.color = color;
            yield return waitTime; // new�� �����ڿ� ���ϸ� ���� ��
        }
    }

    public void FadeIn(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeInCoroutine(_speed));
    }


    IEnumerator FadeInCoroutine(float _speed)
    {

        color = black.color;
        while (color.a > 0f)
        { //�÷� ���� ���İ��� 0�� �� ������
            color.a -= _speed;
            black.color = color;
            yield return waitTime; // new�� �����ڿ� ���ϸ� ���� ��
        }
    }

    public void FlashOut(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashOutCoroutine(_speed));
    }


    IEnumerator FlashOutCoroutine(float _speed)
    {

        color = white.color;
        while (color.a < 1f)
        { //�÷� ���� ���İ��� 1�� �� ������
            color.a += _speed;
            white.color = color;
            yield return waitTime; // new�� �����ڿ� ���ϸ� ���� ��
        }
    }

    public void Flash(float _speed = 0.1f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine(_speed));
    }


    IEnumerator FlashCoroutine(float _speed) //��½ �Ÿ��°�
    {

        color = white.color;
        while (color.a < 1f)
        { //�÷� ���� ���İ��� 1�� �� ������
            color.a += _speed;
            white.color = color;
            yield return waitTime; // new�� �����ڿ� ���ϸ� ���� ��
        }
        while (color.a > 0)
        { //�÷� ���� ���İ��� 1�� �� ������
            color.a -= _speed;
            white.color = color;
            yield return waitTime; // new�� �����ڿ� ���ϸ� ���� ��
        }
    }



    public void FlashIn(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashInCoroutine(_speed));
    }


    IEnumerator FlashInCoroutine(float _speed)
    {

        color = white.color;
        while (color.a > 0)
        { //�÷� ���� ���İ��� 1�� �� ������
            color.a -= _speed;
            white.color = color;
            yield return waitTime; // new�� �����ڿ� ���ϸ� ���� ��
        }
    }

}
;