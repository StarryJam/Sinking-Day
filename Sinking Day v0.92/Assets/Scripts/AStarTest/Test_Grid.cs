using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Grid : MonoBehaviour {
    private Test_Node[,] gridNodes;
    public Vector2 gridSize;
    public float nodeRadius;
    private float nodedDiameter;

    public List<Test_Node> path;

    public LayerMask onLayer;
    public Transform player;
    public Transform endPoint;

    public int gridCntX, gridCntY;


    // Use this for initialization
    void Start()
    {
        nodedDiameter = nodeRadius * 2;
        gridCntX = Mathf.RoundToInt(gridSize.x / nodedDiameter);
        gridCntY = Mathf.RoundToInt(gridSize.y / nodedDiameter);
        gridNodes = new Test_Node[gridCntX, gridCntY];
        CreateGrid();
    }
        // Update is called once per frame
    void Update () {
        FindingPath(player.position, endPoint.position);
        Debug.Log(player.position);
        Debug.Log(endPoint.position);
    }

    private void CreateGrid()
    {
        Vector3 startPoint = transform.position - gridSize.x / 2 * Vector3.right - gridSize.y / 2 * Vector3.forward;
        for(int i = 0; i < gridCntX; i++)
        {
            for (int j = 0; j < gridCntY; j++)
            {
                Vector3 worldPoint = startPoint + Vector3.right * (i * nodedDiameter + nodeRadius) + Vector3.forward * (j * nodedDiameter + nodeRadius);
                bool walkable = !Physics.CheckSphere(worldPoint, nodeRadius, onLayer);
                gridNodes[i, j] = new Test_Node(walkable, worldPoint, i, j);
            }
        }
    }

    void FindingPath(Vector3 startPoint, Vector3 endPoint)
    {
        Test_Node startNode = GetNodeFromPosition(startPoint);
        Test_Node endNode = GetNodeFromPosition(endPoint);

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

            foreach (var node in GetNeiborNodes(currentNode))
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
        path = new List<Test_Node>();
        Test_Node temp = endNode;
        while (temp != startNode)
        {
            path.Add(temp);
            temp = temp.parent;
        }
        path.Reverse();
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

    private void OnDrawGizmos() //绘制网格
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1, gridSize.y));
        if (gridNodes == null) return;
        Test_Node playerNode = GetNodeFromPosition(player.position);
        foreach(var node in gridNodes)
        {
            Gizmos.color = node._canWalk ? Color.white : Color.red;
            Gizmos.DrawCube(node._worldPos, Vector3.one * (nodedDiameter - 0.1f));
        }

        if (path != null)
        {
            foreach(var node in path)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(node._worldPos, Vector3.one * (nodedDiameter - 0.1f));
            }
        }

        if (playerNode != null && playerNode._canWalk)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(playerNode._worldPos, Vector3.one * (nodedDiameter - 0.1f));
        }
    }



    public Test_Node GetNodeFromPosition(Vector3 position)
    {
        Vector3 playerPosition = position - transform.position;
        float percentX = (playerPosition.x + gridSize.x / 2) / gridSize.x;
        float percentY = (playerPosition.z + gridSize.y / 2) / gridSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridCntX - 1) * percentX);
        int y = Mathf.RoundToInt((gridCntY - 1) * percentY);

        return gridNodes[x, y];
    }

    public List<Test_Node> GetNeiborNodes(Test_Node node)
    {
        List<Test_Node> neiberNodes = new List<Test_Node>();
        for(int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;
                int tempX = node._girdX + i;
                int tempY = node._girdY + j;
                if (tempX < gridCntX && tempX >= 0 && tempY >= 0 && tempY < gridCntY)
                {
                    neiberNodes.Add(gridNodes[tempX, tempY]);
                }
            }
        }
        return neiberNodes;
    }
}
