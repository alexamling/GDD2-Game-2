using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-320.01 - Game Design and Development 2
/// 
/// Project 2
/// 
/// Class Description   : Manages the flocks by getting min, max and center values.  Also provides alignment and velocity information.
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 8, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : March 26, 2020
/// Filename            : FlockManager.cs
/// 
/// </summary>

public class FlockManager : MonoBehaviour
{
    #region VARIABLES
    #region GAME OBJECTS
    // Reference to Agent Manager    
    protected GameObject masterManager;
    protected EntityManager entityManager;
    protected TerrainManager terrainManager;
    protected CameraManager cameraManager;
    #endregion

    #region DICTIONARIES
    // Keep track of min, max and center values of flocks
    public Dictionary<string, Vector3> flockingMinValues = new Dictionary<string, Vector3>();
    public Dictionary<string, Vector3> flockingMaxValues = new Dictionary<string, Vector3>();
    public Dictionary<string, Vector3> flockingCenterValues = new Dictionary<string, Vector3>();
    public Dictionary<string, Vector3> alignmentDirection = new Dictionary<string, Vector3>();    
    public Dictionary<string, Vector3> flockingAverageVelocity = new Dictionary<string, Vector3>();
    
    public Dictionary<string, float> maxVelocity = new Dictionary<string, float>();
    #endregion

    #region VECTOR3
    // Variables used when needing to assign temporary vector values
    public Vector3 tempVector;
    #endregion

    #region INT
    int flockCounter;
    #endregion
    #endregion

    #region AWAKE
    /// <summary>
    /// Used for initialization
    /// </summary>
    private void Awake()
    {   
        // Connect to master manager
        masterManager = GameObject.FindGameObjectWithTag("Managers");
    }
    #endregion

    #region START
    /// <summary>
    /// Also used this for initialization
    /// </summary>
    void Start()
    {
        // Connect to other managers
        entityManager = masterManager.GetComponentInChildren<EntityManager>();
        terrainManager = masterManager.GetComponentInChildren<TerrainManager>();
        cameraManager = masterManager.GetComponentInChildren<CameraManager>();
    }
    #endregion

    #region UPDATE
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Reset variables
        this.flockingMinValues["Flocker"] = new Vector3(999.99f, 999.99f, 999.99f);
        this.flockingMaxValues["Flocker"] = new Vector3(-999.99f, -999.99f, -999.99f);
        this.maxVelocity["Flocker"] = 0.0f;
        this.flockCounter = 0;

