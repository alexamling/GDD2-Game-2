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

public enum UnitState { Neutral, Moving, Attacking, Braking}

public class Unit : MonoBehaviour, IDamageable
{

    UnitState unitState;
    private float turnSpeed;
    public Rigidbody rb;
    NavBrain nb;
    public float health = 10;
    private float speedLimit;
    private float fireRate = 2;
    private float counter;
    public NavBrain nav;
   


    // Start is called before the first frame update
    void Start()
    {
    
        turnSpeed = 50f;
        unitState = UnitState.Neutral;
        rb = gameObject.GetComponent<Rigidbody>();
        speedLimit = 10;

       
    }

    public NavBrain Nav
    {
        get { return nav; }
        set { nav = value; }
    }

    

    // Update is called once per frame
    void Update()
    {
        //if (health <= 0) Destroy(gameObject);
        switch(unitState)
        {
            case UnitState.Moving:
                break;
            case UnitState.Attacking:
                if(counter > fireRate)
                {
                   
                }
                break;
            case UnitState.Braking:
                if(rb.velocity.magnitude < .1f)
                {
                    unitState = UnitState.Attacking;
                }
                Brake();
                break;
        }
        counter += Time.deltaTime;
        //Debug.Log(unitState);
    }

    public void PathTo(Vector3 target)
    {
        // maybe more logic in the future
        if (gameObject.activeInHierarchy)
        {
            //seeky bits
            Vector3 force = target - transform.position;
            force.Normalize();
            force *= turnSpeed;
            rb.AddForce(force);

            //slow down there buster brown(clamps unit velocity)
            if(rb.velocity.magnitude > speedLimit)
            {
                Vector3 newVelocity = rb.velocity;
                newVelocity.Normalize();
                rb.velocity = newVelocity * speedLimit;

            }

        }
    }

    public void Attack(IDamageable target)
    {

    }

    public void OnHit(float damage, GameObject origin)
    {
        health -= damage;
        //Debug.Log("hit");
    }

    public UnitState UnitState
    {
        get { return unitState; }
        set { unitState = value; }
    }


    public void Brake()
    {
        rb.velocity *= .93f;
    }

    public void MoveUnits(int level)
    {

    }
}
