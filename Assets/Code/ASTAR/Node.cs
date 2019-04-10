
using UnityEngine;
using System.Collections;

public class Node
{

    public bool walkable;
    public Vector2 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;

    public Node(bool walk, Vector2 wPos, int X, int Y)
    {
        walkable = walk;
        worldPosition = wPos;
        gridX = X;
        gridY = Y;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}