        CalculateCohesion();
        UpdateFlockingVariables();
    }
    #endregion

    #region FLOCKING CALCULATION
    /// <summary>
    /// Perform flocking calculation
    /// </summary>
    private void CalculateCohesion()
    {
        this.alignmentDirection["Flocker"] = Vector3.zero;
        this.flockingAverageVelocity["Flocker"] = Vector3.zero;

        if (entityManager.flockers.Count > 0)
        {
            // Loop through all objects on the terrain
            int FlockerCounter = 0;

            foreach (KeyValuePair<string, GameObject> obj in entityManager.allObjects)
            {
                if (obj.Key.Contains("Flocker"))
                {
                    this.flockCounter++;
                    this.alignmentDirection["Flocker"] += obj.Value.GetComponent<ObjectProperties>().direction;
                    this.flockingAverageVelocity["Flocker"] += obj.Value.GetComponent<ObjectProperties>().velocity;

                    if (!this.flockingMinValues.ContainsKey("Flocker"))
                    {
                        // If there is no flocker in the list, add this flocker
                        this.flockingMinValues.Add("Flocker", obj.Value.GetComponent<ObjectProperties>().position);
                        this.flockingMaxValues.Add("Flocker", obj.Value.GetComponent<ObjectProperties>().position);
                        this.maxVelocity.Add("Flocker", obj.Value.GetComponent<ObjectProperties>().maxSpeed);
                        this.flockingCenterValues.Add("Flocker", Vector3.zero);
                    }
                    else
                    {
                        // If there is a flocker in the list, update the values
                        this.GetSmallestExtents("Flocker", obj.Value);
                        this.GetLargestExtents("Flocker", obj.Value);

                        if (!this.maxVelocity.ContainsKey("Flocker"))
                        {
                            this.maxVelocity.Add("Flocker", obj.Value.GetComponent<ObjectProperties>().maxSpeed);
                        }
                        else
                        {
                            this.maxVelocity["Flocker"] += obj.Value.GetComponent<ObjectProperties>().maxSpeed; 
                        }
                        //this.maxVelocity["Flocker"] += obj.Value.GetComponent<ObjectProperties>().maxSpeed;
                    }

                    FlockerCounter++;
                }
            }

            // If there is more than 1 flocker in the list, calculate flocking for flockers
            if (FlockerCounter > 1)
            {
                this.flockingCenterValues["Flocker"] = GetFlockCenter("Flocker");
            }
            else if (FlockerCounter != 0)
            {
                this.flockingCenterValues["Flocker"] = this.flockingMinValues["Flocker"];
            }

            this.alignmentDirection["Flocker"] = this.alignmentDirection["Flocker"].normalized;

            this.maxVelocity["Flocker"] = this.maxVelocity["Flocker"] / this.flockCounter;

            this.flockingAverageVelocity["Flocker"] = this.flockingAverageVelocity["Flocker"].normalized * this.maxVelocity["Flocker"];
        }
    }

    #region UPDATE FLOCKING VARIABLES
    /// <summary>
    /// Apply the appropriate flocking position to all objects
    /// </summary>
    private void UpdateFlockingVariables()
    {
        foreach (KeyValuePair<string, GameObject> flocker in entityManager.flockers)
        {
            flocker.Value.GetComponent<ObjectProperties>().flockCenter = this.flockingCenterValues["Flocker"];
            flocker.Value.GetComponent<ObjectProperties>().flockDirection = this.alignmentDirection["Flocker"];
        }
    }
    #endregion

    #region DETERMINE FLOCK CENTER
    /// <summary>
    /// Calculate the center of the flock
    /// </summary>
    /// <param name="objType">Specify which object it needs to be calculated for (flocker / zombie)</param>
    /// <returns>Vector containing the center of the flock</returns>
    private Vector3 GetFlockCenter(string objType)
    {
        this.tempVector = Vector3.zero;

        this.tempVector.x = ((flockingMaxValues[objType].x - flockingMinValues[objType].x) / 2) + flockingMinValues[objType].x;
        this.tempVector.y = ((flockingMaxValues[objType].y - flockingMinValues[objType].y) / 2) + flockingMinValues[objType].y;
        this.tempVector.z = ((flockingMaxValues[objType].z - flockingMinValues[objType].z) / 2) + flockingMinValues[objType].z;

        return this.tempVector;
    }
    #endregion

    #region DETERMINE FLOCK EXTENTS
    /// <summary>
    /// Get the smallest extents of the flock
    /// </summary>
    /// <param name="objType">Type of objects (flocker / zombie)</param>
    /// <param name="obj">Object being tested against</param>
    private void GetSmallestExtents(string objType, GameObject obj)
    {
        if (Vector3.Distance(terrainManager.terrainCubeMin, obj.transform.position) < (Vector3.Distance(terrainManager.terrainCubeMin, this.flockingMinValues[objType])))
        {
            this.flockingMinValues[objType] = obj.transform.position;
        }
    }

    /// <summary>
    /// Get the largest extents of the flock
    /// </summary>
    /// <param name="objType">Type of objects (flocker / zombie)</param>
    /// <param name="obj">Object being tested against</param>
    private void GetLargestExtents(string objType, GameObject obj)
    {
        if (Vector3.Distance(obj.transform.position, terrainManager.terrainCubeMax) < Vector3.Distance(this.flockingMaxValues[objType], terrainManager.terrainCubeMax))
        {
            this.flockingMaxValues[objType] = obj.transform.position;
        }

        this.tempVector = Vector3.zero;
    }
    #endregion
    #endregion
}
