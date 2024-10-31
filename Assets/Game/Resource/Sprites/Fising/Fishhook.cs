using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishhook : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 왼쪽 마우스 버튼 클릭
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("클릭한 오브젝트: " + hit.collider.gameObject.name);
            }
        }
    }
}
