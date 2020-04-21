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
    private NavBrain rootNavBrain;
    private Transform root;

    //number of elements in the tree per level
    private int treeSize = 3;
    private int treeLevel = 1;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        units = new List<Unit>();
        navBrainGameObject = Instantiate(navBrainPrefab, transform);
        rootNavBrain = navBrainGameObject.GetComponent<NavBrain>();
        units = new List<Unit>();
        root = navBrainGameObject.transform;

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
       
        rootNavBrain.FindPath(target, units);
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

    //find the number of child units in its control and its decendants control
    public int FindNumChildUnits(Transform t)
    {
        int sum = 0;
        NavBrain nb = t.gameObject.GetComponent<NavBrain>();
        sum += nb.ChildUnits.Count;
        for (int i = 0; i < t.childCount; i++)
        {
            sum += FindNumChildUnits(t.GetChild(i));
        }
        return sum;
    }

    public Transform FindMinNumChildUnits(Transform t)
    {
        //this is true if all available navBrains are full
        if((FindNumDecendants(navBrainGameObject.transform) - 1) * 4 == FindNumChildUnits(navBrainGameObject.transform))
        {
            return null;
        }

        int minNum = int.MaxValue;
        Transform minTransform = t;
        //checking the level below t in the tree
        for(int i = 0; i < t.childCount; i++)
        {
            if (minNum > FindNumChildUnits(t.GetChild(i)))
            {
                minTransform = t.GetChild(i);
                minNum = FindNumChildUnits(t.GetChild(i))
;           }
        }

        
        if(minNum < 4)
        {
            return minTransform;
        }

        //if all children at this level are full
        else
        {
            return FindMinNumChildUnits(minTransform);
        }

    }

    public void AddEnemy(Unit u)
    {
        if(FindMinNumChildUnits(root) == null)
        {
            float numNavBrains = Mathf.Pow((float)treeSize, treeLevel);
            for(int i = 0; i < numNavBrains; i++ )
            {
                CreateNavBrain(root);
            }
            treeLevel++;

        }
        NavBrain reciever = FindMinNumChildUnits(root).gameObject.GetComponent<NavBrain>();
        u.Nav = reciever;
        reciever.ChildUnits.Add(u);
    }

}
