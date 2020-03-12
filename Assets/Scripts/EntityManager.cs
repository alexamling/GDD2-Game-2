﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-202.05 - Interactive Media Development
/// 
/// Project 5 - Flocking and Path Following
/// 
/// Class Description   : Creates and manages agents
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 8, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : December 11, 2018
/// Filename            : EntityManager.cs
/// 
/// </summary>

public class EntityManager : MonoBehaviour
{
    #region VARIABLES
    #region PREFABS
    // Objects / Prefabs
    public GameObject playerPrefab;
    public GameObject flockerPrefab;
    //public GameObject pathFollowerPrefab;
    //public GameObject waypointPrefab;
    //public GameObject enemyPrefab;
    //public GameObject groundObstaclePrefab;
    //public GameObject floatingObstacle0Prefab;
    //public GameObject floatingObstacle1Prefab;
    //public GameObject floatingObstacle2Prefab;
    #endregion

    #region GAME OBJECTS
    protected GameObject managers;
    protected TerrainManager terrainManager;
    private CollisionDetection collisionDetection;
    #endregion

    #region LISTS AND DICTIONARIES
    // Individual object lists
    // Dictionaries per object type instantiated in game
    //public Dictionary<string, GameObject> psgs = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> flockers = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> pathFollowers = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> waypoints = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> floatingObstacles = new Dictionary<string, GameObject>();

    // Dictionary containing all objects instantiated in the game view
    public Dictionary<string, GameObject> allObjects = new Dictionary<string, GameObject>();

    // List of waypoints used by path follower
    private List<Vector3> waypointPositions = new List<Vector3>();
    #endregion

    #region NEW OBJECT INSTANTIATION VARIABLE
    // New object
    private GameObject newObject;
    private Vector3 newObjectExtents;
    private Vector3 instantiationPosition;
    private bool validPosition;
    private int maxLoops;
    private MeshRenderer newObjectMesh;
    private MeshRenderer dictObjectMesh;
    #endregion

    #region QUANTITIES OF OBJECTS TO INSTANTIATE
    // Predefined quantities of objects to instantiate
    int nrOfFlockers;
    int nrOfPathFollowers;
    int nrOfWaypoints;
    int nrOfEnemies;
    int nrOfObstacles;
    #endregion

    #region DEFAULT INSTANTIATION PROPERTIES OF OBJECTS
    // flocker default properties
    private float flockerMass;
    private float flockerMaxSpeed;

    // wanderer default properties
    private Vector3 pathFollowerInstantiationPosition;
    private float pathFollowerMass;
    private float pathFollowerMaxSpeed;

    // Debug lines properties
    public bool enableDebugLines;
    #endregion
    #endregion

    #region AWAKE
    /// <summary>
    /// Get properties required for screen orientation and bounds
    /// </summary>
    private void Awake()
    {
        // Get access to all objects in game through managers object
        managers = GameObject.FindGameObjectWithTag("Managers");

        terrainManager = managers.GetComponentInChildren<TerrainManager>();
        this.collisionDetection = gameObject.GetComponent<CollisionDetection>();

        // Set initial debug line value
        this.enableDebugLines = true;

        // Add follower waypoints
        this.waypointPositions.Add(new Vector3(32f, -48f, 31f));
        this.waypointPositions.Add(new Vector3(26f, -48f, 10f));
        this.waypointPositions.Add(new Vector3(37f, -48f, -7f));
        this.waypointPositions.Add(new Vector3(38f, -48f, -29f));
        this.waypointPositions.Add(new Vector3(11f, -48f, -38f));
        this.waypointPositions.Add(new Vector3(-12f, -48f, -27f));
        this.waypointPositions.Add(new Vector3(-8f, -48f, -4f));
        this.waypointPositions.Add(new Vector3(-13f, -48f, 21f));
        this.waypointPositions.Add(new Vector3(6f, -48f, 37f));
        this.waypointPositions.Add(new Vector3(22f, -48f, 40f));

        // Set the quantities and values of objects to instantiate
        this.nrOfFlockers = 50; // Editible in Inspector
        //this.nrOfPathFollowers = 1; // No path followers used
        //this.nrOfWaypoints = this.waypointPositions.Count; // No Waypoints used

        // Set instantiation properties of objects        
        this.flockerMass = 3f;
        this.flockerMaxSpeed = 5f;

        // Set instantiation properties for path follower
        this.pathFollowerInstantiationPosition = new Vector3(30f, -48f, 40f);
        this.pathFollowerMass = 3f;
        this.pathFollowerMaxSpeed = 5f;
    }
    #endregion

