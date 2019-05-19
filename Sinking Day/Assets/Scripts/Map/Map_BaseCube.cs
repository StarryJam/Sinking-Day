using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_BaseCube : MonoBehaviour {

    public Node node;

    public GameObject cube;
    public Material material_Normal;
    public Material material_InPath;
    public Material material_Unwalkable;
    public Material material_InMoveRange;

    // Use this for initialization
    void Start () {
        material_Normal = (Material)Resources.Load("Prefebs/Map/normal");
        material_Unwalkable = (Material)Resources.Load("Prefebs/Map/unwalkable");
        material_InPath = (Material)Resources.Load("Prefebs/Map/inPath");
        material_InMoveRange = (Material)Resources.Load("Prefebs/Map/inRange");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
