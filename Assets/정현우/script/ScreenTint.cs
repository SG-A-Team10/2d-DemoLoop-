using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTint : MonoBehaviour
{
    [SerializeField]
    Color unTintedColor;
    [SerializeField]
    Color tintedColor;
    float f;
    public float speed = 0;

    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Tint()
    {
        StopAllCoroutines();
        f = 0f; // 0�� ���� ����, 1�� ��ȭ�� ����
        StartCoroutine(TintScreen());
    }

    public void UnTint()
    {
        StopAllCoroutines();
        f = 0f; // 0�� ���� ����, 1�� ��ȭ�� ����
        StartCoroutine(UnTintScreen());
    }

    private IEnumerator TintScreen()
    {
        while (f < 1f)
        {
            // ������ ���� �ð� ���̸� ��Ÿ����, �̸� ���� f�� ���������� ����
            f += Time.deltaTime * speed; 
            // f�� ���� 0�� 1 ���̷� �����մϴ�. �̷��� �ϸ� f�� 1�� �ʰ����� �ʵ��� ����
            f = Math.Clamp(f, 0f, 1f);

            Color c = image.color;

            // unTintedColor�� tintedColor ������ ������ f�� ����Ͽ� ����մϴ�.
            // f�� 0�� ���� unTintedColor, 1�� ���� tintedColor�� ��ȯ��. 
            // Color.Lerp�� �� ���� ���� ������ ������ �� RGB ���Ӹ� �ƴ϶� ���� ���� �Բ� ���� �߿�
            c = Color.Lerp(unTintedColor, tintedColor, f); // 
            image.color = c;

            // ���� �������� ������ ��ٸ� �� ���� ������ �����ϵ��� �մϴ�.
            // �̷��� �ϸ� �� �����Ӹ��� ������ ���ݾ� ��ȭ
            yield return new WaitForEndOfFrame();
        }
        //UnTint();  //�����ε�
    }

    private IEnumerator UnTintScreen()
    {
        while (f < 1f)
        {
            f += Time.deltaTime * speed;
            f = Math.Clamp(f, 0f, 1f);

            Color c = image.color;
            c = Color.Lerp(tintedColor, unTintedColor, f);
            image.color = c;

            yield return new WaitForEndOfFrame();
        }
    }
}
