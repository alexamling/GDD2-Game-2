using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-320.01 - Game Design and Development 2
/// 
/// Project 2
/// 
/// Class Description   : Calculates and implements the avoidance forces to ensure an object doesn't
///                         run into or through another object
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 9, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : March 26, 2020
/// Filename            : Force_Avoidance.cs
/// 
/// </summary>

public class Forces_Avoidance : Forces
{
    #region VARIABLES
    // List of obstacles to avoid
    private List<GameObject> objectsToAvoid = new List<GameObject>();

    // Ultimate Force Vector3
    private Vector3 avoidanceForce;
    private Vector3 avoidanceForceTemp;
    private Vector3 obstaclePosition;
    private Vector3 localizedObstacleVector;

    // Floats
    private float weight;
    private float dotRight;
    private float dotForward; // dot product to forward
    private float radiiSum; // radii sum
    private float safeDistance;
    #endregion

    #region START
    // Use this for initialization
    new void Start()
    {
        base.Start();

        this.weight = 20f;
        this.safeDistance = 2f;
    }
    #endregion

    #region UPDATE
    // Update is called once per frame
    new void Update()
    {
        this.CalculateForces();

        base.totalForce += (this.avoidanceForce.normalized * this.weight);

        base.Update();
    }
    #endregion

    #region CALCULATE STEERING FORCES
    /// <summary>
    /// Normal steering forces to seek PSG
    /// </summary>
    protected void CalculateForces()
    {
        this.avoidanceForce = Vector3.zero;
        this.obstaclePosition = Vector3.zero;
        this.localizedObstacleVector = Vector3.zero;

        this.dotRight = 0f;
        this.dotForward = 0f;
        this.radiiSum = 0f;

        ObjectAvoidance();
    }
    #endregion

    #region OBJECT AVOIDANCE
    /// <summary>
    /// Loops through all objects in the game and determines which objects need to avoid something and
    /// adds them to an avoidance list.
    /// </summary>
    protected void ObjectAvoidance()
    {
        foreach (KeyValuePair<string, GameObject> obstacle in entityManager.allObjects)
        {
            if (!obstacle.Value.name.Equals(this.name) && 
               (!obstacle.Value.name.Contains("Psg")) && 
               (!obstacle.Value.name.Contains(this.name.Substring(0, 3))))
            {
                this.obstaclePosition = obstacle.Value.GetComponent<ObjectProperties>().position;
                this.localizedObstacleVector = this.obstaclePosition - this.objectProperties.position;

                // dot product to right
                this.dotRight = Vector3.Dot(localizedObstacleVector, transform.right);

                // dot product to forward
                this.dotForward = Vector3.Dot(localizedObstacleVector, transform.forward);

                // radii sum
                this.radiiSum = (obstacle.Value.GetComponent<ObjectProperties>().radius + obstacle.Value.GetComponent<ObjectProperties>().bounds.extents.x) * 1.1f;

                this.safeDistance = obstacle.Value.GetComponent<ObjectProperties>().avoidanceDistance;

                if (localizedObstacleVector.magnitude < safeDistance)
                {
                    if (dotForward > 0)
                    {
                        if (Mathf.Abs(dotRight) < radiiSum)
                        {
                            // Add the obstacle to the avoidance list
                            objectsToAvoid.Add(obstacle.Value);
                        }
                    }

                    this.obstaclePosition = Vector3.zero;
                }
            }
        }

        // If there are obstacles to avoid, implement avoidance
        if (objectsToAvoid.Count > 0)
        {
            ApplyObjectAvoidance();
            objectsToAvoid.Clear();
        }
    }

    /// <summary>
    /// Cycle through the list of objects that require avoidance calculations
    /// and apply the required forces appropriately
    /// </summary>
    private void ApplyObjectAvoidance()
    {
        foreach (GameObject obstacle in objectsToAvoid)
        {
            this.obstaclePosition = obstacle.GetComponent<ObjectProperties>().position;
            localizedObstacleVector = this.obstaclePosition - this.objectProperties.position;

            // dot product to right
            float dotRight = Vector3.Dot(localizedObstacleVector, transform.right);

            this.avoidanceForceTemp = this.transform.right * this.objectProperties.maxSpeed * -1;

            if (dotRight < 0)
            {
                this.avoidanceForceTemp = avoidanceForceTemp * -1;
            }

            this.avoidanceForce += (avoidanceForceTemp - this.objectProperties.velocity);
        }
    }
    #endregion
}
