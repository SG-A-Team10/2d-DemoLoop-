// 2024-09-07 �ۼ��� : ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHit : MonoBehaviour
{
    // virtual: �޼��带 �������� ������, �ڽ� Ŭ�������� �������� �� �ֵ��� ����ϴ� Ű����
    public virtual void Hit()
    {
    
    }

    public virtual bool CanBeHit(List<ResourceNodeType> canBehit)
    {
        return true;
    }
}

