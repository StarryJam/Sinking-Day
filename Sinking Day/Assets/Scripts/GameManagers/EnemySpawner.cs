using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public static int aliveEnemy;
    public Wave[] waves;
    public Transform START;
    public float waveRate = 1;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnEnemy());
	}
	
	

    IEnumerator SpawnEnemy()
    {
        foreach(Wave wave in waves)
        {
            for(int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemy, START.position, Quaternion.identity);
                aliveEnemy++;
                yield return new WaitForSeconds(wave.rate);
            }
            while (aliveEnemy > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
    }

    [System.Serializable]
    public class Wave
    {
        public GameObject enemy;
        public int count;
        public float rate;
    }
}
