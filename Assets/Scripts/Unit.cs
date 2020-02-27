using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IDamageable
{
    void OnHit(AttackData attackData);
}

public interface ISelectable
{
    void Select();
}

public struct AttackData
{
    GameObject origin;
    float damage;
}

public class Unit : MonoBehaviour, IDamageable
{
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
       nav.destination = target;
    }

    public void Attack(IDamageable target)
    {

    }

    public void OnHit(AttackData attackData)
    {
        throw new System.NotImplementedException();
    }
}
