using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltProjectile : Projectile
{
    public GameObject[] particles;
    public AnimationCurve scaleCurve;

    public override void Init(Vector3 target)
    {
        //transform.LookAt(target);
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].GetComponent<LineRenderer>().SetPosition(1, target);
        }
    }

    // Update is called once per frame
    void Update()
    {
        particles[Random.Range(0, particles.Length)].SetActive(true);
        particles[Random.Range(0, particles.Length)].SetActive(false);
        for (int i = 0; i<particles.Length; i++)
        {
            particles[i].GetComponent<LineRenderer>().widthMultiplier =scaleCurve.Evaluate((Time.time - startTime) / lifetime);
        }
    }
}
