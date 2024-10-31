using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookContainerIntereact : Interactable
{
    // 가상메서드를 상속하고 구현
    [SerializeField] GameObject closedChest;
    [SerializeField] GameObject openedChest;
    [SerializeField] bool opened;
    public override void Interact(Character character)
    {
        if (opened == false)
        { 
            opened = true;
            closedChest.SetActive(false);
            openedChest.SetActive(true);
        }
    }
}
