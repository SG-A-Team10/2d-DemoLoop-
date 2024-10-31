// �ۼ��� : ������, ��¥ : 2024-09-06 

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static CameraManager instance;

    public GameObject target; // ī�޶� ���󰡴� ��� ex) player

    //public BoxCollider2D bound; // ī�޶� ���� ������ ���� �ڽ��ݶ��̴�

    private Vector3 minBound; // �ڽ� �ݶ��̴� ������ �ִ��� ����
    private Vector3 maxBound; // �ڽ� �ݶ��̴� ������ �ּҰ��� ����

    private float halfWidth; // ī�޶��� ���� ����(ī�޶� �� ������ ���� ������ �� ����)
    private float halfHeight; // ī�޶��� ���� ����(ī�޶� �� ������ ���� ������ �� ����)

    private Camera theCamera; // �̰� ī�޶��� �ݳ��� ���� ���ϱ� ���� ����, Camera�� �Ӽ��� Ȱ��

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    private Vector3 targetPosition;

    void Start()
    {
        theCamera = GetComponent<Camera>();
        //minBound = bound.bounds.min;
        //maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize; // ī�޶��� ������(size) ������ 
        halfWidth = halfHeight * Screen.width / Screen.height; // �ݳʺ� ���ϴ� ������ ����
        // Screen.width / Screen.height << ���� �ػ� ��Ÿ��, 
    }

    void LateUpdate()
    {
        if (target.gameObject != null)
        {
            this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z);


            // Clamp �Լ��� 3���� �μ��� ���� (��, �ּҰ�, �ִ밪)
            // (10, 0, 100)
            // ���� 10�̸� ���ϰ��� 10�̴�. ������ 10�� 0~100 ���̿� �ֱ� �����̴�
            // (-100, 0, 100)
            // ���� -100�̸� ���ϰ��� 0�� �ȴ�. ������ 10�� 0~100 ���̸� ����� �����̴�
            // (1000, 0, 100)
            // ���� 1000�̸� ���ϰ��� 100�� �ȴ�. ������ 1000�� 0~100 ���̸� ����� �����̴�

            // Clamp �Լ��� Ȱ���Ͽ�, ī�޶� ���� ����
            // �ּڰ��� �ݳ���, �ݳʺ� ���ϰ�, �ִ��� �ݳ���, �ݳʺ� ����
            //float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            //float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            //ī�޶� ��ġ ����
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
    }
}
