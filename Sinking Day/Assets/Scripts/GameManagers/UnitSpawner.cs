using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour {


    [System.Serializable]
    public class CreateInfo
    {
        public int numOfTurn;
        public Unit unit;
        public Vector2 pos;
    }
    public static UnitSpawner spawner;
    public List<CreateInfo> infos;

    private void Awake()
    {
        spawner = this;
    }

    public void SpawnUnits()
    {
        foreach(var info in infos)
        {
            if (info.numOfTurn == StageManager.numOfTurn + 1)
            {
                Instantiate(info.unit, new Vector3(info.pos.x * 2, 0, info.pos.y * 2), Quaternion.identity);
            }
        }
    }

}
