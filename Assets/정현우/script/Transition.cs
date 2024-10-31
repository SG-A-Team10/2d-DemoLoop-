using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public enum TrasitionType
{ 
    Warp,
    Scence
}

public class Transition : MonoBehaviour
{
    [SerializeField] TrasitionType transitionType;
    [SerializeField] string sceneNameToTransition;
    [SerializeField] Vector3 targetPosition;

    Transform destination;

    void Start()
    {
        //  transform.GetChild(1)�� ���� ��ũ��Ʈ�� �پ� �ִ� ���� ������Ʈ��
        //  �� ��° �ڽ�(�ε����� 0���� �����ϹǷ� 1�� �� ��° �ڽ�)�� �������� ���Դϴ�.
        //  ��, destination ������ ���� ���� ������Ʈ�� �� ��° �ڽ��� Transform�� ����
        
        destination = transform.GetChild(1); // Destination ��ü�� ������
    }

    internal void InitiateTransition(Transform toTransition)
    {

        //Cinemachine.CinemachineBrain currentCamera = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();

        switch (transitionType) 
        {
            case TrasitionType.Warp:  // �Ϲ������� ���� �̵� �Ǵ� ���� �̵�
                
                //currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(toTransition, destination.position - toTransition.position);
                
                toTransition.position = new Vector3(destination.position.x, destination.position.y, toTransition.position.z);
                break;

            case TrasitionType.Scence: // Scene�� ����Ƽ���� �ٸ� ������ �̵��ϴ� ��

                //currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(toTransition, targetPosition - toTransition.position);
                
                GameScenceManager.instance.InitSwitchScene(sceneNameToTransition, targetPosition);
                break;
        }
 
    }
}
