using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class SetChildren : MonoBehaviour {

	// Use this for initialization
	void Start () {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            child.gameObject.AddComponent<SubObject>();
            child.tag = tag;
            if (child.childCount > 0)
            {
                child.gameObject.AddComponent<SetChildren>();
            }
            else
            {
                if (child.gameObject.GetComponent<MeshCollider>() == null)
                {
                    child.gameObject.AddComponent<MeshCollider>();
                    child.gameObject.GetComponent<MeshCollider>().convex = true;
                    child.gameObject.GetComponent<MeshCollider>().isTrigger = false;
                }
            }
        }
    }

    public void SetOutlineOnChildren()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            child.gameObject.AddComponent<Outline>();
            if (child.childCount > 0 && child.GetComponent<SetChildren>() != null)
            {
                child.gameObject.GetComponent<SetChildren>().SetOutlineOnChildren();
            }
        }
    }

    public void DestroyOutlineOnChildren()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            Destroy(child.GetComponent<Outline>());
            if (child.childCount > 0 && child.GetComponent<SetChildren>() != null) 
            {
                child.gameObject.GetComponent<SetChildren>().DestroyOutlineOnChildren();
            }
        }
    }
}
