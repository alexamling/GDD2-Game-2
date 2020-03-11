using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ProjectileData
{
    public float damage;
    public float range;
    public float speed;
    public float radius;
}

public class Projectile : MonoBehaviour
{
    private Vector3 direction;
    public ProjectileData data;

    public void Init(Vector3 target)
    {
        direction = (target-transform.position);
        direction.y = 0;
        direction = Vector3.Normalize(direction);
        Destroy(gameObject, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * data.speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable hitObj = other.gameObject.GetComponent<IDamageable>();
        if (hitObj != null)
        {
            hitObj.OnHit(data.damage, gameObject);
        }
    }
}
