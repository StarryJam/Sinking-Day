using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowField : MonoBehaviour {

    public float slowRate = 90;
    public List<GameObject> enemyList;
    public ParticleSystem effect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (enemyList != null)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].GetComponent<Enemy>().SlowDown(slowRate);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyList.Remove(other.gameObject);
        }
    }

    private void OnDestroy()
    {
        ParticleSystem effect = Instantiate(this.effect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }
}
