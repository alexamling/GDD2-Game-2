using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavBrain : MonoBehaviour
{
    public NavMeshAgent nav;
    UnitState unitState;
    public List<Unit> childUnits;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        unitState = UnitState.Moving;
        childUnits = new List<Unit>();
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

        other.gameObject.GetComponent<Unit>().UnitState = UnitState.Attacking;
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<Unit>().UnitState = UnitState.Moving;
    }

    public List<Unit> ChildUnits
    {
        get
        {
            return childUnits;
        }

    }

   
    

}