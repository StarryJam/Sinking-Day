using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitStateUI : MonoBehaviour {

    public Unit unit;
    public Image healthBar_Current;
    private Vector3 originPos;

    private void Start()
    {
        originPos = healthBar_Current.transform.localPosition;
    }

    private void Update()
    {
        UpdateHealth(unit.maxHealth,unit.currentHealth);
    }

    public void UpdateHealth(float maxHealth, float currentHealth)
    {
        float width = healthBar_Current.rectTransform.rect.width;
        float healthPercent = currentHealth / maxHealth;
        healthBar_Current.transform.localPosition = originPos + (1 - healthPercent) * new Vector3(-width, 0, 0);
    }
}
