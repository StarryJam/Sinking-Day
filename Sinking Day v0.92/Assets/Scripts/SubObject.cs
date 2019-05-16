using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class SubObject : MonoBehaviour, Selected
{
    private void Start()
    {
    }

    public void BeSelected()
    {
        transform.parent.GetComponent<Selected>().BeSelected();
        //else
        //{
        //    //GetComponentInParent<Selected>().BeSelected();
        //}
    }

    public void DeSelected()
    {
        //throw new System.NotImplementedException();
    }

    public GameObject FindFather()
    {
        if (transform.parent.GetComponent<SubObject>() != null)
        {
            return transform.parent.GetComponent<SubObject>().FindFather();
        }
        else
            return transform.parent.gameObject;
    }
    
}
