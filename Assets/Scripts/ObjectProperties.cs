using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-202.05 - Interactive Media Development
/// 
/// Project 5 - Flocking and Path Following
/// 
/// Class Description   : This is a container file.  All properties associated with an object
///                         are stored in this file that is attached to it.
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 8, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : December 11, 2018
/// Filename            : ObjectProperties.cs
/// 
/// </summary>

public enum MovementState
{
    BoundaryContact,
    Evading,
    Fleeing,
    Leading,
    Pursueing,
    Seeking,
    Wandering,
    None
}

public class ObjectProperties : MonoBehaviour
{
    #region VARIABLES
    #region MOTION DEFINITION
    public MovementState motion;
    #endregion

    #region OBJECTS SEEKING, FLEEING, PURSUING OR EVADING
    public GameObject currentlyFleeing;
    public GameObject currentlyEvading;
    public GameObject currentlySeeking;
    public GameObject currentlyPursuing;
    #endregion

    #region OBJECT BOUNDS
    public Bounds bounds;
    #endregion

    #region LIST OF CURRENT COLLISIONS
    public Dictionary<string, GameObject> currentCollisions = new Dictionary<string, GameObject>();
    #endregion

    #region FORCES, POSITIONS, DIRECTIONS

    Rigidbody rb;
    #endregion

    #region OBJECT TYPE SPECIFIC PROPERTIES
    // Instantiation
    public float mass;
    public float maxSpeed;
    public float radius;

    // View distances
    public float separationDistance;
    public float avoidanceDistance;
    public float viewDistance;

    // Weights
    public float seekWeight;
    public float fleeWeight;
    public float pursueWeight;
    public float evadeWeight;
    #endregion

    #region TYPE OF OBJECT
    public bool seeker;
    public bool fleer;
    #endregion
    #endregion

    #region AWAKE
    // Use this for initialization
    private void Awake()
    {
       

        this.motion = MovementState.Wandering;

        this.separationDistance = 3f;
        this.avoidanceDistance = 10f;
        this.viewDistance = 5f;

        this.radius = this.bounds.extents.x;
    }
    #endregion

    #region START
    // Use this for initialization
    void Start()
    {
        
    }
    #endregion

    #region UPDATE
    // Update is called once per frame
    void Update()
    {
      

    }
    #endregion

    #region HELPER FUNCTIONS
    /// <summary>
    /// Return the bounds of the provided game object
    /// </summary>
    /// <param name="obj">Game object that bounds are required for</param>
    /// <returns>Bounds of the object</returns>
   
    #endregion
}
