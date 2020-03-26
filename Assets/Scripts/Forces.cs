using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-320.01 - Game Design and Development 2
/// 
/// Project 2
/// 
/// Class Description   : This is a management class to consolidate all forces that are to be
///                         applied to a game object.  Once all weighted forces have been combined
///                         the total is normalized and applied to the entity class where the
///                         sum of the weighted forces is applied to the object.
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 9, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : March 26, 2020
/// Filename            : Forces.cs
/// 
/// </summary>

public class Forces : MonoBehaviour
{
    #region VARIABLES
    #region REFERENCES TO GAMEOBJECTS    
    // Managers
    protected GameObject masterManager;    
    protected EntityManager entityManager;
    protected TerrainManager terrainManager;
    
    public Entity entity;
    #endregion

    #region SCRIPTS AND PROPERTIES
    // Scripts
    public Forces_Seek forcesSeek;
    public Forces_Flee forcesFlee;

    // Properties
    protected ObjectProperties objectProperties;

    // Float
    public Vector3 totalForce;
    public Vector3 desiredVelocity;
    #endregion
    #endregion

    #region AWAKE
    protected void Awake()
    {
        masterManager = GameObject.FindGameObjectWithTag("Managers");        
    }
    #endregion

    #region START
    // Use this for initialization
    protected void Start()
    {
        entityManager = masterManager.GetComponentInChildren<EntityManager>();
        entity = masterManager.GetComponentInChildren<Entity>();
        terrainManager = masterManager.GetComponentInChildren<TerrainManager>();

        this.objectProperties = this.gameObject.GetComponent<ObjectProperties>();

        this.objectProperties.totalForce = Vector3.zero;
        forcesSeek = this.gameObject.GetComponent<Forces_Seek>();
        forcesFlee = this.gameObject.GetComponent<Forces_Flee>();
    }
    #endregion

    #region UPDATE
    // Update is called once per frame
    protected void Update()
    {
        //entity.ultimateForce += this.totalForce;
        this.objectProperties.ultimateForce += this.totalForce;
        //this.objectProperties.ultimateForce += this.objectProperties.totalForce;
        this.totalForce = Vector3.zero;
    }
    #endregion
}
