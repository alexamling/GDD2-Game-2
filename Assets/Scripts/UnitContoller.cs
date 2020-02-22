using System.Collections;
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
    public GameObject[] instances;
    public Matrix4x4[] transforms;
}
public class UnitContoller : MonoBehaviour
{
    // selected units
    // all player units - array of arraylists
    // player landmarks
    // enemy units - array of arraylists
    public UnitData[] enemyUnitData;
    // enemy landmarks

    void Start()
    {
        enemyUnitData = SetupUnitData(enemyUnitData);

        // TODO: temp fix for testing REMOVE THIS
        enemyUnitData[0].activeCount = enemyUnitData[0].maxCount;
        for (int i = 0; i < enemyUnitData[0].maxCount; i++)
        {
            enemyUnitData[0].instances[i].transform.position = new Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(10, 100), UnityEngine.Random.Range(-10, 10));
        }
    }

    private UnitData[] SetupUnitData(UnitData[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i].instances = new GameObject[data[i].maxCount];
            data[i].transforms = new Matrix4x4[data[i].maxCount];
            for (int j = 0; j < data[i].maxCount; j++)
            {
                GameObject instance = Instantiate(data[i].prefab);
                instance.transform.position = new Vector3(0, -1000, 0);
                data[i].instances[j] = instance;
                data[i].transforms[j] = Matrix4x4.TRS(instance.transform.position, instance.transform.rotation, instance.transform.localScale);
            }
        }
        return data;
    }

    void Update()
    {
        //TODO: instance render each type of unit and landmark
        UpdateUnitData(enemyUnitData);
        InstanceRenderer.Render(enemyUnitData[0].mesh, enemyUnitData[0].material, enemyUnitData[0].transforms, enemyUnitData[0].activeCount);
    }

    private void UpdateUnitData(UnitData[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data[i].maxCount; j++)
            {
                GameObject instance = data[i].instances[j];
                data[i].transforms[j] = Matrix4x4.TRS(instance.transform.position, instance.transform.rotation, instance.transform.localScale);
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
