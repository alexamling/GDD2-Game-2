using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-202.05 - Interactive Media Development
/// 
/// Project 5 - Flocking and Path Following
/// 
/// Class Description   : Manages the processes behind the implementation of the CollisionDetection script
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 10, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : December 11, 2018
/// Filename            : CollisionManager.cs
/// 
/// </summary>

public class CollisionManager : MonoBehaviour
{
    #region VARIABLES
    #region REFERENCES TO GAMEOBJECTS
    // Managers
    protected GameObject masterManager;
    protected EntityManager entityManager;
    protected FlockManager flockManager;
    protected TerrainManager terrainManager;
    protected CollisionDetection collision;
    private ObjectProperties objectProperties;
    #endregion

    #region OTHER VARIABLES
    private Vector3 tempVector;
    private bool safePosition;
    private int currentWaypoint;
    #endregion
    #endregion

    #region AWAKE
    // Use this for initialization
    protected void Awake()
    {
        masterManager = GameObject.FindGameObjectWithTag("Managers");

        tempVector = Vector3.zero;
        this.safePosition = true;
    }
    #endregion

    #region START
    // Use this for initialization also
    protected void Start()
    {
        this.entityManager = masterManager.GetComponentInChildren<EntityManager>();
        this.flockManager = masterManager.GetComponentInChildren<FlockManager>();
        this.terrainManager = masterManager.GetComponentInChildren<TerrainManager>();

        this.collision = this.gameObject.GetComponentInChildren<CollisionDetection>();
    }
    #endregion

    #region UPDATE
    // Runs every frame
    protected void Update()
    {
        this.CheckCollisions();
        this.ProcessCollisions();
    }
    #endregion

    #region CHECK FOR COLLIDING OBJECTS
    /// <summary>
    /// Determines which objects are colliding and updates each objects' list of colliding objects accordingly
    /// </summary>
    private void CheckCollisions()
    {
        // If the allObjects Dictionary is not empty
        if (this.entityManager.allObjects.Count > 0)
        {
            // For each object in the dictionary
            foreach (KeyValuePair<string, GameObject> obj0 in this.entityManager.allObjects)
            {
                this.objectProperties = obj0.Value.GetComponent<ObjectProperties>();

                // Compare it to every other object in the dictionary
                foreach (KeyValuePair<string, GameObject> obj1 in this.entityManager.allObjects)
                {
                    if (!obj0.Equals(obj1))
                    {
                        // Define which objects have special cases for collisions (waypoints need to advance to next point)
                        if (((obj0.Value.name.Contains("PathFollower") && obj1.Value.name.Contains("Waypoint")) ||
                            (obj0.Value.name.Contains("Waypoint") && obj1.Value.name.Contains("PathFollower"))))
                        {
                            if (this.collision.CircleCollision(obj0.Value, 6f, obj1.Value, 6f))
                            {
                                if (!this.objectProperties.currentCollisions.ContainsKey(obj1.Value.name))
                                {
                                    this.objectProperties.currentCollisions.Add(obj1.Value.name, obj1.Value);
                                }
                            }
                        }
                        else if (this.collision.AABBCollision(obj0.Value, obj1.Value))
                        {
                            if (!this.objectProperties.currentCollisions.ContainsKey(obj1.Value.name))
                            {
                                this.objectProperties.currentCollisions.Add(obj1.Value.name, obj1.Value);
                            }                            
                        }
                    }
                }
            }
        }
    }
    #endregion

    #region PROCESS ALL OBJECTS WITH COLLIDING OBJECTS
    /// <summary>
    /// Process the list of objects that have active collisions
    /// </summary>
    private void ProcessCollisions()
    {
        if (this.entityManager.allObjects.Count > 0)
        {  
            // For each object in allObjects
            foreach (KeyValuePair<string, GameObject> obj0 in this.entityManager.allObjects)
            {
                this.objectProperties = obj0.Value.GetComponent<ObjectProperties>();

                // If it contains any active collisions
                if (this.objectProperties.currentCollisions.Count > 0)
                {
                    // Perform the appropriate action based on the object experiencing the collision
                    foreach (KeyValuePair<string, GameObject> obj1 in this.objectProperties.currentCollisions)
                    {
                        if (obj0.Key.Contains("Psg") && obj1.Key.Contains("Flocker"))
                        {
                            ProcessPsg(obj0.Value, obj1.Value);
                        }
                        else if (obj0.Key.Contains("Flocker") && obj1.Key.Contains("Psg"))
                        {
                            ProcessPsg(obj1.Value, obj0.Value);
                        }
                        else if (obj0.Key.Contains("Waypoint") && obj1.Key.Contains("PathFollower"))
                        {
                            ProcessWaypoint(obj0.Value, obj1.Value);
                        }
                        else if (obj0.Key.Contains("PathFollower") && obj1.Key.Contains("Waypoint"))
                        {
                            ProcessWaypoint(obj1.Value, obj0.Value);
                        }
                    }
                }

                // Clear the list of collisions
                obj0.Value.GetComponent<ObjectProperties>().currentCollisions.Clear();
            }
        }
    }
    #endregion

    #region PERFORM PSG RELATED PROCESSING
    /// <summary>
    /// Perform actions appropriate to Psg Collisions
    /// </summary>
    /// <param name="psg">The psg object</param>
    /// <param name="obj1">The colliding object</param>
    private void ProcessPsg(GameObject psg, GameObject obj1)
    {
        psg.GetComponent<MeshRenderer>().enabled = false;

        // Generate a new random position for the Psg
        //do
        //{
            this.tempVector = Vector3.zero;

            this.tempVector = new Vector3(UnityEngine.Random.Range(this.terrainManager.terrainCubeMin.x + 10, this.terrainManager.terrainCubeMax.x - 10),
                                          UnityEngine.Random.Range(this.terrainManager.terrainCubeMin.y + 10, this.terrainManager.terrainCubeMax.y - 10),
                                          UnityEngine.Random.Range(this.terrainManager.terrainCubeMin.z + 10, this.terrainManager.terrainCubeMax.z - 10));

            psg.transform.position = this.tempVector;

            // Ensure there is nothing else that the Psg would be colliding with in the new location
            foreach (KeyValuePair<string, GameObject> obj0 in this.entityManager.allObjects)
            {
                if (!psg.Equals(obj0.Value))
                {
                    if (this.collision.AABBCollision(psg, obj0.Value))
                    {
                        this.safePosition = false;
                        break;
                    }
                }
            }

        //} while (!safePosition);

        // Move the Psg to the new position
        psg.GetComponent<ObjectProperties>().position = psg.transform.position;

        psg.GetComponent<MeshRenderer>().enabled = true;
    }
    #endregion

    #region PERFORM WAYPOINT RELATED PROCESSING
    /// <summary>
    /// Moves the target of the pathfollower to the next waypoint
    /// </summary>
    /// <param name="waypoint">The waypoint reached</param>
    /// <param name="pathFollower">The path follower</param>
    private void ProcessWaypoint(GameObject waypoint, GameObject pathFollower)
    {        
        this.currentWaypoint = Int32.Parse(waypoint.name.Substring(8, 1));

        // If the waypoint is number 9, start back at 0
        if (waypoint.name.Equals("Waypoint9"))
        {
            pathFollower.GetComponent<ObjectProperties>().currentlySeeking = entityManager.waypoints["Waypoint0"];
        }
        else // alternatively, continue to the next one
        {
            pathFollower.GetComponent<ObjectProperties>().currentlySeeking = entityManager.waypoints["Waypoint" + (this.currentWaypoint + 1)];
        }
    }
    #endregion
}
