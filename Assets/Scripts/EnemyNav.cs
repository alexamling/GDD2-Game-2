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

    //number of elements in the tree per level
    private int treeSize = 3;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        units = new List<Unit>();
        navBrainGameObject = Instantiate(navBrainPrefab, transform);
        navBrain = navBrainGameObject.GetComponent<NavBrain>();
        units = new List<Unit>();

       //for(int i =0; i < 20; i++)
       //{
       //    CreateNavBrain(navBrainGameObject.transform);
       //}
        
    }

    public GameObject NavBrainGameObject
    {
        get { return navBrainGameObject; }
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

    public void CreateNavBrain(Transform t)
    {
        //if (t.childCount < 3)
        //{
        //    Instantiate(navBrainPrefab, t);
        //}
        //
        ////if not last navBrain in the hierarchy level, go to the "right"
        //else if(t.GetSiblingIndex() < t.parent.childCount - 1)
        //{
        //    CreateNavBrain(t.parent.GetChild(t.GetSiblingIndex() + 1));
        //}
        //
        //else
        //{
        //    CreateNavBrain(t.GetChild(0));
        //}

        Transform minDec = FindMinDecendents(t);
        if(FindNumDecendants(t) > treeSize)
        {
            CreateNavBrain(minDec);
        }
        else 
        {
            Instantiate(navBrainPrefab, t);
        }

    }

    //due to recursive nature also includes self in count
    public int FindNumDecendants(Transform t)
    {
        int sum = 0;
        sum++;
        for(int i = 0; i < t.childCount; i++)
        {
           sum += FindNumDecendants(t.GetChild(i));
        }
        return sum;
    }

    //Of the children of supplied transform, which has the least 
    public Transform FindMinDecendents(Transform t)
    {
       
        int minNum = int.MaxValue;
        Transform minTransform = t;
        for (int i = 0; i < t.childCount; i++)
        {
            
           if(minNum > FindNumDecendants(t.GetChild(i)))
            {
                
                minNum = FindNumDecendants(t.GetChild(i));
                minTransform = t.GetChild(i);
            }
        }

        return minTransform;
    }
}
