using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct InstanceData
{
    [SerializeField]
    Mesh mesh;
    [SerializeField]
    GameObject gameObject;
}
public class AssetManager : MonoBehaviour
{
    // arr of player unit prefabs
    // arr of player landmark prefabs
    // arr of enemy unit prefabs
    public InstanceData[] unitData;
    // arr of enemy landmark prefabs
}
