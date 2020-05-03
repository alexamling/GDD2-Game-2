using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Init(Vector3 target)
    {
        direction = (target - transform.position);
        direction.y = 0;
        direction = Vector3.Normalize(direction);
        Destroy(gameObject, 1);
    }
}
