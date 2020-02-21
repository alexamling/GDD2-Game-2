using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // TODO: Scene management

    // TODO: Level controller

    // TODO: Unit controller

    private Vector2 initialClickPos;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        ProcessInput();
    }

    /// <summary>
    /// All direct player input is handled here
    /// if any other class reacts to player input, it should be invoked from here
    /// </summary>
    void ProcessInput()
    {
        // raycast to world
            // TODO: if hovering on unit, give info

        // if left mouse was just pressed
            // update initial click pos
            // UI raycast
                // or
            // read raycast
                // if you hit a unit or landmark select it


        // if left mouse is held down
            // compare to inital click pos - if {x} dist from point
                // begin area selection

        // if right mouse was just pressed
            // read raycast
                // if you hit an enemy unit or landmark, attack it
                // if you hit terrain, ping it 
                    // LevelManager.Ping(pos)

        // space pressed
            // pause/unpause

        // esc pressed
            // quit - TODO: pause menu
    }
}
