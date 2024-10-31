using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TilemapCropsManager : TimeAgent
{
    // ���� �� Ÿ�� �̹���
    [SerializeField] TileBase plowed;

    // ������ �ɾ��� Ÿ�� �̹���
    [SerializeField] TileBase seeded;

    // �۹��� ǥ�õ� Ÿ�ϸ�
    Tilemap targetTilemap;

    // �۹��� �̹����� ǥ���� ������Ʈ(prefab)
    [SerializeField] GameObject cropsSpritePerfab;

    [SerializeField] CropsContainer container;

    private void Start()
    {
        GameManager.Instance.GetComponent<CropsManager>().cropsManager = this;
        targetTilemap = GetComponent<Tilemap>();
        onTimeTick += Tick;
        Init();
        VisualizeMap();
        
    }

    private void VisualizeMap() 
    { 
        for(int i = 0; i < container.crops.Count; i++) 
        {
            VisualizeTile(container.crops[i]);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i<container.crops.Count; i++) 
        {
            container.crops[i].renderer = null;    
        }
    }


    // �ð��� �带 ������ �۹� ���¸� �����ϴ� �Լ�
    public void Tick()
    {
        if (targetTilemap == null) { return; }

        // ��� �۹� Ÿ���� �ϳ��� Ȯ��
        foreach (CropTile cropTile in container.crops)
        {
            // ���� Ÿ�Ͽ� �ɾ��� �۹��� ���ٸ� �������� �Ѿ
            if (cropTile.crop == null) { continue; }

            // �ð��� �����鼭 �۹��� �ջ��
            cropTile.damage += 0.02f;

            // �ջ��� 1�� ������ �۹��� �������Ƿ� ��Ȯ ó��
            if (cropTile.damage > 1f)
            {
                cropTile.Harvested();  // ��Ȯ
                targetTilemap.SetTile(cropTile.position, plowed);  // �ٽ� ������ ����
                continue;
            }

            // �۹��� ������ �ڶ����� �ƹ��͵� ���� ����
            if (cropTile.Complete)
            {
                Debug.Log("i am done growing");
                continue;
            }

            // ���� �ð��� 1�� ����
            cropTile.growTimer += 1;

            // ���� �ܰ��� ���� �ð��� �� �Ǹ� ���� �ܰ�� �Ѿ
            if (cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
            {
                cropTile.renderer.gameObject.SetActive(true);  // �۹� �̹����� ȭ�鿡 ǥ��
                cropTile.renderer.sprite = cropTile.crop.sprtes[cropTile.growStage];  // �ش� �ܰ��� �̹����� ����

                cropTile.growStage += 1;  // ���� �ܰ�� �̵�
            }
        }
    }

    internal bool Check(Vector3Int position)
    {
        // �����Ͱ� �����ϸ� true, �������� ������ false�� ��ȯ
        return container.Get(position) != null;
    }

    // ���� ���� �Լ�
    public void Plow(Vector3Int position)
    {
        if (Check(position) == true)
        {
            return;
        }

        // ���ο� �� Ÿ���� ����
        CreatePlowedTile(position);
    }

    // ������ �ɴ� �Լ�
    public void Seed(Vector3Int position, Crop toSeed)
    {
        CropTile tile = container.Get(position);

        if (tile == null) {  return; }

        // ������ �ɾ��� Ÿ�� �̹����� ����
        targetTilemap.SetTile(position, seeded);

        // �ش� ��ġ�� ���� ������ ����
        tile.crop = toSeed;
    }

    public void VisualizeTile(CropTile cropTile)
    {
        targetTilemap.SetTile(cropTile.position, cropTile.crop != null ? seeded : plowed);

        if (cropTile.renderer == null)
        {
            GameObject go = Instantiate(cropsSpritePerfab, transform);  // �۹� �̹����� ǥ���� ������Ʈ ����

            // Ÿ�ϸ� ��ǥ�� ���� ��ǥ�� ��ȯ�Ͽ� ��ġ ����
            go.transform.position = targetTilemap.CellToWorld(cropTile.position);
            go.transform.position -= Vector3.forward * 0.01f;
            cropTile.renderer = go.GetComponent<SpriteRenderer>();  // SpriteRenderer ����
        }

        bool growing = cropTile.crop != null && cropTile.growTimer >= cropTile.crop.growthStageTime[0];

        cropTile.renderer.gameObject.SetActive(true);  // �۹� �̹����� ȭ�鿡 ǥ��

        if (growing == true)
        {
            cropTile.renderer.sprite = cropTile.crop.sprtes[cropTile.growStage-1];  // �ش� �ܰ��� �̹����� ����
        }

    }

    // ���ο� �� Ÿ���� �����ϴ� �Լ�
    private void CreatePlowedTile(Vector3Int position)
    {

        CropTile crop = new CropTile();  // ���ο� CropTile ��ü ����
        container.Add(crop);  // ��ųʸ��� �߰�

        crop.position = position;  // Ÿ�� ��ġ ����

        VisualizeTile(crop);

        targetTilemap.SetTile(position, plowed);  // �ش� Ÿ���� ������ ����
    }

    // �۹��� ��Ȯ�ϴ� �Լ�
    internal void PickUp(Vector3Int gridPosition)
    {
        Vector2Int position = (Vector2Int)gridPosition;
        CropTile tile = container.Get(gridPosition);
        if (tile == null){ return; }

        // ���� �۹��� ������ �ڶ��ٸ�
        if (tile.Complete)
        {
            // �������� �����Ͽ� �ش� ��ġ�� ����߸�
            itemSpawnManager.instance.SpawnItem
                (
                targetTilemap.CellToWorld(gridPosition),
                tile.crop.yield,  // �۹� ��Ȯ��
                tile.crop.count  // ��Ȯ�� ������ ����
                );

            tile.Harvested();  // ��Ȯ �� �ʱ�ȭ
            VisualizeTile (tile);
        }
    }
}
