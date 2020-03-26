using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-320.01 - Game Design and Development 2
/// 
/// Project 2
/// 
/// Class Description   : Calculates and applies forces to implement separation.  Each object flees from
///                         the object closest to itself.
/// Created By          : Benjamin Kleynhans
/// Date Created        : November 30, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : March 26, 2020
/// Filename            : Separation.cs
/// 
/// </summary>

public class Forces_Separation : Forces
{
    #region VARIABLES
    #region GAME OBJECTS    
    private GameObject closestObject;
    #endregion

    #region VECTOR3s
    // Ultimate Force Vector3
    private Vector3 separationForce;
    private Vector3 closestObjectPosition;
    private Vector3 testObjectPosition;

    // Floats
    private float weight;
    #endregion
    #endregion

    #region AWAKE
    /// <summary>
    /// Runs during initialization
    /// </summary>
    new void Awake()
    {
        base.Awake();

        this.weight = 20f;

        // Set closest object to this object
        this.closestObject = gameObject;
    }
    #endregion

    #region START
    /// <summary>
    /// Use this for initialization
    /// </summary>
    new void Start()
    {
        base.Start();
    }
    #endregion

    #region UPDATE
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    new void Update()
    {
        this.separationForce = Vector3.zero;

        this.CalculateForces();

        base.totalForce += (this.separationForce.normalized * this.weight);

        base.Update();
    }
    #endregion

    #region CALCULATE STEERING FORCES
    /// <summary>
    /// Normal steering forces to seek PSG
    /// </summary>
    protected void CalculateForces()
    {
        this.FindClosest();
        this.ApplySeparation();

        this.closestObject = gameObject;
    }
    #endregion

    #region SEPARATION CALCULATION
    /// <summary>
    /// Find the closest object to this object of the same type
    /// </summary>
    private void FindClosest()
    {
        foreach (KeyValuePair<string, GameObject> obj in entityManager.allObjects)
        {
            // If the object in the list is of the same type as this object and it is not this object
            if (obj.Value.gameObject.name.Contains(gameObject.name.Substring(0, 4)) && 
                (!obj.Value.Equals(gameObject)))
            {
                //objectProperties.position = gameObject.transform.position;
                this.testObjectPosition = obj.Value.GetComponent<ObjectProperties>().position;
                this.closestObjectPosition = this.closestObject.GetComponent<ObjectProperties>().position;

                // If the closest object is this object, it is the first time running the code.
                // Set the first object found that is not this object, to the closest object
                if (closestObject.Equals(gameObject))
                {
                    closestObject = obj.Value;
                }
                // If the object found is closer to this object than the current closest object, update closest object
                else if ((Vector3.Distance(this.testObjectPosition, this.objectProperties.position)) < (Vector3.Distance(this.closestObjectPosition, this.objectProperties.position)))
                {
                    this.closestObject = obj.Value;
                }
            }
        }
    }

    /// <summary>
    /// Apply the seperation force
    /// </summary>
    private void ApplySeparation()
    {
        // If this is not the first time running the code
        if (!this.closestObject.Equals(gameObject))
        {
            // If the closest object is closer than 3 units to this object
            if (Vector3.Distance(this.objectProperties.position, this.closestObject.GetComponent<ObjectProperties>().position) < this.objectProperties.separationDistance)
            {
                this.separationForce += forcesFlee.GetFleeForce(this.closestObject);
            }
        }
    }
    #endregion
}
