using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class NavBrain : MonoBehaviour
{
    public NavMeshAgent nav;
    UnitState unitState;
    public List<Unit> childUnits;
    public bool available;
    private float timeSinceUnavailable;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        unitState = UnitState.Moving;
        childUnits = new List<Unit>();
        available = true;
    }

    public bool Available
    {
        get { return available; }
        set 
        { if (value)
            {
                timeSinceUnavailable = 0;
            }
            available = value; 
        }
    }

    public float TimeSinceUnavailable
    {
        get { return timeSinceUnavailable; }
    }
        

    // Update is called once per frame
    void Update()
    {
        if (unitState == UnitState.Moving && childUnits != null)
        {


            foreach (Unit u in childUnits)
            {

                u.PathTo(transform.position);

            }
        }

        if(!available)
        {
            timeSinceUnavailable += Time.deltaTime;
        }
    }



    public void FindPath(Vector3 target, List<Unit> units)
    {

        nav.SetDestination(target);
        childUnits = units;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.GetComponent<Unit>() != null)
        {

            other.gameObject.GetComponent<Unit>().UnitState = UnitState.Braking;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Unit>() != null)
        {

            other.gameObject.GetComponent<Unit>().UnitState = UnitState.Moving;
        }
    }

    public List<Unit> ChildUnits
    {
        get
        {
            return childUnits;
        }

    }

   
    

}