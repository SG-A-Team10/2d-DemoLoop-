using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class AGrid : MonoBehaviour
{
    // Start is called before the first frame update

    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    ANode[,] grid;

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;
    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter); //가로 길이
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter); //세로 길이
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new ANode[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2- Vector3.forward * gridWorldSize.y / 2;
        Vector3 worldPoint;
        for(int x = 0; x<gridSizeX; x++){
            for(int y = 0; y<gridSizeY; y++)
            {
                worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter* nodeRadius) + Vector3.forward * (y * nodeDiameter+nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new ANode(walkable, worldPoint, x, y);
            }
        }
    }

    //노드의 주변 노드(a방면)를 찾는 함수
    public List<ANode> GetNeighbours(ANode node)//GetNodeFromWorldPoint는 유니티상의 worldPosition의 위치값으로 생성된 그리드상의 위치로 변환하여 그 위치의 노드를 반환
    //씬에 있는 현재 오브젝트와, 목표 오브젝트가 있는 노드를 얻기 위한 함수
    //GetNeighbours : 선택된 노드의 8방면상의 이웃노드를 반환하는 함수(열린목록에 추가하기위한 함수)
    {
        List<ANode> neighbours = new List<ANode>();
        for(int x = -1; x <=1; x++){
            for(int y = -1; y<=1; y++){
                if(x == 0 &&y==0 ) continue; //자기자신인 경우 스킵
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                //x, y의 값이 Grid범위 안에 있을 경우
                if(checkX >= 0 && checkX < gridSizeX && checkY >=0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }

            }
        }
        return neighbours;
    }
    public ANode GetNodeFromWorldPoint(Vector3 worldPosition){
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.x + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }
    //유니티의 worldPosition으로부터 그리드상의 노드를 찾는 함수
    public List<ANode> path;
    private void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if(grid!=null){
            foreach(ANode n in grid){
                Gizmos.color = (n.isWalkAble) ? Color.white : Color.red;
                if(path != null)
                    if(path.Contains(n))
                        Gizmos.color = Color.black;
                Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
