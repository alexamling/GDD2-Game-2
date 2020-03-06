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
    public float health = 10;
   


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

    public void OnHit(float damage, GameObject origin)
    {
        health -= damage;
        Debug.Log("hit");
    }
}
