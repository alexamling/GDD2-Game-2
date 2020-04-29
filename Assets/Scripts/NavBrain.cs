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



    public void FindPath(Vector3 target)
    {

        nav.SetDestination(target);
       

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

    public void Pyramid(Vector3 dest, float radians)
    {

        float pyramidScale = 10.0f;
        for (int i = 0; i < transform.childCount; i++)
        {

            float angle = (Mathf.PI / 6) * (i + 1) + radians;
            Debug.Log(angle);
            Vector3 childDest = new Vector3(dest.x + Mathf.Cos(angle) * pyramidScale, transform.position.y, dest.z + Mathf.Sin(angle) * pyramidScale);
            transform.GetChild(i).gameObject.GetComponent<NavBrain>().Pyramid(childDest, radians);
        }

        FindPath(dest);
        Debug.Log(dest);
    }

   
    

}