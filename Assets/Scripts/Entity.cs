using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-320.01 - Game Design and Development 2
/// 
/// Project 2
/// 
/// Class Description   : Applies the previously calculated forces to the game objects.
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 8, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : March 26, 2020
/// Filename            : Entity.cs
/// 
/// </summary>

public class Entity : MonoBehaviour
{
    #region VARIABLES
    #region REFERENCES TO GAMEOBJECTS
    // Managers
    protected GameObject masterManager;
    protected EntityManager entityManager;
    protected FlockManager flockManager;
    protected TerrainManager terrainManager;

    // Properties
    protected ObjectProperties objectProperties;
    #endregion

    #region OTHER VARIABLES
    // Vectors necessary for force-based movement
    private Vector3 position;
    private Vector3 acceleration;
    private Vector3 direction;
    private Vector3 velocity;

    // Forces always applied
    private Vector3 friction;
    private static Vector3 gravitationalForce;
    private static float normalForce;
    private Vector3 desiredVelocity;
    // Forces to be applied
    //public Vector3 ultimateForce;
    protected Vector3 classForce;
    #endregion
    #endregion

    #region AWAKE
    // Use this for initialization
    protected void Awake()
    {
        masterManager = GameObject.FindGameObjectWithTag("Managers");
    }
    #endregion

    #region START
    // Use this for initialization also
    protected void Start()
    {
        entityManager = masterManager.GetComponentInChildren<EntityManager>();
        flockManager = masterManager.GetComponentInChildren<FlockManager>();
        terrainManager = masterManager.GetComponentInChildren<TerrainManager>();

        normalForce = 1.00f;
        gravitationalForce = new Vector3(0f, -9.80f, 0f);
    }
    #endregion

    #region UPDATE
    // Update is called once per frame
    protected void Update()
    {        
        this.velocity = this.objectProperties.velocity;
        this.position = this.objectProperties.position;
        this.direction = this.objectProperties.direction;

        ApplyForces();

        this.objectProperties.velocity = this.velocity;
        this.objectProperties.position = this.position;
        this.objectProperties.direction = this.direction;
    }
    #endregion

    #region FORCE APPLICATION METHODS
    private void ApplyForces()
    {
        ApplyFriction(normalForce);
        //ApplyGravity(gravitationalForce);
        ApplyForce(this.objectProperties.ultimateForce);

        this.velocity += this.acceleration * Time.deltaTime;
        this.velocity = Vector3.ClampMagnitude(this.velocity, this.objectProperties.maxSpeed);

        Vector3 seekingPosition = this.objectProperties.currentlySeeking.GetComponent<ObjectProperties>().position;

        // Arrival
        if (Vector3.Distance(this.objectProperties.position, seekingPosition) < 8)
        {
            this.velocity = this.velocity * 0.99f;
        }

        this.position += this.velocity * Time.deltaTime;
        this.direction = this.velocity.normalized;

        this.transform.position = this.position;
        this.transform.rotation = Quaternion.LookRotation(this.velocity, Vector3.up);

        this.acceleration = Vector3.zero;
        this.objectProperties.ultimateForce = Vector3.zero;

    }

    private void ApplyForce(Vector3 force)
    {
        this.acceleration += force / this.objectProperties.mass;
    }

    private void ApplyGravity(Vector3 force)
    {
        this.acceleration += force;
    }

    private void ApplyFriction(float coeff)
    {
        this.friction = this.velocity * -1;
        this.friction.Normalize();
        this.friction = this.friction * coeff;
        this.acceleration += this.friction;
    }
    #endregion
}
