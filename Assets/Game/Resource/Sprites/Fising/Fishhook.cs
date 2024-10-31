using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishhook : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���� ���콺 ��ư Ŭ��
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Ŭ���� ������Ʈ: " + hit.collider.gameObject.name);
            }
        }
    }
}
