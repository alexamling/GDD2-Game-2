using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    // TODO: Scene management

    // TODO: Level controller

    // TODO: Unit controller
    public UnitContoller unitContoller;

    public GameObject playerPrefab;

    public float playerSpeed = 50.0f;
    public float zoomSpeed = 1.0f;
    public float minZoom = 5.0f;
    public float maxZoom = 45.0f;

    new private Camera camera;
    private Vector3 orthoRight;
    private Vector3 orthoUp;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        unitContoller = gameObject.GetComponent<UnitContoller>();
        camera = transform.GetComponentInChildren<Camera>();
        orthoRight = new Vector3(camera.transform.right.x, 0, camera.transform.right.z);
        orthoUp = new Vector3(camera.transform.up.x, 0, camera.transform.up.z);
    }

    void Update()
    {        
        ProcessInput();
    }


    private Vector2 viewPortPos;
    private float scroll;
    private Ray mouseRay;
    private RaycastHit rayHit;
    private bool hit;
    /// <summary>
    /// All direct player input is handled here
    /// if any other class reacts to player input, it should be invoked from here
    /// </summary>
    void ProcessInput()
    {
        // camera movement
        
        //viewPortPos = camera.ScreenToViewportPoint(Input.mousePosition);
        //if (viewPortPos.x > .9f) { transform.position += transform.right * Mathf.Clamp01((viewPortPos.x - .9f) * 10) * Time.deltaTime * camSpeed; }
        //if (viewPortPos.x < .1f) { transform.position += transform.right * Mathf.Clamp01((.1f - viewPortPos.x) * 10 ) * Time.deltaTime * -camSpeed; }
        //if (viewPortPos.y > .9f) { transform.position += orthoUp * Mathf.Clamp01((viewPortPos.y - .9f) * 10) * Time.deltaTime * camSpeed; }
        //if (viewPortPos.y < .1f) { transform.position += orthoUp * Mathf.Clamp01((.1f - viewPortPos.y) * 10) * Time.deltaTime * -camSpeed; }


        if (Input.GetKey(KeyCode.W)) { transform.position += orthoUp * Time.deltaTime * playerSpeed; }
        if (Input.GetKey(KeyCode.S)) { transform.position += orthoUp * Time.deltaTime * -playerSpeed; }
        if (Input.GetKey(KeyCode.A)) { transform.position += orthoRight * Time.deltaTime * -playerSpeed; }
        if (Input.GetKey(KeyCode.D)) { transform.position += orthoRight * Time.deltaTime * playerSpeed; }

        // zoom
        scroll = Input.mouseScrollDelta.y;
        camera.orthographicSize = Mathf.Clamp(-scroll * zoomSpeed + camera.orthographicSize, minZoom, maxZoom);


        // raycast to world
        mouseRay = camera.ScreenPointToRay(Input.mousePosition);
        hit = Physics.Raycast(mouseRay, out rayHit, 100.0f);
        
        // TODO: if hovering on unit, give info

        // if left mouse was just pressed
        // update initial click pos
        // UI raycast
        // or
        // read raycast
        // if you hit a unit or landmark select it


        // if left mouse is held down
        // compare to inital click pos - if {x} dist from point
        // click/drag to move camera?

        // if right mouse was just pressed
        if (Input.GetMouseButtonDown(0))
        {
            // read raycast
            if (hit)
            {
                if (rayHit.collider.gameObject.GetComponent<NavMeshSurface>())
                {
                    //unitContoller.player.PathTo(rayHit.point);
                    Debug.DrawLine(transform.position, rayHit.point);
                    Debug.Log("player path: " + rayHit.point);
                }
            }
            // if you hit an enemy unit or landmark, attack it
            // if you hit terrain, move towards it
        }

        unitContoller.UnitTest(unitContoller.player.transform.position);

        // space pressed
        // pause/unpause
        if (Input.GetKeyDown(KeyCode.Space))
        {
            unitContoller.SpawnUnits();
        }

        // TODO: all other hotkeys

        // Using B as a tester key, definitely subject to change
        if(Input.GetKeyDown(KeyCode.B))
        {
            if(hit)
            {
                if (rayHit.collider.gameObject.GetComponent<NavMeshSurface>())
                {
                    unitContoller.FindPath(rayHit.point);
                }
            }
        }

        // esc pressed
        // quit - TODO: pause menu
    }
}
