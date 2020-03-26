using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-320.01 - Game Design and Development 2
/// 
/// Project 2
/// 
/// Class Description   : Calculates the forward force to be applied to the flock for alignment
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 11, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : March 26, 2020
/// Filename            : Forces_Alignment.cs
/// 
/// </summary>

public class Forces_Alignment : Forces
{
    #region VARIABLES
    // Ultimate Force Vector3
    private Vector3 alignmentForce;

    // Other Forces Vector3
    private Vector3 forwardForce;

    // View Distance float
    private float viewDistance = 5f;
    

    // Floats
    private float weight;
    private float forwardWeight;
    #endregion

    #region AWAKE
    // Use this for initialization
    new void Awake()
    {
        base.Awake();

        this.weight = 5f;
        this.forwardWeight = 6f;
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
        this.CalculateForces();

        base.totalForce += (this.alignmentForce.normalized * this.weight);

        base.Update();
    }
    #endregion

    #region CALCULATE STEERING FORCES
    /// <summary>
    /// Normal steering forces to seek PSG
    /// </summary>
    protected void CalculateForces()
    {
        this.alignmentForce = Vector3.zero;
        this.forwardForce = Vector3.zero;

        ApplyFlocking();
    }
    #endregion

    #region APPLY ALIGNMENT
    /// <summary>
    /// Zombie evasion conditions and forces utilizing flee method in base class
    /// </summary>
    private void ApplyFlocking()
    {
        this.forwardForce = forcesSeek.GetSeekForce(this.objectProperties.currentlySeeking.GetComponent<ObjectProperties>().position) * this.forwardWeight;

        this.alignmentForce = this.forwardForce;
    }
    #endregion
}