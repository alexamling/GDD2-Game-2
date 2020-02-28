﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct UnitData
{
    [SerializeField]
    public Mesh mesh;
    [SerializeField]
    public int maxCount;
    [SerializeField]
    public GameObject prefab;
    public int activeCount;
    public Material material;
    public GameObject activeInstances;
    public GameObject inactiveInstances;
    //public Unit[] instances;
    public Matrix4x4[] transforms;
}
public class UnitContoller : MonoBehaviour
{
    public Player player;
    // selected units
    // all player units - array of arraylistsn
    // player landmarks
    // enemy units - array of arraylists
    public UnitData[] enemyUnitData;
    // enemy landmarks

    public EnemyNav enemyNav;
    public GameObject navControllerPrefab;
    public GameObject enemyNavController;


    void Start()
    {

        enemyNavController = Instantiate(navControllerPrefab);
        enemyNav = enemyNavController.GetComponent<EnemyNav>();
        enemyUnitData = SetupUnitData(enemyUnitData);

        // TODO: temp fix for testing REMOVE THIS
        for (int a = 0; a < enemyUnitData.Length; a++)
        {
            for (int i = 0; i < enemyUnitData[a].activeInstances.transform.childCount; i++)
            {
                enemyUnitData[a].activeInstances.transform.GetChild(i).transform.position = new Vector3(UnityEngine.Random.Range(-100, 100), UnityEngine.Random.Range(10, 100), UnityEngine.Random.Range(-100, 100));
                enemyUnitData[a].activeCount += 1;
            }

        }

    }

    private UnitData[] SetupUnitData(UnitData[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i].activeInstances = new GameObject();
            data[i].inactiveInstances = new GameObject();

            data[i].transforms = new Matrix4x4[data[i].maxCount];
            for (int j = 0; j < data[i].maxCount; j++)
            {
                Unit instance = Instantiate(data[i].prefab.GetComponent<Unit>());
                enemyNav.Units.Add(instance.GetComponent<Unit>());
                instance.transform.position = new Vector3(0, -1000, 0);
                // TODO swap this out: instance.transform.parent = enemyUnitData[i].inactiveInstances.transform;
                instance.transform.parent = enemyUnitData[i].activeInstances.transform;
                data[i].transforms[j] = Matrix4x4.TRS(instance.transform.position, instance.transform.rotation, instance.transform.localScale);
            }
        }
        return data;
    }

    void Update()
    {
        //TODO: instance render each type of unit and landmark
        UpdateUnitData(enemyUnitData);
        for (int i = 0; i < enemyUnitData.Length; i++)
        {
            InstanceRenderer.Render(enemyUnitData[i].mesh, enemyUnitData[i].material, enemyUnitData[i].transforms, enemyUnitData[i].activeCount);
        }
    }

    private void UpdateUnitData(UnitData[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; i < data[i].activeInstances.transform.childCount; j++)
            {
                Transform instanceTransform = data[i].activeInstances.transform.GetChild(i);
                data[i].transforms[j] = Matrix4x4.TRS(instanceTransform.position, instanceTransform.rotation, instanceTransform.localScale);
            }
        }
    }

    public void UnitTest(Vector3 target)
    {

        for (int i = 0; i < enemyUnitData.Length; i++)
        {
            for (int j = 0; i < enemyUnitData[i].activeInstances.transform.childCount; j++)
            {
                Unit instance = enemyUnitData[i].activeInstances.transform.GetChild(i).GetComponent<Unit>();
                instance.PathTo(target);
            }
        }
    }

    void DeselectUnits()
    {

    }

    void ToggleUnitSelection(ISelectable unit)
    {
    
    }

    void SpawnUnits(/*TODO: input values*/)
    {

    }
}
