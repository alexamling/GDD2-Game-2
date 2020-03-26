using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-320.01 - Game Design and Development 2
/// 
/// Project 2
/// 
/// Class Description   : Flees from a target location
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 9, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : March 26, 2020
/// Filename            : Forces_Flee.cs
/// 
/// </summary>

public class Forces_Flee : Forces
{
    #region VARIABLES
    // Ultimate Force Vector3
    private Vector3 fleeForce;
    
    // Floats
    private float weight;
    #endregion

    #region AWAKE
    // Use this for initialization
    new void Awake()
    {
        base.Awake();

        this.weight = 20f;
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
        if (this.objectProperties.motion == MovementState.Fleeing)
        {
            this.CalculateForces();

            base.totalForce += (this.fleeForce.normalized * this.weight);

            base.Update();
        }
    }
    #endregion

    #region CALCULATE STEERING FORCES
    /// <summary>
    /// Normal steering forces to seek PSG
    /// </summary>
    protected void CalculateForces()
    {
        this.fleeForce = Vector3.zero;
        base.desiredVelocity = Vector3.zero;

        this.Flee(this.objectProperties.currentlyFleeing);
    }
    #endregion

    #region EXTERNAL ACCESS TO FLEE METHOD
    public Vector3 GetFleeForce(GameObject huntingObject)
    {
        this.fleeForce = Vector3.zero;

        this.Flee(huntingObject);

        return this.fleeForce;
    }

    public Vector3 GetFleeForce(Vector3 huntingObjectPosition)
    {
        this.fleeForce = Vector3.zero;

        this.Flee(huntingObjectPosition);

        return this.fleeForce;
    }
    #endregion

    #region FLEE METHODS
    // All Entitys have the knowledge of how to flee
    // They just may not be calling it all the time
    /// <summary>
    /// Flee using vector3
    /// </summary>
    /// <param name="targetPosition">Vector3 position of desired target (random point)</param>
    /// <returns>Steering force calculated to seek the desired target (random point)</returns>
    private void Flee(Vector3 huntingObjectPosition)
    {
        // Find DV (desired veloctiy)
        base.desiredVelocity = this.objectProperties.position - huntingObjectPosition;

        // Scale vel to max speed
        base.desiredVelocity.Normalize();
        base.desiredVelocity = base.desiredVelocity * this.objectProperties.maxSpeed;

        this.fleeForce += base.desiredVelocity;
    }

    /// <summary>
    /// Flee using gameobject
    /// </summary>
    /// <param name="targetPosition">Vector3 position of desired target (random point)</param>
    /// <returns>Steering force calculated to seek the desired target (random point)</returns>
    private void Flee(GameObject huntingObject)
    {
        //  Find DV (desired velocity)
        base.desiredVelocity = this.objectProperties.position - huntingObject.transform.position;

        // Scale vel to max speed
        base.desiredVelocity.Normalize();
        base.desiredVelocity = base.desiredVelocity * this.objectProperties.maxSpeed;

        this.fleeForce += base.desiredVelocity;
    }

    #endregion
}
