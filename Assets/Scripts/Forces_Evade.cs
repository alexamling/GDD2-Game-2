using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-320.01 - Game Design and Development 2
/// 
/// Project 2
/// 
/// Class Description   : Implements evasion by "dodging" the future position of the object it's evading
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 8, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : March 26, 2020
/// Filename            : Forces_Evade.cs
/// 
/// </summary>

public class Forces_Evade : Forces
{
    #region VARIABLES
    // Ultimate Force Vector3
    private Vector3 evadeForce;
    private Vector3 hunterObjectPosition;
    private Vector3 hunterObjectVelocity;

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
        if (this.objectProperties.motion == MovementState.Evading)
        {
            this.CalculateForces();

            this.objectProperties.totalForce += (this.evadeForce.normalized * this.weight);

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
        this.evadeForce = Vector3.zero;

        this.Evade(this.objectProperties.currentlyPursuing);
    }
    #endregion

    #region PURSUE METHOD
    /// <summary>
    /// Pursue a target by determining its next probable location
    /// </summary>
    /// <param name="hunterObject">GameObject to pursue</param>
    /// <returns>Forced used to pursue the object</returns>
    public void Evade(GameObject hunterObject)
    {
        this.hunterObjectPosition = hunterObject.GetComponent<ObjectProperties>().position;
        this.hunterObjectVelocity = hunterObject.GetComponent<ObjectProperties>().velocity;

        this.evadeForce = forcesFlee.GetFleeForce(this.hunterObjectPosition - (this.hunterObjectVelocity * this.objectProperties.viewDistance));

        this.evadeForce += this.evadeForce;
    }
    #endregion
}
