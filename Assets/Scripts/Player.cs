using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public ProjectileData[] projectilePresets;

    private Transform inactiveProjectiles;
    private Transform activeProjectiles;

    // Start is called before the first frame update
    void Start()
    {
        inactiveProjectiles = new GameObject().transform;
        activeProjectiles = new GameObject().transform;
        inactiveProjectiles.parent = transform;
        activeProjectiles.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FireProjectile(ProjectileData type)
    {

    }
}
