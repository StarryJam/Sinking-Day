using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using cakeslice;

public class Enemy : MonoBehaviour,Selected {
    /*----------------------敌人信息---------------------*/
    public string enemyName;
    public float speed = 10;
    public float maxLife = 100;
    public int reward = 10;
    /*---------------------------------------------------*/
    private float life;
    public float slowRate = 0;
    private Transform[] checkPoint;
    private int index = 0;
    public GameObject dieEffect;
    public Canvas bloodBarModule;
    private Slider bloodBar;
    

	// Use this for initialization
	void Start () {
        checkPoint = CheckPoint.checkPoint;
        life = maxLife;
        bloodBarModule = Instantiate(bloodBarModule, transform.position + new Vector3(0, 3, 0), Camera.main.transform.rotation);
        bloodBar = bloodBarModule.GetComponent<Blood>().blood;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void LateUpdate()
    {
        Move();
        if (life <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        UpdateBloodBar();
    }

    public void SlowDown(float rate)
    {
        if(rate>slowRate)
            slowRate = rate;
    }

    public void UpdateBloodBar()
    {
        bloodBar.value = life / maxLife;
    }

    void Die()
    {
        GameObject effect = GameObject.Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
        Destroy(gameObject);
        Destroy(bloodBarModule.gameObject);
    }

    void Move()
    {
        if (index > checkPoint.Length - 1)
        {
            Destroy(this.gameObject);
            Die();
            return; //判断是否到达终点并销毁
        }
        transform.Translate((checkPoint[index].position - transform.position).normalized * Time.deltaTime * speed * (1 - slowRate/100));
        slowRate = 0;
        bloodBarModule.transform.position = transform.position + new Vector3(0, 3, 0);
        bloodBarModule.transform.rotation = Camera.main.transform.rotation;
        if (Vector3.Distance(transform.position, checkPoint[index].position) < 0.2f)
        {
            index++;
        }
    }

    private void OnDestroy()
    {
        EnemySpawner.aliveEnemy--;
        DeSelected();
    }

    public void ShowInformation()
    {
        List<string> information = UIManager.information;
        information.Add(enemyName);
        information.Add("Life: " + maxLife);
        information.Add("Speed: " + speed);
        information.Add("Reward: ￥" + reward);
        UIManager.ShowInformation();
    }

    public void BeSelected()
    {
        gameObject.AddComponent<cakeslice.Outline>();
        ShowInformation();
    }

    public void DeSelected()
    {
        Destroy(gameObject.GetComponent<cakeslice.Outline>());
        UIManager.HideInformation();
    }

    public void SetChildren()
    {
        throw new System.NotImplementedException();
    }
}
