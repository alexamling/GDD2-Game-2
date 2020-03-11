using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavBrain : MonoBehaviour
{
   public NavMeshAgent nav;
    UnitState unitState;
    List<Unit> childUnits;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        unitState = UnitState.Moving;
    }

    // Update is called once per frame
    void Update()
    {
        if(unitState == UnitState.Moving && childUnits != null)
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
        Debug.Log("bruh");
        other.gameObject.GetComponent<Unit>().UnitState = UnitState.Attacking;
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<Unit>().UnitState = UnitState.Moving;
    }
}
