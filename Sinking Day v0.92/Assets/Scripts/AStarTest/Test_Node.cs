using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Node
{
    public bool _canWalk;
    public Vector3 _worldPos;
    public int _girdX, _girdY;

    public int gCost, hCost;

    public Test_Node parent;

    public int fCost
    {
        get { return gCost + hCost; }
    }

    public Test_Node(bool canWalk, Vector3 position, int gridX, int gridY)
    {
        _canWalk = canWalk;
        _worldPos = position;
        _girdX = gridX;
        _girdY = gridY;
    }
}
