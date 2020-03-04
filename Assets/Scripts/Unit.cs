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

public enum UnitState { Neutral, Moving}

public class Unit : MonoBehaviour, IDamageable
{

    UnitState unitState;
    private int speed;
    Rigidbody rb;
    NavBrain nb;

   

    // Start is called before the first frame update
    void Start()
    {
    
        speed = 3;
        unitState = UnitState.Neutral;
        rb = gameObject.GetComponent<Rigidbody>();
       
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
            //seeky bits
            Vector3 force = target - transform.position;
            force.Normalize();
            force *= speed;
            rb.AddForce(force);

        }
    }

    public void Attack(IDamageable target)
    {

    }

    public void OnHit(AttackData attackData)
    {
        throw new System.NotImplementedException();
    }
}
