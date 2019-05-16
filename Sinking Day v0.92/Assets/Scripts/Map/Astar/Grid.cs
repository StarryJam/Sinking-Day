using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    private bool isShowPath = false;

    public static Grid map;

    private Node[,] gridNodes;
    public Vector2 gridSize;
    public float nodeRadius;
    private float nodedDiameter;

    public List<Node> path;

    public LayerMask onLayer;
    //public Transform player;

    public Transform startPoint;
    public Transform endPoint;

    public int gridCntX, gridCntY;

    public Map_BaseCube BaseCube;


    // Use this for initialization

    private void Awake()
    {
        map = this;
        path = new List<Node>();
        nodedDiameter = nodeRadius * 2;
        gridCntX = Mathf.RoundToInt(gridSize.x / nodedDiameter);
        gridCntY = Mathf.RoundToInt(gridSize.y / nodedDiameter);
        gridNodes = new Node[gridCntX, gridCntY];
        CreateGrid();
    }

    void Start()
    {
    }
        // Update is called once per frame
    void Update () {
        //FindPath(startPoint.position, endPoint.position);
        UpdateMap();
    }

    private void CreateGrid() //创建地图
    {
        Vector3 startPoint = transform.position - gridSize.x / 2 * Vector3.right - gridSize.y / 2 * Vector3.forward;
        for(int i = 0; i < gridCntX; i++)
        {
            for (int j = 0; j < gridCntY; j++)
            {
                Vector3 worldPoint = startPoint + Vector3.right * (i * nodedDiameter + nodeRadius) + Vector3.forward * (j * nodedDiameter + nodeRadius);
                gridNodes[i, j] = new Node(worldPoint, i, j);
            }
        }
        foreach(var node in gridNodes) //生产地基块
        {
            Map_BaseCube tempCube = ((GameObject)Instantiate(Resources.Load("Prefebs/Map/Map_BaseCube"), node._worldPos, Quaternion.identity)).GetComponent<Map_BaseCube>();
            node.mapCube = tempCube;
            node.mapCube.transform.parent = transform;
        }
        UpdateMap();
    }

    private void UpdateMap()
    {
        foreach (var node in gridNodes)
        {
            bool walkable = !Physics.CheckSphere(node._worldPos, nodeRadius, onLayer);
            if (!walkable)
            {
                node.state = Node.NodeState.unwalkable;
                node.mapCube.cube.GetComponent<MeshRenderer>().material = node.mapCube.marterial_Unwalkable;
            }
            else
            {
                if (path.Contains(node) && isShowPath)
                {
                    node.state = Node.NodeState.inPath;
                    if (isShowPath)
                        node.mapCube.cube.GetComponent<MeshRenderer>().material = node.mapCube.marterial_InPath;
                    else
                        node.mapCube.cube.GetComponent<MeshRenderer>().material = node.mapCube.marterial_Normal;
                }
                else
                {
                    node.state = Node.NodeState.narmal;
                    node.mapCube.cube.GetComponent<MeshRenderer>().material = node.mapCube.marterial_Normal;
                }
            }

        }
    }

    public void FindPath(Vector3 startPoint, Vector3 endPoint) //寻路算法
    {
        Node startNode = GetNodeFromPosition(startPoint);
        Node endNode = GetNodeFromPosition(endPoint);


        List<Node> openSet = new List<Node>();
        HashSet<Node> closeSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

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
                if (node.state==Node.NodeState.unwalkable || closeSet.Contains(node)) continue;
                int newCost = currentNode.gCost + GetNodedsDistance(currentNode, node);
                if (newCost < node.gCost || !openSet.Contains(node))
                {
                    node.gCost = newCost;
                    node.hCost = GetNodedsDistance(node, endNode);
                    node.parentNode = currentNode;
                    if (!openSet.Contains(node))
                    {
                        openSet.Add(node);
                    }
                }
            }
        }
    }

    public void DisplayPath()
    {
        isShowPath = true;
    }

    public void UndisplayPath()
    {
        isShowPath = false;
    }

    public void ClearPath()
    {
        isShowPath = false;
        path.Clear();
    }

    private void GeneratePath(Node startNode, Node endNode)
    {
        path = new List<Node>();
        Node temp = endNode;
        while (temp != startNode)
        {
            path.Add(temp);
            temp = temp.parentNode;
        }
        path.Reverse();
    }

    private int GetNodedsDistance(Node a, Node b)
    {
        int cntX = Mathf.Abs(a._girdX - b._girdX);
        int cntY = Mathf.Abs(a._girdY - b._girdY);
        if (cntX > cntY)
            return 14 * cntY + 10 * (cntX - cntY);
        else
            return 14 * cntX + 10 * (cntY - cntX);
    }

    private Node GetNodeFromPosition(Vector3 position)
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

    private List<Node> GetNeiborNodes(Node node)
    {
        List<Node> neiberNodes = new List<Node>();
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
