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
    public ParticleSystem effect;
    public TrailRenderer trail;
}

public class Projectile : MonoBehaviour
{
    private Vector3 direction;

    Projectile(Vector3 dir, ProjectileData data)
    {
        direction = dir;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
