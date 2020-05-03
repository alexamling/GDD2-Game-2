using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float damage;
    public float range;
    public float speed;
    public float radius;
    public float lifetime;
    protected float startTime;

    public abstract void Init(Vector3 target);

    public void Start()
    {
        startTime = Time.time;
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed;
    }

    protected void OnTriggerEnter(Collider other)
    {
        IDamageable hitObj = other.gameObject.GetComponent<IDamageable>();
        if (hitObj != null)
        {
            hitObj.OnHit(damage, gameObject);
        }
    }
}
