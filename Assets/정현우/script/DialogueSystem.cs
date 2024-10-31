// 2024-09-29 �ۼ��� : ������

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// DialogueSystem: ���� �� ��ȭ�� ó���ϴ� �ý���
public class DialogueSystem : MonoBehaviour
{
    // ��ȭ�� ���� ������ ����
    public bool IsActive = false;

    // ��ȭ ������ ��µ� �ؽ�Ʈ (TextMeshProUGUI�� ����Ƽ�� �ؽ�Ʈ ������ ������Ʈ)
    [SerializeField] TextMeshProUGUI targetText;

    // ��ȭ ����� �̸��� ��µ� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI nameText;

    // ��ȭ ����� �ʻ�ȭ �̹���
    [SerializeField] Image portrait;

    // ���� ���� ���� ��ȭ ����(������� ��� ��ȭ �����̳�)
    DialogueContainer currentDialogue;

    // ���� ��� ���� ��ȭ ���� ��ȣ
    int currentTextLine;

    // �ؽ�Ʈ�� �󸶳� ��µǾ������� ������� ��Ÿ�� (0% ~ 100%)
    [Range(0f, 1f)]
    [SerializeField] float visibleTextPercent;

    // �� ���ڰ� ǥ�õǴ� �ӵ� (���ڴ� ������)
    [SerializeField] float timePerLetter = 0.05f;

    // ��ü ��縦 ����ϴ� �� �ɸ��� �ð��� ���� ����� �ð�
    float totalTimetoType, currentTime;

    // ����� ��� ��
    string lineToShow;


    // �� �����Ӹ��� ����Ǵ� �Լ�
    private void Update()
    {
        // �����̽��ٸ� ������ �� ��縦 �ѱ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PushText();
        }

        // ESC Ű�� ������ �� ��ȭâ�� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Conclude();
        }

        // ���� ��簡 ��µǴ� ���̸� ����ؼ� �ؽ�Ʈ�� ���
        TypeOutText();
    }

    // ��縦 �� ���ھ� Ÿ���� ȿ���� ����ϴ� �Լ�
    private void TypeOutText()
    {
        // ���� ��簡 �̹� �� ��µǾ����� �ƹ� �͵� ���� ����
        if (currentTime >= 1f) { return; }

        // �ð��� �����鼭 ��� ��� ���൵�� ���
        currentTime += Time.deltaTime;
        visibleTextPercent = currentTime / totalTimetoType;
        visibleTextPercent = Mathf.Clamp(visibleTextPercent, 0, 1f); // 0%���� 100% ���̷� ����
        UpdateText(); // �ؽ�Ʈ ������Ʈ
    }

    // ��� �ؽ�Ʈ�� ������Ʈ�ϴ� �Լ� (�Ϻθ� ���)
    void UpdateText()
    {
        // ���� ����� ���� ���� ���
        int letterCount = (int)(lineToShow.Length * visibleTextPercent);
        // ���� ���� ����ŭ �ؽ�Ʈ�� �߶� ǥ��
        targetText.text = lineToShow.Substring(0, letterCount);
    }

    // �����̽��ٸ� ������ �� ȣ��Ǵ� �Լ�
    private void PushText()
    {
        // ���� ��簡 ���� �� ��µ��� �ʾ�����, ���� ��縦 �� ���� ���
        if (visibleTextPercent < 1f)
        {
            visibleTextPercent = 1f;
            UpdateText();
            return;
        }

        // ��ȭ�� ����
        if (currentTextLine >= currentDialogue.line.Count)
        {
            Conclude();
        }
        else
        {
            // ���� ���� ��簡 ������ ���� �ٷ� �Ѿ
            CycleLine();
        }
    }

    // ���� ��� ���� �����ϴ� �Լ�
    void CycleLine()
    {
        // ���� ��� ���� ������
        lineToShow = currentDialogue.line[currentTextLine];

        // ���� ���� ���� ��� �ð��� ���
        totalTimetoType = lineToShow.Length * timePerLetter;

        // Ÿ�̸� �ʱ�ȭ �� �ؽ�Ʈ ����
        currentTime = 0f;
        visibleTextPercent = 0f;
        targetText.text = "";

        // ���� �ٷ� �Ѿ
        currentTextLine += 1;
    }

    // ��ȭ�� �ʱ�ȭ�ϴ� �Լ� (��ȭ ���� �� ȣ��)
    public void Initialize(DialogueContainer dialogueContainer)
    {
        Show(true); // ��ȭâ�� ���̰� ����
        currentDialogue = dialogueContainer; // ��� ������ ����
        currentTextLine = 0; // ù ��° �ٺ��� ����
        CycleLine(); // ù ��� �� ���
        UpdatePortrait(); // ĳ���� �ʻ�ȭ �� �̸� ������Ʈ
    }

    // ĳ������ �ʻ�ȭ�� �̸��� ������Ʈ�ϴ� �Լ�
    private void UpdatePortrait()
    {
        // ��翡 �ִ� ĳ������ �ʻ�ȭ�� �̸��� ������ ����
        portrait.sprite = currentDialogue.actor.portrait;
        nameText.text = currentDialogue.actor.Name;
    }

    // ��ȭâ�� ���̰� �ϰų� ����� �Լ�
    private void Show(bool v)
    {
        gameObject.SetActive(v);
    }

    // ��ȭ�� ������ �� ȣ��Ǵ� �Լ�
    private void Conclude()
    {
        Debug.Log("The dialogue has ended"); // ����� �޽��� ���
        Show(false); // ��ȭâ �����
    }
}