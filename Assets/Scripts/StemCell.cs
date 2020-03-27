using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// IGME-320.01 - Game Design and Development 2
/// 
/// Project 2
/// 
/// Class Description   : Stem cell script attached to the stem cell unit
/// Created By          : Benjamin Kleynhans
/// Date Created        : March 26, 2020
/// Last Modified By    : Benjamin Kleynhans
/// Date Modified       : March 26, 2020
/// Filename            : StemCell.cs
/// 
/// </summary>

public class StemCell : Entity
{
    #region START
    // Use this for initialization
    new void Start()
    {
        base.Start();
        //this.objectProperties = this.gameObject.GetComponent<ObjectProperties>();

        //this.objectProperties.currentlySeeking = entityManager.allObjects["Player"];
    }
    #endregion

    #region UPDATE
    // Update is called once per frame
    new void Update()
    {
        //base.Update();        
    }
    #endregion
}
