using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IDamageable
{
    void OnHit(float damage, GameObject origin);
}

public interface ISelectable
{
    void Select();
}

public class Unit : MonoBehaviour, IDamageable
{
    public float health = 10;
    private NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PathTo(Vector3 target)
    {
        // maybe more logic in the future
        if (gameObject.activeInHierarchy)
        {
            nav.destination = target;
        }
    }

    public void Attack(IDamageable target)
    {

    }

    public void OnHit(float damage, GameObject origin)
    {
        health -= damage;
        Debug.Log("hit");
    }
}
