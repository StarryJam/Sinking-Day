using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFX1_Target : MonoBehaviour
{
    public GameObject Target;

    private GameObject currentTarget;
    RFX1_TransformMotion transformMotion;

    //自定义部分
    private float damage = 10;
    private GameObject target;


    // Use this for initialization
    void Start ()
	{
	    transformMotion = GetComponentInChildren<RFX1_TransformMotion>();
      UpdateTarget();
        var tm = GetComponentInChildren<RFX1_TransformMotion>(true);
        if (tm != null) tm.CollisionEnter += Tm_CollisionEnter;
    }


    private void Tm_CollisionEnter(object sender, RFX1_TransformMotion.RFX1_CollisionInfo e)
    {
        e.Hit.transform.gameObject.GetComponent<Unit>().takeDamage(damage);
        Destroy(gameObject, 5);
    }

    public void SetProjectile(GameObject _target, float _damage)
    {
        damage = _damage;
        target = _target;
    }


    void Update()
    {
        UpdateTarget();
    }
	
	// Update is called once per frame
	void UpdateTarget ()
	{
	    if (Target == null)
	    {
            //Debug.Log("You must set the target!");
	        return;
	    }
	    if (transformMotion == null)
	    {
	        Debug.Log("You must attach the target script on projectile effect!");
	        return;
	    }
	    if (Target != currentTarget)
	    {
	        currentTarget = Target;
	        transformMotion.Target = currentTarget;
	    }
	}
}
