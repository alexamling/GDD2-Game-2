using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-320.01 - Game Design and Development 2
/// 
/// Project 2
/// 
/// Class Description   : Detects collisions between two objects, vectors and other derivatives
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 8, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : March 26, 2020
/// Filename            : CollisionDetection.cs
/// 
/// </summary>

public class CollisionDetection : MonoBehaviour {

    #region VARIABLES
    private Vector3 tempVector;

    private Bounds obj1;
    private Bounds obj2;

    private bool returnValue;
    #endregion

    #region AABB COLLISION
    /// <summary>
    /// AABB Collision detection algorithm
    /// </summary>
    /// <param name="object1">First object to test for collision</param>
    /// <param name="object2">Second object to test for collision</param>
    /// <returns>True or False</returns>
    public bool AABBCollision(GameObject object1, GameObject object2)
    {
        this.returnValue = false;
        
        obj1 = GetBounds(object1);
        obj2 = GetBounds(object2);

        if ((obj2.min.x < obj1.max.x) &&
            (obj2.max.x > obj1.min.x) &&
            (obj2.max.z > obj1.min.z) &&
            (obj2.min.z < obj1.max.z))
        {
            returnValue = true;
        }

        return returnValue;
    }

    /// <summary>
    /// AABB Collision detection algorithm
    /// </summary>
    /// <param name="object1">First object to test for collision</param>
    /// <param name="object2">Second object to test for collision</param>
    /// <param name="obj1Mesh">MeshRenderer of the first object</param>
    /// <param name="obj2Mesh">MeshRenderer of the second object</param>
    /// <returns>True or False</returns>
    public bool AABBCollision(GameObject object1, MeshRenderer obj1Mesh, GameObject object2, MeshRenderer obj2Mesh)
    {
        this.returnValue = false;

        obj1 = new Bounds(object1.transform.position, obj1Mesh.bounds.size);
        obj2 = new Bounds(object2.transform.position, obj2Mesh.bounds.size);

        if ((obj2.min.x < obj1.max.x) &&
            (obj2.max.x > obj1.min.x) &&
            (obj2.max.z > obj1.min.z) &&
            (obj2.min.z < obj1.max.z))
        {
            returnValue = true;
        }

        return returnValue;
    }

    /// <summary>
    /// AABB Collision detection algorithm
    /// </summary>
    /// <param name="object1">First object to test for collision</param>
    /// <param name="object2">Second object to test for collision</param>
    /// <returns>True or False</returns>
    public bool AABBCollision(Vector3 positionObj1, MeshRenderer obj1Mesh, Vector3 positionObj2, MeshRenderer obj2Mesh)
    {
        this.returnValue = false;

        obj1 = new Bounds(positionObj1, obj1Mesh.bounds.size);
        obj2 = new Bounds(positionObj2, obj2Mesh.bounds.size);

        if ((obj2.min.x < obj1.max.x) &&
            (obj2.max.x > obj1.min.x) &&
            (obj2.max.z > obj1.min.z) &&
            (obj2.min.z < obj1.max.z))
        {
            returnValue = true;
        }

        return returnValue;
    }
    #endregion

    #region CIRCLE COLLISION
    /// <summary>
    /// Circle Collision detection algorithm
    /// </summary>
    /// <param name="object1">First object to test for collision</param>
    /// <param name="object2">Second object to test for collision</param>
    /// <returns>True or False</returns>
    public bool CircleCollision(GameObject object1, GameObject object2)
    {
        this.returnValue = false;

        obj1 = GetBounds(object1);
        obj2 = GetBounds(object2);

        float obj1RadiusSq = (Mathf.Pow((obj1.max.x - obj1.center.x), 2) + Mathf.Pow((obj1.max.z - obj1.center.z), 2));
        float obj2RadiusSq = (Mathf.Pow((obj2.max.x - obj2.center.x), 2) + Mathf.Pow((obj2.max.z - obj2.center.z), 2));

        float distanceBetweenObjects = (Mathf.Pow((obj1.center.x - obj2.center.x), 2) + (Mathf.Pow((obj1.center.z - obj2.center.z), 2)));

        float sumOfRadiiSq = obj1RadiusSq + obj2RadiusSq;

        if (distanceBetweenObjects < sumOfRadiiSq)
        {
            returnValue = true;
        }            

        return returnValue;
    }

