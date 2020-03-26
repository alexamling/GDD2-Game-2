using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-320.01 - Game Design and Development 2
/// 
/// Project 2
/// 
/// Class Description   : Manages the switching between cameras in game
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 8, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : March 26, 2020
/// Filename            : CameraManager.cs
/// 
/// </summary>

public class CameraManager : MonoBehaviour
{
    #region VARIABLES
    #region GAME OBJECTS
    // References to game objects
    private GameObject managers;
    protected EntityManager entityManager;
    private FlockManager flockManager;
    #endregion

    #region CAMERAS    
    //References to Cameras
    public Camera camera1;
    public Camera camera2;

    // Manages active camera
    public Camera currentActiveCamera;
    public Transform playerObject;
    #endregion
    #endregion

    #region AWAKE
    /// <summary>
    /// Use this for initialization
    /// </summary>
    protected void Awake()
    {
        // Access the agent manager object
        this.managers = GameObject.FindGameObjectWithTag("Managers");
    }
    #endregion

    #region START
    /// <summary>
    /// Use this for initialization also
    /// </summary>
    protected void Start()
    {
        this.entityManager = managers.GetComponentInChildren<EntityManager>();
        this.flockManager = managers.GetComponentInChildren<FlockManager>();

        //this.playerObject = GameObject.Find("Player").transform;

        this.camera1.enabled = true;
        this.camera2.enabled = false;

        this.currentActiveCamera = this.camera1;
    }
    #endregion

    #region UPDATE
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected void Update()
    {
        // Change the camera
        ChangeCamera();

        UpdateCamera();
    }
    #endregion

    #region CHANGE CAMERA
    /// <summary>
    /// Perform the camera change
    /// </summary>
    private void ChangeCamera()
    {
        if ((Input.anyKey) && (!Input.GetKeyDown(KeyCode.D)))
        {
            DisableActiveCamera();

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))        // Change to main camra
            {
                this.currentActiveCamera = camera1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))   // Change to flock center camera
            {
                this.currentActiveCamera = camera2;
            }

            EnableNewCamera();
        }
    }

    /// <summary>
    /// Disables the active camera
    /// </summary>
    private void DisableActiveCamera()
    {
        this.currentActiveCamera.enabled = false;
    }

    /// <summary>
    /// Enables the new camera
    /// </summary>
    private void EnableNewCamera()
    {
        this.currentActiveCamera.enabled = true;
    }

    /// <summary>
    /// Updates the position and direction of the smoothfollow camera
    /// </summary>
    private void UpdateCamera()
    {
        if (flockManager.alignmentDirection.ContainsKey("Flocker"))
        {
            this.playerObject.GetComponent<ObjectProperties>().direction = flockManager.alignmentDirection["Flocker"];

            if (this.currentActiveCamera == this.camera2)
            {
                this.playerObject.GetComponent<ObjectProperties>().position = flockManager.flockingCenterValues["Flocker"];
            }

            this.playerObject.transform.position = this.playerObject.GetComponent<ObjectProperties>().position;
            this.playerObject.transform.rotation = Quaternion.Euler(this.playerObject.GetComponent<ObjectProperties>().direction) * this.playerObject.transform.rotation;
        }
    }
    #endregion
}
