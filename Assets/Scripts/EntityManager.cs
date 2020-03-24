using System.Collections;
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
        //for (int i = 0; i < nrOfFlockers; i++)
        //{
        //    CreateFlocker(flockerPrefab);
        //}
        //
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
        //this.newObject.GetComponent<ObjectProperties>().position = this.newObject.transform.position;

        // Add the new object to the appropriate lists
        allObjects.Add(this.newObject.name, this.newObject);
    }
    #endregion

   
   

    

    #region GENERATE RANDOM INSTANTIATION POSITION
    /// <summary>
    /// Generates a random position for object instantiation within the confines of the terrain
    /// </summary>
    /// <param name="this.newObject">GameObject requiring the instantiation position</param>
    /// <returns></returns>
   
    #endregion
    #endregion
}