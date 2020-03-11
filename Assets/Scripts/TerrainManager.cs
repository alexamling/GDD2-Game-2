using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-202.05 - Interactive Media Development
/// 
/// Project 5 - Flocking and Path Following
/// 
/// Class Description   : This is a manager object that takes care of terrain related operations
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 8, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : December 11, 2018
/// Filename            : TerrainManager.cs
/// 
/// </summary>

public class TerrainManager : MonoBehaviour
{
    #region TERRAIN SPACE CALCULATION
    // Terrain object    
    private Terrain flatTerrain;

    // Terrain dimension vectors
    public Vector3 flatTerrainDimensions;

    public Vector3 terrainCubeSize;
    public Vector3 terrainCubeMin;
    public Vector3 terrainCubeMax;
    public Vector3 terrainCubeCenter;
    #endregion

    #region AWAKE
    // Use this for initialization
    private void Awake()
    {
        this.flatTerrain = Terrain.activeTerrain;
        this.flatTerrainDimensions = flatTerrain.terrainData.size;

        // Defines the size of the cube
        this.terrainCubeSize = new Vector3(100f, 100f, 100f);

        // Defines the minimum extents of the cube
        this.terrainCubeMin = new Vector3(
            -(this.terrainCubeSize.x / 2),
            -(this.terrainCubeSize.y / 2),
            -(this.terrainCubeSize.z / 2)
        );

        // Defines the maximum extents of the cube
        this.terrainCubeMax = new Vector3(
            (this.terrainCubeSize.x / 2),
            (this.terrainCubeSize.y / 2),
            (this.terrainCubeSize.z / 2)
        );

        this.terrainCubeCenter = new Vector3(0f, 0f, 0f);
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
}
