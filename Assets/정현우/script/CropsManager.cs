using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

[Serializable]
public class CropTile
{
    // �۹��� ���� �ð�(�󸶳� �ð��� ��������)
    public int growTimer;

    // �۹��� ���� �ܰ�(�� �ܰ���� �ڶ�����)
    public int growStage;

    // �ɾ��� �۹� ����
    public Crop crop;

    // �۹��� ����� ������ SpriteRenderer (�̹��� ������)
    public SpriteRenderer renderer;

    // �۹��� ���� ���ط� (�󸶳� �ջ�Ǿ�����)
    public float damage;

    // Ÿ���� ��ġ ���� (Ÿ�� ��ǥ)
    public Vector3Int position;

    // �۹��� ������ �ڶ����� Ȯ���ϴ� �Ӽ�
    public bool Complete
    {
        get
        {
            // ���� crop�� null(�ɾ����� ���� ����)��� false ��ȯ
            if (crop == null) { return false; }
            // growTimer�� crop�� ���� �ð��� �Ѿ����� Ȯ���Ͽ� ��ȯ
            return growTimer >= crop.TimeToGrow;
        }
    }

    // �۹��� ��Ȯ�Ǿ��� �� ����Ǵ� �Լ�
    internal void Harvested()
    {
        // ��� ���¸� �ʱ�ȭ
        growTimer = 0;
        growStage = 0;
        crop = null;  // �۹��� ��������.
        renderer.gameObject.SetActive(false);  // ȭ�鿡�� �� ���̰� ����
        damage = 0;  // ���ط� �ʱ�ȭ
    }
}


public class CropsManager : MonoBehaviour
{
    public TilemapCropsManager cropsManager;

    public void PickUp(Vector3Int position)
    {
        if (cropsManager == null)
        {
            Debug.LogWarning("No tilemap crops manager are referenced in the crops manager");
            return; 
        }

        cropsManager.PickUp(position);

    }

    public bool Check(Vector3Int position)
    {
        if (cropsManager == null)
        {
            Debug.LogWarning("No tilemap crops manager are referenced in the crops manager");
            return false;
        }

        return cropsManager.Check(position);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        if (cropsManager == null)
        {
            Debug.LogWarning("No tilemap crops manager are referenced in the crops manager");
            return;
        }

        cropsManager.Seed(position, toSeed);
    }

    public void Plow(Vector3Int position) 
    {
        if (cropsManager == null)
        {
            Debug.LogWarning("No tilemap crops manager are referenced in the crops manager");
            return;
        }

        cropsManager.Plow(position);
    }
   
}