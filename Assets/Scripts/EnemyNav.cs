using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    // Start is called before the first frame update

    private NavMeshAgent nav;
    public List<Unit> units;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        units = new List<Unit>();
    }

    public List<Unit> Units
    {
        get { return units; }
    }

    public void FindPath(Vector3 target)
    {
        nav.SetDestination(target);
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

   
}
