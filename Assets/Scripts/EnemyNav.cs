using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    // Start is called before the first frame update

    private NavMeshAgent nav;
    public List<Unit> units;
    public GameObject navBrainPrefab;
    private GameObject navBrainGameObject;
    private NavBrain navBrain;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        units = new List<Unit>();
        navBrainGameObject = Instantiate(navBrainPrefab, transform);
        navBrain = navBrainGameObject.GetComponent<NavBrain>();
    }

    public List<Unit> Units
    {
        get { return units; }
    }

    public void FindPath(Vector3 target)
    {

        navBrain.FindPath(target, units);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
