using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-320.01 - Game Design and Development 2
/// 
/// Project 2
/// 
/// Class Description   : Seek a target object or location
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 9, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : March 26, 2020
/// Filename            : Forces_Seek.cs
/// 
/// </summary>

public class Forces_Seek : Forces
{
    #region VARIABLES
    // Ultimate Force Vector3
    private Vector3 seekForce;

    // Floats
    private float weight;
    #endregion

    #region AWAKE
    // Use this for initialization
    new void Awake()
    {
        base.Awake();

        this.weight = 10f;
    }
    #endregion

    #region START
    // Use this for initialization
    new void Start()
    {
        base.Start();
    }
    #endregion

    #region UPDATE
    // Update is called once per frame
    new void Update()
    {
        //this.seekForce = Vector3.zero;

        this.CalculateForces();

        base.totalForce += (this.seekForce.normalized * this.weight);

        base.Update();
    }
    #endregion

    #region CALCULATE STEERING FORCES
    /// <summary>
    /// Normal steering forces to seek PSG
    /// </summary>
    protected void CalculateForces()
    {
        this.seekForce = Vector3.zero;
        base.desiredVelocity = Vector3.zero;

        this.Seek(this.objectProperties.currentlySeeking);
    }
    #endregion

    #region EXTERNAL ACCESS TO SEEK METHOD
    public Vector3 GetSeekForce(GameObject targetObject)
    {
        this.seekForce = Vector3.zero;

        this.Seek(targetObject);

        return this.seekForce;
    }

    public Vector3 GetSeekForce(Vector3 targetObjectPosition)
    {
        this.seekForce = Vector3.zero;

        this.Seek(targetObjectPosition);

        return this.seekForce;
    }
    #endregion

    #region SEEK METHODS
    // All Entitys have the knowledge of how to seek
    // They just may not be calling it all the time
    /// <summary>
    /// Seek using vector3
    /// </summary>
    /// <param name="targetPosition">Vector3 position of desired target</param>
    /// <returns>Steering force calculated to seek the desired target</returns>
    private void Seek(Vector3 targetPosition)
    {
        // Step 1: Find DV (desired velocity)
        base.desiredVelocity = targetPosition - this.objectProperties.position;

        // Step 2: Scale vel to max speed
        base.desiredVelocity.Normalize();
        base.desiredVelocity = base.desiredVelocity * this.objectProperties.maxSpeed;

        this.seekForce += base.desiredVelocity;
    }

    /// <summary>
    /// Seek using gameobject
    /// </summary>
    /// <param name="targetPosition">Vector3 position of desired target</param>
    /// <returns>Steering force calculated to seek the desired target</returns>
    private void Seek(GameObject targetObject)
    {
        // Step 1: Find DV (desired velocity)
        base.desiredVelocity = targetObject.transform.position - this.objectProperties.position;

        // Step 2: Scale vel to max speed
        base.desiredVelocity.Normalize();
        base.desiredVelocity = base.desiredVelocity * this.objectProperties.maxSpeed;
                
        this.seekForce += desiredVelocity;
    }
    #endregion
}
