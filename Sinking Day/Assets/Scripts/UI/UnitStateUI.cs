﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitStateUI : MonoBehaviour {

    public Unit unit;
    public Image healthBar_Current;
    private Vector3 originPos;
    public GameObject actionPoints;
    public static Sprite actionPointImg;
    public static Sprite usingActionPointImg;

    private void Start()
    {
        originPos = healthBar_Current.transform.localPosition;
    }

    private void Update()
    {
        UpdateHealth();
        UpdateActionPoint();
    }

    public void UpdateHealth()
    {
        float width = healthBar_Current.rectTransform.rect.width;
        float healthPercent = unit.currentHealth / unit.maxHealth;
        healthBar_Current.transform.localPosition = originPos + (1 - healthPercent) * new Vector3(-width, 0, 0);
    }

    public void UpdateActionPoint()
    {
        for (int i = 0; i < actionPoints.transform.childCount; i++)
        {
            actionPoints.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < unit.currentActionPoint; i++)
        {
            actionPoints.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void UsingActionPoint(int point)
    {
        for (int i = 0; i < actionPoints.transform.childCount; i++)
        {
            actionPoints.transform.GetChild(i).GetComponent<Image>().sprite = actionPointImg;
        }
        for (int i = 0; i < point; i++)
        {
            actionPoints.transform.GetChild(unit.currentActionPoint - 1 - i).GetComponent<Image>().sprite = usingActionPointImg;
        }
    }
}
