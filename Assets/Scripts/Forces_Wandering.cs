using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-320.01 - Game Design and Development 2
/// 
/// Project 2
/// 
/// Class Description   : Implements wandering by projecting two circles infront of the object.  The first
///                         is projected along the Y axis and the other along the Z axis.  This allows for
///                         wandering in 3 Dimensions.  An arbitrary point is chosen on each circle at random
///                         and then the object is moved in that direction.  The wandering algorithm is 
///                         executed once per second using InvokeRepeating.
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 8, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : March 26, 2020
/// Filename            : Forces_Wandering.cs
/// 
/// </summary>

public class Forces_Wandering : Forces
{
    #region VARIABLES
    #region VECTORs
    // Wander Variables
    // Vectors
    private Vector3 currentPosition;
    private Vector3 currentDirection;    
    private Vector3 newDirection;
    private Vector3 directionToPointOnDisk;
    private Vector3 newPositionOnDisk;
    private Vector3 diskCenterPosition;
    private Vector3 vectorV;                    // non-normalize forward vector / or position vector on local coordinates
    private Vector3 vectorW;                    // non-normalize forward vector / or position vector on local coordinates
    private Vector3 wanderForce;
    #endregion

    #region FLOATS
    // Floats
    private float distanceToDiskCenter;
    private float diskRadius;
    private float randomDegreesOnDiskY;
    private float randomDegreesOnDiskZ;
    private float adjustmentAngleRadians;    
    private float adjustmentAngleDegrees;    
    // Floats
    private float weight;
    #endregion
    #endregion

    #region AWAKE
    /// <summary>
    /// Use this for initialization
    /// </summary>
    new void Awake()
    {
        base.Awake();

        this.weight = 1f;

        this.distanceToDiskCenter = 3f;
        this.diskRadius = 1.5f;
    }
    #endregion

    #region START
    /// <summary>
    /// Use this for initialization also
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
        if (this.objectProperties.motion == MovementState.Wandering)
        {
            // Run this method once per second
            if (!IsInvoking("CalculateForces"))
            {
                InvokeRepeating("CalculateForces", 0, 1.0f);
            }
        }
        else if (IsInvoking("CalculateForces"))
        {
            CancelInvoke("CalculateForces");
        }
    }
    #endregion

    #region CALCULATE STEERING FORCES
    /// <summary>
    /// Normal steering forces to seek PSG
    /// </summary>
    protected void CalculateForces()
    {
        this.wanderForce = Vector3.zero;

        this.ApplyWandering();

        base.totalForce += (this.wanderForce.normalized * this.weight);

        base.Update();
    }
    #endregion

    #region CALCULATE AND APPLY WANDERING
    /// <summary>
    /// Calculate and apply wandering motion
    /// </summary>
    private void ApplyWandering()
    {        
        this.currentPosition = this.objectProperties.position;

        // When the object is instantiated it doesn't have a direction, set the direction before continuing
        if (this.objectProperties.direction == Vector3.zero)
        {
            this.objectProperties.direction = new Vector3(1.0f, 0f, 0f);
        }

        this.currentDirection = this.objectProperties.direction;

        // Calculate wandering
        Wander();

        // Adjust the direction in which the prefab is facing
        this.objectProperties.direction = Quaternion.Euler(0f, this.adjustmentAngleDegrees, this.adjustmentAngleDegrees) * this.currentDirection;
        // Add the force for the required movement
        this.wanderForce = Quaternion.Euler(0f, this.adjustmentAngleDegrees, this.adjustmentAngleDegrees) * this.currentDirection;
    }
    #endregion

    #region DETAILED WANDERING CALCULATIONS
    /// <summary>
    /// Perform detailed calculations to implement wandering
    /// </summary>
    public void Wander()
    {
        // Calculate the center of the projected disk
        this.diskCenterPosition = this.currentPosition + (this.currentDirection * this.distanceToDiskCenter);

        // Get random degrees to project onto the edge of the disk
        this.randomDegreesOnDiskY = Random.Range(-180f, 180f);
        this.randomDegreesOnDiskZ = Random.Range(-180f, 180f);

        // Calculate the direction to the point on the edge of the disk
        this.directionToPointOnDisk = Quaternion.Euler(0f, this.randomDegreesOnDiskY, this.randomDegreesOnDiskZ) * this.currentDirection;

        // Calculate the position vector of the projected point on the edge of the disk
        this.newPositionOnDisk = this.diskCenterPosition + (this.directionToPointOnDisk * this.diskRadius);
                
        // Calculate VectorV and VectorW for use in dot product calculation of angle adjustment
        this.vectorV = this.diskCenterPosition - this.currentPosition;
        this.vectorW = this.newPositionOnDisk - this.currentPosition;

        // Calculate the angle to adjust the object        
        this.adjustmentAngleRadians = Mathf.Acos((Vector3.Dot(this.vectorV, this.vectorW)) / (Vector3.Magnitude(this.vectorV) * Vector3.Magnitude(this.vectorW)));
        
        // Convert the angle from radians to degrees
        this.adjustmentAngleDegrees = Mathf.Abs(this.adjustmentAngleRadians * Mathf.Rad2Deg);
    }
    #endregion
}
