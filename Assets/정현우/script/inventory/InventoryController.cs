using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject toolbarPanel;
    [SerializeField] GameObject Mark;
    [SerializeField] GameObject dialougePanel;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && dialougePanel.activeInHierarchy == false)
        {
            Mark.SetActive(!panel.activeInHierarchy);
            panel.SetActive(!panel.activeInHierarchy);
            // activeInHierarchy, 현재 활성화 상태를 확인하고, 그 상태를 반전
            toolbarPanel.SetActive(!toolbarPanel.activeInHierarchy);
            Mark.SetActive(!panel.activeInHierarchy);
        }
    }
}