    /// <summary>
    /// Circle Collision detection algorithm
    /// </summary>
    /// <param name="object1">First object to test for collision</param>
    /// <param name="increaseObj1BoundsBy">Amount by which we want to increase the bounds of object 1</param>
    /// <param name="object2">Second object to test for collision</param>
    /// <param name="increaseObj2BoundsBy">Amount by which we want to increase the bounds of object 2</param>
    /// <returns>True or false</returns>
    public bool CircleCollision(GameObject object1, float increaseObj1BoundsBy, GameObject object2, float increaseObj2BoundsBy)
    {
        this.returnValue = false;

        this.tempVector = new Vector3(increaseObj1BoundsBy, increaseObj1BoundsBy, increaseObj1BoundsBy);

        obj1 = GetBounds(object1);
        obj1.Expand(this.tempVector);

        this.tempVector = new Vector3(increaseObj2BoundsBy, increaseObj2BoundsBy, increaseObj2BoundsBy);
        obj2.Expand(this.tempVector);
        obj2 = GetBounds(object2);

        float obj1RadiusSq = (Mathf.Pow((obj1.max.x - obj1.center.x), 2) + Mathf.Pow((obj1.max.z - obj1.center.z), 2));
        float obj2RadiusSq = (Mathf.Pow((obj2.max.x - obj2.center.x), 2) + Mathf.Pow((obj2.max.z - obj2.center.z), 2));

        float distanceBetweenObjects = (Mathf.Pow((obj1.center.x - obj2.center.x), 2) + (Mathf.Pow((obj1.center.z - obj2.center.z), 2)));

        float sumOfRadiiSq = obj1RadiusSq + obj2RadiusSq;

        if (distanceBetweenObjects < sumOfRadiiSq)
        {
            returnValue = true;
        }

        return returnValue;
    }

    /// <summary>
    /// Circle Collision detection algorithm
    /// </summary>
    /// <param name="object1">First object to test for collision</param>
    /// <param name="object2">Second object to test for collision</param>
    /// <returns>True or False</returns>
    public bool CircleCollision(GameObject object1, MeshRenderer obj1Mesh, GameObject object2, MeshRenderer obj2Mesh)
    {
        this.returnValue = false;

        obj1 = new Bounds(object1.transform.position, obj1Mesh.bounds.size);
        obj2 = new Bounds(object2.transform.position, obj2Mesh.bounds.size);

        float obj1RadiusSq = (Mathf.Pow((obj1.max.x - obj1.center.x), 2) + Mathf.Pow((obj1.max.z - obj1.center.z), 2));
        float obj2RadiusSq = (Mathf.Pow((obj2.max.x - obj2.center.x), 2) + Mathf.Pow((obj2.max.z - obj2.center.z), 2));

        float distanceBetweenObjects = (Mathf.Pow((obj1.center.x - obj2.center.x), 2) + (Mathf.Pow((obj1.center.z - obj2.center.z), 2)));

        float sumOfRadiiSq = obj1RadiusSq + obj2RadiusSq;

        if (distanceBetweenObjects < sumOfRadiiSq)
        {
            returnValue = true;
        }

        return returnValue;
    }
    #endregion

    #region HELPER METHODS
    /// <summary>
    /// Return the bounds of the provided game object
    /// </summary>
    /// <param name="obj">Game object that bounds are required for</param>
    /// <returns>Bounds of the object</returns>
    private Bounds GetBounds(GameObject obj)
    {
        Bounds returnValue = new Bounds();

        if (obj.GetComponent<SpriteRenderer>() != null)
        {
            returnValue = obj.GetComponent<SpriteRenderer>().bounds;
        }
        else if (obj.GetComponent<MeshRenderer>() != null)
        {
            returnValue = obj.GetComponent<MeshRenderer>().bounds;
        }

        return returnValue;
    }
    #endregion
}
