// 2024-10-27, ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� Ŭ������ ���۹� �����͸� �����ϴ� ����

[CreateAssetMenu(menuName = "Data/Crops Container")]
public class CropsContainer : ScriptableObject
{

    // CropTile�� ���۹��� ������ ��� �ִ� Ŭ����
    public List<CropTile> crops;

    public CropTile Get(Vector3Int position)
    {
        // crops ����Ʈ���� position�� ��ġ�ϴ� ���۹��� ã�� ��ȯ�մϴ�. ���� ��ġ�ϴ� ���۹��� ���ٸ� null�� ��ȯ
        // x�� crops ����Ʈ�� �� ���۹� ��ü�� ����Ű��,
        // Find �޼���� position �Ӽ��� �־��� position�� ��ġ�ϴ� ù ��° ���۹� ��ü�� ��ȯ
        return crops.Find(x => x.position == position);
    }

    public void Add(CropTile crop)
    { 
        crops.Add(crop);
    }
}
