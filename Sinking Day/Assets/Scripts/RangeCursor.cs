using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCursor : MonoBehaviour {
    
    public Canvas rangeCursorUI;
    public List<GameObject> unitsInRange;

    private void Start()
    {
        rangeCursorUI = UIManager.CreateUI(UIManager.ui_RangeCursorOnUnit, transform);
        rangeCursorUI.gameObject.SetActive(false);
    }

    public void CheckRange(int range)
    {
        int rangeValue = range * 2 + 1;
        GetComponent<CapsuleCollider>().radius= rangeValue;
        rangeCursorUI.gameObject.SetActive(true);
        rangeCursorUI.transform.localScale = new Vector3(rangeValue, rangeValue, rangeValue);
    }

    public void Cancel()
    {
        GetComponent<CapsuleCollider>().radius = 0;
        rangeCursorUI.gameObject.SetActive(false);
        unitsInRange.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Unit>() != null)
        {
            unitsInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Unit>())
        {
            unitsInRange.Remove(other.gameObject);
        }
    }
}
