using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-320.01 - Game Design and Development 2
/// 
/// Project 2
/// 
/// Class Description   : Implements cohesion by searching the center of the flock
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 8, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : March 26, 2020
/// Filename            : Forces_Cohesion.cs
/// 
/// </summary>

public class Forces_Cohesion : Forces
{
    #region VARIABLES
    // Ultimate Force Vector3
    private Vector3 cohesionForce;

    // Other Forces Vector3
    private Vector3 centerForce;
    private Vector3 forwardForce;

    // View Distance float
    private float viewDistance = 5f;    

    // Floats
    private float weight;
    private float centerWeight;
    #endregion

    #region AWAKE
    // Use this for initialization
    new void Awake()
    {
        base.Awake();

        this.weight = 5f;
        this.centerWeight = 5f;
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
        this.CalculateForces();

        base.totalForce += (this.cohesionForce.normalized * this.weight);

        base.Update();
    }
    #endregion

    #region CALCULATE STEERING FORCES
    /// <summary>
    /// Normal steering forces to seek PSG
    /// </summary>
    protected void CalculateForces()
    {
        this.cohesionForce = Vector3.zero;

        ApplyFlocking();
    }

    /// <summary>
    /// Zombie evasion conditions and forces utilizing flee method in base class
    /// </summary>
    private void ApplyFlocking()
    {
        //this.centerForce = forcesSeek.GetSeekForce(this.objectProperties.flockCenter);// * this.centerWeight;
        this.centerForce = forcesSeek.GetSeekForce(this.objectProperties.flockCenter);// * this.centerWeight;

        this.cohesionForce = this.centerForce;
    }
    #endregion
}