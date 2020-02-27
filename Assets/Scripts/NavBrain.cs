using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavBrain : MonoBehaviour
{
    private NavMeshAgent nav;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindPath(Vector3 target)
    {
        nav.SetDestination(target);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    }
}
