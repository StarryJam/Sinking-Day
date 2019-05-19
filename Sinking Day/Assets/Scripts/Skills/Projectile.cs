using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public GameObject target;
    public float damage;
    public float speed;
    public bool haveTarget = false;
    public ProjectileDieFVX dieFVX;

    public void SetProjectile(GameObject _target, float _speed, float _damage)
    {
        target = _target;
        damage = _damage;
        speed = _speed;
        haveTarget = true;
    }

    Projectile(float _speed)
    {
        speed = _speed;
    }

    private void Update()
    {
        if (haveTarget)
        {
            if (target != null)
            {
                transform.LookAt(target.transform);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (haveTarget)
        {
            if (other.gameObject == target)
            {
                target.GetComponent<Unit>().takeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        if (dieFVX != null)
            Instantiate(dieFVX, transform.position, Quaternion.identity);
    }

}
