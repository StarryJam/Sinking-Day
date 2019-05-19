using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_FindPath : MonoBehaviour
{
    public Transform player, endPoint;
    private Test_Grid _grid;
    // Use this for initialization
    void Start()
    {
        _grid = GetComponent<Test_Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        FindingPath(player.position, endPoint.position);
    }

    void FindingPath(Vector3 startPoint, Vector3 endPoint)
    {
        Test_Node startNode = _grid.GetNodeFromPosition(startPoint);
        Test_Node endNode = _grid.GetNodeFromPosition(endPoint);

        List<Test_Node> openSet = new List<Test_Node>();
        HashSet<Test_Node> closeSet = new HashSet<Test_Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Test_Node currentNode = openSet[0];

            for (int i = 0; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost == currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closeSet.Add(currentNode);

            if (currentNode == endNode)
            {
                GeneratePath(startNode, endNode);
                return;
            }

            foreach (var node in _grid.GetNeiborNodes(currentNode))
            {
                if (!node._canWalk || closeSet.Contains(node)) continue;
                int newCost = currentNode.gCost + GetNodedsDistance(currentNode, node);
                if (newCost < node.gCost || !openSet.Contains(node))
                {
                    node.gCost = newCost;
                    node.hCost = GetNodedsDistance(node, endNode);
                    node.parent = currentNode;
                    if (!openSet.Contains(node))
                    {
                        openSet.Add(node);
                    }
                }
            }
        }
    }

    public void GeneratePath(Test_Node startNode, Test_Node endNode)
    {
        List<Test_Node> path = new List<Test_Node>();
        Test_Node temp = endNode;
        while (temp != startNode)
        {
            path.Add(temp);
            temp = temp.parent;
        }
        path.Reverse();
        _grid.path = path;
    }

    int GetNodedsDistance(Test_Node a, Test_Node b)
    {
        int cntX = Mathf.Abs(a._girdX - b._girdX);
        int cntY = Mathf.Abs(a._girdY - b._girdY);
        if (cntX > cntY)
            return 14 * cntY + 10 * (cntX - cntY);
        else
            return 14 * cntX + 10 * (cntY - cntX);
    }
}