    #region START
    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        CreateObjects();
    }
    #endregion

    #region UPDATE
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Enable or disable debug lines
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.enableDebugLines = !this.enableDebugLines;
        }
    }
    #endregion

    #region OBJECT INSTANTIATION
    /// <summary>
    /// Create the objects
    /// </summary>
    private void CreateObjects()
    {
        Vector3 instantiationPosition = Vector3.zero;

        CreatePlayer(playerPrefab);

        //CreateFloatingObstacle(floatingObstacle0Prefab);
        //CreateFloatingObstacle(floatingObstacle1Prefab);
        //CreateFloatingObstacle(floatingObstacle2Prefab);

        // Create flockers
        for (int i = 0; i < nrOfFlockers; i++)
        {
            CreateFlocker(flockerPrefab);
        }

        //// Create pathFollowers
        //for (int i = 0; i < nrOfPathFollowers; i++)
        //{
        //    CreatePathFollower(pathFollowerPrefab);
        //}

        //// Create waypoints
        //for (int i = 0; i < nrOfWaypoints; i++)
        //{
        //    CreateWaypoint(waypointPrefab, waypointPositions[i]);
        //}
    }

    #region PLAYER INSTANTIATION
    /// <summary>
    /// Instantiates a player prefab on the terrain
    /// </summary>
    /// <param name="player">Sphere of tremendousness or something. It's what they chase</param>
    private void CreatePlayer(GameObject player)
    {
        // Specifies the position where the player character will be instantiated
        Vector3 playerPosition = new Vector3(-24f, 1f, -37f);

        // Instantiate a new player
        this.newObject = Instantiate(player, playerPosition, Quaternion.Euler(0, 0, 0));
        // Assign the object to the appropriate location in the Hierarchy
        // This refers to the Hierarchy as specified inside the scene in Unity. i.e. the player will be
        // created in Level1 -> Managers -> EntityManager -> Player
        this.newObject.transform.parent = GameObject.Find("Player").transform;
        // Get the prefab name of the player
        this.newObject.name = "Player";

        //Set player specific properties
        this.newObject.GetComponent<ObjectProperties>().seeker = false;
        this.newObject.GetComponent<ObjectProperties>().fleer = false;
        this.newObject.GetComponent<ObjectProperties>().mass = 0f;
        this.newObject.GetComponent<ObjectProperties>().maxSpeed = 0f;
        this.newObject.GetComponent<ObjectProperties>().motion = MovementState.None;
        this.newObject.GetComponent<ObjectProperties>().position = this.newObject.transform.position;

        // Add the new object to the appropriate lists
        allObjects.Add(this.newObject.name, this.newObject);
    }
    #endregion

    #region FLOCKER INSTANTIATION
    /// <summary>
    /// Instantiates a flocker prefab on the terrain
    /// </summary>
    /// <param name="flocker">Human gameobject</param>
    private void CreateFlocker(GameObject flocker)
    {
        // Instantiate a new monster
        this.newObject = Instantiate(flocker, InstantiationPosition(flocker), Quaternion.Euler(0, 0, 0));
        // Assign the object to the appropriate location in the Hierarchy
        // This refers to the Hierarchy as specified inside the scene in Unity. i.e. the player will be
        // created in Level1 -> Managers -> EntityManager -> EnemyMinions
        this.newObject.transform.parent = GameObject.Find("EnemyMinions").transform;
        // Get the prefab name of the object
        this.newObject.name = this.newObject.name.Substring(0, this.newObject.name.IndexOf("(")) + flockers.Count;

        //Set object specific properties
        this.newObject.GetComponent<ObjectProperties>().seeker = true;
        this.newObject.GetComponent<ObjectProperties>().fleer = true;
        this.newObject.GetComponent<ObjectProperties>().mass = this.flockerMass;
        this.newObject.GetComponent<ObjectProperties>().maxSpeed = this.flockerMaxSpeed;
        this.newObject.GetComponent<ObjectProperties>().motion = MovementState.Seeking;
        this.newObject.GetComponent<ObjectProperties>().position = this.newObject.transform.position;

        // Add the new object to the appropriate lists
        flockers.Add(this.newObject.name, this.newObject);
        allObjects.Add(this.newObject.name, this.newObject);
    }
    #endregion

    #region PATH FOLLOWER INSTANTIATION
    /// <summary>
    /// Instantiates a pathFollower prefab on the terrain
    /// </summary>
    /// <param name="pathFollower">Path follower gameobject</param>
    private void CreatePathFollower(GameObject pathFollower)
    {
        //// Instantiate a new monster
        this.newObject = Instantiate(pathFollower, this.pathFollowerInstantiationPosition, Quaternion.Euler(0, 0, 0));
        // Assign the object to the appropriate location in the Hierarchy
        this.newObject.transform.parent = GameObject.Find("PathFollowers").transform;
        // Get the prefab name of the object
        this.newObject.name = this.newObject.name.Substring(0, this.newObject.name.IndexOf("(")) + pathFollowers.Count;

        // Set object specific properties                
        this.newObject.GetComponent<ObjectProperties>().seeker = true;
        this.newObject.GetComponent<ObjectProperties>().fleer = false;
        this.newObject.GetComponent<ObjectProperties>().mass = this.pathFollowerMass;
        this.newObject.GetComponent<ObjectProperties>().maxSpeed = this.pathFollowerMaxSpeed;
        this.newObject.GetComponent<ObjectProperties>().motion = MovementState.Seeking;
        this.newObject.GetComponent<ObjectProperties>().position = this.newObject.transform.position;

        // Add the new object to the appropriate lists
        pathFollowers.Add(this.newObject.name, this.newObject);
        allObjects.Add(this.newObject.name, this.newObject);
    }
    #endregion

    #region WAYPOINT INSTANTIATION
    /// <summary>
    /// Instantiates a waypoint prefab on the terrain
    /// </summary>
    /// <param name="waypoint">waypoint gameobject</param>
    /// <param name="waypointPosition">waypoint gameobject position of instantiation</param>
    private void CreateWaypoint(GameObject waypoint, Vector3 waypointPosition)
    {
        //// Instantiate a new monster
        this.newObject = Instantiate(waypoint, waypointPosition, Quaternion.Euler(0, 0, 0));
        // Assign the object to the appropriate location in the Hierarchy
        this.newObject.transform.parent = GameObject.Find("Waypoints").transform;
        // Get the prefab name of the object
        this.newObject.name = this.newObject.name.Substring(0, this.newObject.name.IndexOf("(")) + waypoints.Count;

        // Set object specific properties                
        this.newObject.GetComponent<ObjectProperties>().seeker = false;
        this.newObject.GetComponent<ObjectProperties>().fleer = false;
        this.newObject.GetComponent<ObjectProperties>().mass = 0f;
        this.newObject.GetComponent<ObjectProperties>().maxSpeed = 0f;
        this.newObject.GetComponent<ObjectProperties>().motion = MovementState.None;
        this.newObject.GetComponent<ObjectProperties>().position = this.newObject.transform.position;

        // Add the new object to the appropriate lists
        waypoints.Add(this.newObject.name, this.newObject);
        allObjects.Add(this.newObject.name, this.newObject);
    }
    #endregion

    #region FLOATING OBSTACLE INSTANTIATION
    /// <summary>
    /// Instantiates a floating obstacle prefab on the terrain
    /// </summary>
    /// <param name="obstacle">floating gameobject</param>
    private void CreateFloatingObstacle(GameObject obstacle)
    {
        for (int i = 0; i < 30; i++)
        {
            //// Instantiate a new monster
            this.newObject = Instantiate(obstacle, InstantiationPosition(obstacle), Quaternion.Euler(0, 0, 0));
            // Assign the object to the appropriate location in the Hierarchy
            this.newObject.transform.parent = GameObject.Find("Obstacles").transform;
            // Get the prefab name of the object
            this.newObject.name = obstacle.name + i;

            // Set object specific properties                
            this.newObject.GetComponent<ObjectProperties>().seeker = false;
            this.newObject.GetComponent<ObjectProperties>().fleer = false;
            this.newObject.GetComponent<ObjectProperties>().mass = 0f;
            this.newObject.GetComponent<ObjectProperties>().maxSpeed = 0f;
            this.newObject.GetComponent<ObjectProperties>().motion = MovementState.None;
            this.newObject.GetComponent<ObjectProperties>().position = this.newObject.transform.position;

            // Add the new object to the appropriate lists
            floatingObstacles.Add(this.newObject.name, this.newObject);
            allObjects.Add(this.newObject.name, this.newObject);
        }
    }
    #endregion

    #region GENERATE RANDOM INSTANTIATION POSITION
    /// <summary>
    /// Generates a random position for object instantiation within the confines of the terrain
    /// </summary>
    /// <param name="this.newObject">GameObject requiring the instantiation position</param>
    /// <returns></returns>
    private Vector3 InstantiationPosition(GameObject createObject)
    {
        // Get extents of new object being created
        this.newObjectExtents = createObject.GetComponentInChildren<ObjectProperties>().bounds.extents;
        this.newObjectExtents = Vector3.Scale(this.newObjectExtents, createObject.transform.localScale);

        // Create a mesh to use for instantiation testing
        this.newObjectMesh = createObject.GetComponent<MeshRenderer>();
        this.dictObjectMesh = new MeshRenderer();

        this.instantiationPosition = Vector3.zero;
        this.validPosition = true;

        this.maxLoops = 10;

        // Generate a random instantiation location and test if it overlaps with an existing obect in the game view
        do
        {
            this.validPosition = true;

            // Generate random location
            this.instantiationPosition = new Vector3(
                Random.Range(
                    (terrainManager.terrainCubeMin.x + createObject.GetComponent<ObjectProperties>().bounds.extents.x),
                    (terrainManager.terrainCubeMax.x - createObject.GetComponent<ObjectProperties>().bounds.extents.x)
                ),
                Random.Range(
                    (terrainManager.terrainCubeMin.y + createObject.GetComponent<ObjectProperties>().bounds.extents.y + 30f),
                    (terrainManager.terrainCubeMax.y - createObject.GetComponent<ObjectProperties>().bounds.extents.y - 5f)
                ),
                Random.Range(
                    (terrainManager.terrainCubeMin.z + createObject.GetComponent<ObjectProperties>().bounds.extents.z),
                    (terrainManager.terrainCubeMax.z - createObject.GetComponent<ObjectProperties>().bounds.extents.z)
                ));

            if (allObjects.Count == 0)
            {
                this.validPosition = true;
            }
            else
            {   // Compare new location with existing objects
                foreach (KeyValuePair<string, GameObject> obj in allObjects)
                {
                    this.dictObjectMesh = obj.Value.GetComponent<MeshRenderer>();

                    if (collisionDetection.AABBCollision(this.instantiationPosition,
                                                        this.newObjectMesh,
                                                        obj.Value.GetComponent<ObjectProperties>().position,
                                                        this.dictObjectMesh))
                    {
                        this.validPosition = false;
                        break;
                    }
                }
            }

            this.maxLoops--;

            if (this.maxLoops == 0)
            {
                break;
            }

        } while (!this.validPosition);
        
        // Return valid instantiation location
        return this.instantiationPosition;
    }
    #endregion
    #endregion
}