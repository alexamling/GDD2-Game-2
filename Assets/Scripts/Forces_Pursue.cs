using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-202.05 - Interactive Media Development
/// 
/// Project 5 - Flocking and Path Following
/// 
/// Class Description   : Pursues an object by seeking its projected position
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 8, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : December 11, 2018
/// Filename            : Forces_Pursue.cs
/// 
/// </summary>

public class Forces_Pursue : Forces
{
    #region VARIABLES
    // Ultimate Force Vector3
    private Vector3 pursueForce;
    private Vector3 targetObjectPosition;
    private Vector3 targetObjectVelocity;

    // Floats
    private float weight;
    #endregion

    #region AWAKE
    // Use this for initialization
    new void Awake()
    {
        base.Awake();

        this.weight = 1f;
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
        if (this.objectProperties.motion == MovementState.Pursueing)
        {
            this.CalculateForces();

            base.totalForce += (this.pursueForce.normalized * this.weight);

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
        this.pursueForce = Vector3.zero;

        this.Pursue(this.objectProperties.currentlyPursuing);
    }
    #endregion

    #region PURSUE METHOD
    /// <summary>
    /// Pursue a target by determining its next probable location
    /// </summary>
    /// <param name="targetObject">GameObject to pursue</param>
    /// <returns>Forced used to pursue the object</returns>
    public void Pursue(GameObject targetObject)
    {        
        this.targetObjectPosition = targetObject.GetComponent<ObjectProperties>().position;
        this.targetObjectVelocity = targetObject.GetComponent<ObjectProperties>().velocity;

        this.pursueForce = forcesSeek.GetSeekForce(this.targetObjectPosition + (this.targetObjectVelocity * this.objectProperties.viewDistance));

        this.pursueForce += this.pursueForce;
    }
    #endregion
}
