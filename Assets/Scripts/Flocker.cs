﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-320.01 - Game Design and Development 2
/// 
/// Project 2
/// 
/// Class Description   : Prefab script attached to the flocker object
/// Created By          : Benjamin Kleynhans
/// Date Created        : December 9, 2018
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : March 26, 2020
/// Filename            : Flocker.cs
/// 
/// </summary>

public class Flocker : Entity
{
    #region START
    // Use this for initialization
    new void Start()
    {
        base.Start();
        this.objectProperties = this.gameObject.GetComponent<ObjectProperties>();

        this.objectProperties.currentlySeeking = entityManager.allObjects["Player"];
    }
    #endregion

    #region UPDATE
    // Update is called once per frame
    new void Update()
    {
        base.Update();        
    }
    #endregion
}
