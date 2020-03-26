using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-320.01 - Game Design and Development 2
/// 
/// Project 2
/// 
/// Class Description   : Applies forces inward from the boundary in the event that an object breaches
///                         the border.
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 8, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : March 26, 2020
/// Filename            : Forces_Boundary.cs
/// 
/// </summary>

public class Forces_Boundary : Forces
{
    #region VARIABLES
    // Ultimate Force Vector3
    private Vector3 boundaryForce;

    // Floats
    protected float boundaryBuffer;
    private float weight;
    #endregion

    #region AWAKE
    // Use this for initialization
    new void Awake()
    {
        base.Awake();

        this.boundaryBuffer = 5f;
        this.weight = 1f;
    }
    #endregion

    #region START
    // Use this for initialization also
    new void Start()
    {
        base.Start();
    }
    #endregion

    #region UPDATE
    // Update is called once per frame
    new void Update()
    {
        this.KeepInBounds();
                
        this.CalculateForces();

        base.totalForce += (this.boundaryForce.normalized * this.weight);

        base.Update();
        
    }
    #endregion

    #region CALCULATE STEERING FORCES
    /// <summary>
    /// Normal steering forces to seek PSG
    /// </summary>
    protected void CalculateForces()
    {
        this.boundaryForce = Vector3.zero;

        this.Boundary();
    }
    #endregion

    #region PREVENT BOUND EXIT
    /// <summary>
    /// If an object happens to try and exit the bounds, redirect them to the center
    /// </summary>
    private void KeepInBounds()
    {
        // If the object is moving out of the play area
        if (objectProperties.position.x < terrainManager.terrainCubeMin.x)
        {
            objectProperties.position.x = terrainManager.terrainCubeMin.x;
        }
        else if (objectProperties.position.x > terrainManager.terrainCubeMax.x)
        {
            objectProperties.position.x = terrainManager.terrainCubeMax.x;
        }

        if (objectProperties.position.y < terrainManager.terrainCubeMin.y)
        {
            objectProperties.position.y = terrainManager.terrainCubeMin.y;
        }
        else if (objectProperties.position.y > terrainManager.terrainCubeMax.y)
        {
            objectProperties.position.y = terrainManager.terrainCubeMax.y;
        }

        if (objectProperties.position.z < terrainManager.terrainCubeMin.z)
        {
            objectProperties.position.z = terrainManager.terrainCubeMin.z;
        }
        else if (objectProperties.position.z > terrainManager.terrainCubeMax.z)
        {
            objectProperties.position.z = terrainManager.terrainCubeMax.z;
        }
    }
    #endregion

    #region BOUNDARY BUFFER IMPLEMENTATION
    /// <summary>
    /// Start turning the object away from the wall the moment it gets too close to the edge
    /// </summary>
    private void Boundary()
    {
        // This section is intended to keep the object from reaching the outer edges of the play area
        // If the object exits the screen on the left or right
        if ((this.objectProperties.bounds.min.x - 5 < terrainManager.terrainCubeMin.x) ||
            (this.objectProperties.bounds.max.x + 5 > terrainManager.terrainCubeMax.x) ||
            (this.objectProperties.bounds.min.y - 5 < terrainManager.terrainCubeMin.y) ||
            (this.objectProperties.bounds.max.y + 5 > terrainManager.terrainCubeMax.y) ||
            (this.objectProperties.bounds.min.z - 5 < terrainManager.terrainCubeMin.z) ||
            (this.objectProperties.bounds.max.z + 5 > terrainManager.terrainCubeMax.z))
        {
            if (this.objectProperties.motion != MovementState.BoundaryContact)
            {
                this.objectProperties.motion = MovementState.BoundaryContact;
            }

            this.boundaryForce += forcesSeek.GetSeekForce(terrainManager.terrainCubeCenter);
        }
        else
        {
            if (this.objectProperties.motion == MovementState.BoundaryContact)
            {
                this.objectProperties.motion = MovementState.Wandering;
            }            
        }        
    }
    #endregion
}
