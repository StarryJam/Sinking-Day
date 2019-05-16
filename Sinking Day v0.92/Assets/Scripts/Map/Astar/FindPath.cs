using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPath : MonoBehaviour {
    public Transform startPoint, endPoint;
    private Grid _grid;
	// Use this for initialization
	void Start () {
        _grid = GetComponent<Grid>();
	}
	
	// Update is called once per frame
	void Update () {
        FindingPath(startPoint.position, endPoint.position);
	}

    void FindingPath(Vector3 startPoint,Vector3 endPoint)
    {
        Node startNode = _grid.GetNodeFromPosition(startPoint);
        Node endNode = _grid.GetNodeFromPosition(endPoint);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closeSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            for(int i=0; i < openSet.Count; i++)
            {
                if(openSet[i].fCost<currentNode.fCost||(openSet[i].fCost==currentNode.fCost&& openSet[i].hCost == currentNode.hCost))
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

            foreach(var node in _grid.GetNeiborNodes(currentNode))
            {
                if (node.state!=Node.NodeState.unwalkable || closeSet.Contains(node)) continue;
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

    public void GeneratePath(Node startNode,Node endNode)
    {
        List<Node> path = new List<Node>();
        Node temp = endNode;
        while (temp != startNode)
        {
            path.Add(temp);
            temp = temp.parent;
        }
        path.Reverse();
        _grid.path = path;
    }

    int GetNodedsDistance(Node a, Node b)
    {
        int cntX = Mathf.Abs(a._girdX - b._girdX);
        int cntY = Mathf.Abs(a._girdY - b._girdY);
        if (cntX > cntY)
            return 14 * cntY + 10 * (cntX - cntY);
        else
            return 14 * cntX + 10 * (cntY - cntX);
    }
}
