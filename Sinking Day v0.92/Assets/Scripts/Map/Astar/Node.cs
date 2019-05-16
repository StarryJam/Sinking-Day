using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node{
    public Vector3 _worldPos;
    public int _girdX, _girdY;
    public Map_BaseCube mapCube;

    public NodeState state;

    public int gCost, hCost;

    public Node parentNode;

    public enum NodeState
    {
        narmal,
        unwalkable,
        inPath
    }

    public int fCost
    {
        get { return gCost + hCost; }
    }

    public Node(Vector3 position, int gridX,int gridY)
    {
        _worldPos = position;
        _girdX = gridX;
        _girdY = gridY;
        //UpdateState();
    }
    
    //public void UpdateState()
    //{
    //    if (_canWalk)
    //    {
    //        state = NodeState.narmal;
    //    }
    //    else
    //    {
    //        state = NodeState.unwalkable;
    //    }
    //}
}
