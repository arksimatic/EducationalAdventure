/*
MIT License

Copyright (c) 2017 Sebastian Lague

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

Github: https://github.com/SebLague/Pathfinding/tree/master/Episode%2003%20-%20astar/Assets/Scripts
*/



using System.Collections.Generic;
using UnityEngine;

public class NodesTable : MonoBehaviour
{
    public bool Drawing=false;
    public LayerMask UnWalkableMask;// warstwa dla kolizji
    public float nodeRadius;//wielkosc kostek
    public float space;// pomiedzy kostkami ale do rysowanka tylko
    public Vector2 gridWorldSize;
    public Node[,] grid;
    public List<Node> path;
    private float nodeDiameter;
    private int gridSizeX, gridSizeY;
    bool rescale = false;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
        for (int i = 0; i < gridSizeX; i++){
            for (int j = 0; j < gridSizeY; j++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius) +
                                     Vector3.up * (j * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapBox(worldPoint,new Vector2(nodeRadius,nodeRadius),0.0f,UnWalkableMask));
                grid[i,j]= new Node(walkable,worldPoint,i,j);
            }
        }

    }

    void Update()
    { 
        if(rescale == false)
        {
            rescale = true;
            CreateGrid();
        }
    }

    public List<Node> GetNeighbours(Node node)//zawsze dbbrze jest miec sasiada:D, szuka sasiadów noda potrzebne wwyszukiwaniu drogi.
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)//ignoruje noda wlasciwego 
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)//przerabia pozycje na najblizszego Noda
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }
    public Node FindNearestAvailable(Node targetNode)
    {
        Node WalkAble=targetNode;
        Node OldNeighbour=new Node(true,Vector2.zero,200,200);
        
        foreach (Node neighbour in grid)// przeskakuje po wszystkich nodach i szuka najblizszego
        {
            if (!neighbour.walkable)
            {
                continue;
            }

            
            if (GetDistance(targetNode, neighbour) < GetDistance(targetNode, OldNeighbour))
            {
                
                OldNeighbour = neighbour;


            }
        }

        return OldNeighbour;

    }


    void OnDrawGizmos()// rysuje "szachowanice"
    {
        if (Drawing == true)
        {
            Gizmos.DrawWireCube(transform.position, new Vector2(gridWorldSize.x, gridWorldSize.y));
            if (grid != null)
            {
                foreach (Node n in grid)
                {
                    Gizmos.color = (n.walkable) ? Color.white : Color.red;

                    if (path != null)
                        if (path.Contains(n))
                            Gizmos.color = Color.black;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - space));
                }
            }
        }
    }

    public int GetDistance(Node nodeA, Node nodeB)// liczy dystans miedzy nodami
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }

}

