using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour {
    public int money = 200;
    public int life = 10;
    

    public void MoneyChange(int value=0)
    {
        money += value;
    }

    private void Start()
    {
        MoneyChange();
    }

}
