using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-202.05 - Interactive Media Development
/// 
/// Project 5 - Flocking and Path Following
/// 
/// Class Description   : Draws debug lines and debug objects onto the game objects
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 8, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : December 11, 2018
/// Filename            : DebugLines.cs
/// 
/// </summary>

public class DebugLines : MonoBehaviour
{
    #region VARIABLES
    #region ENABLE OR DISABLE DEBUG LINES
    // Reference to Agent Manager
    protected GameObject masterManager;
    protected EntityManager entityManager;
    protected ObjectProperties objectProperties;
    #endregion

    #region PREFABS
    public GameObject futurePositionPrefab;             // Future position
    private GameObject futurePosition;
    #endregion

    #region TARGET OBJECT
    private GameObject targetObject;
    #endregion

    #region MATERIALS
    public Material debugBlue;                          // Right vector
    public Material debugGreen;                         // Forward vector
    public Material debugBlack;                         // Target
    #endregion

    #region OBJECT VECTORS
    private Vector3 direction;
    private Vector3 velocity;
    #endregion

    #region DEBUG VECTORS
    private Vector3 forwardVector;
    private Vector3 rightVector;
    private Vector3 futurePositionVector;
    #endregion
    #endregion

    #region AWAKE
    /// <summary>
    /// Runs before the first frame is displayed
    /// </summary>
    private void Awake()
    {
        this.futurePosition = Instantiate(futurePositionPrefab);
        this.futurePosition.transform.parent = gameObject.transform;
    }
    #endregion

    #region START
    private void Start()
    {
        this.masterManager = GameObject.FindGameObjectWithTag("Managers");
        this.entityManager = this.masterManager.GetComponentInChildren<EntityManager>();
        this.objectProperties = this.gameObject.GetComponent<ObjectProperties>();
    }
    #endregion

    #region ONRENDEROBJECT
    /// <summary>
    /// Runs when objects are being rendered to screen
    /// </summary>
    private void OnRenderObject()
    {
        if (this.entityManager.enableDebugLines)
        {
            if (!this.futurePosition.GetComponentInChildren<MeshRenderer>().enabled)
            {
                this.futurePosition.GetComponentInChildren<MeshRenderer>().enabled = true;
            }

            DrawDebugLines();
        }
        else
        {
            if (this.futurePosition.GetComponentInChildren<MeshRenderer>().enabled)
            {
                this.futurePosition.GetComponentInChildren<MeshRenderer>().enabled = false;
            }
        }
    }
    #endregion

    #region DRAW THE DEBUG LINES
    /// <summary>
    /// Draw the debug lines
    /// </summary>
    private void DrawDebugLines()
    {
        UpdateVariables();

        if (targetObject != null)
        {
            DrawTarget();
        }

        DrawFuturePosition();
        DrawForwardVector();
        DrawRightVector();
    }

    /// <summary>
    /// Update variables required to draw the debug lines
    /// </summary>
    private void UpdateVariables()
    {
        //this.direction = this.gameObject.GetComponent<ObjectProperties>().direction;
        //this.velocity = this.gameObject.GetComponent<ObjectProperties>().velocity;
        this.direction = this.objectProperties.direction;
        this.velocity = this.objectProperties.velocity;
    }

    /// <summary>
    /// Draw the projected future physical position of the current object using current velocity
    /// </summary>
    private void DrawFuturePosition()
    {
        this.futurePosition.transform.position = this.objectProperties.position + (Vector3.Normalize(this.velocity) * 10);

        // Only attempt to draw this object if it is enabled
        if (!this.futurePosition.GetComponent<MeshRenderer>().enabled)
        {
            this.futurePosition.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    /// <summary>
    /// Draw a line to the target of this object
    /// </summary>
    private void DrawTarget()
    {
        DrawLines("target", this.objectProperties.position, this.targetObject.transform.position);
    }

    /// <summary>
    /// Draw the forward vector
    /// </summary>
    private void DrawForwardVector() //Green
    {        
        this.forwardVector = (this.objectProperties.position + (this.direction * 3));

        DrawLines("forwardVector", this.objectProperties.position, this.forwardVector);
    }

    /// <summary>
    /// Draw the right vector
    /// </summary>
    private void DrawRightVector() //Blue
    {   
        this.rightVector = this.objectProperties.position + this.transform.right * 2;

        DrawLines("rightVector", this.objectProperties.position, this.rightVector);
    }

    /// <summary>
    /// Draw the actual lines to the display port
    /// </summary>
    /// <param name="line">Type of line to draw (target, forwardVector, rightVector)</param>
    /// <param name="origin">Origin vector of line</param>
    /// <param name="destination">Destination vector of line</param>
    private void DrawLines(string line, Vector3 origin, Vector3 destination)
    {
        switch (line)
        {
            case "target":
                debugBlack.SetPass(0);

                break;
            case "forwardVector":
                debugGreen.SetPass(0);

                break;
            case "rightVector":
                debugBlue.SetPass(0);

                break;
        }

        GL.Begin(GL.LINES);
        GL.Vertex(origin);
        GL.Vertex(destination);
        GL.End();
    }
    #endregion
}
