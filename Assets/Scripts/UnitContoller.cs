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
    public Unit[] instances;
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
            enemyUnitData[a].activeCount = enemyUnitData[a].maxCount;
            for (int i = 0; i < enemyUnitData[a].maxCount; i++)
            {
                enemyUnitData[a].instances[i].transform.position = new Vector3(UnityEngine.Random.Range(-100, 100),  UnityEngine.Random.Range(-100, 100));
            }

        }

    }

    private UnitData[] SetupUnitData(UnitData[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i].instances = new Unit[data[i].maxCount];
            data[i].transforms = new Matrix4x4[data[i].maxCount];
            for (int j = 0; j < data[i].maxCount; j++)
            {
                Unit instance = Instantiate(data[i].prefab.GetComponent<Unit>());
                enemyNav.Units.Add(instance.GetComponent<Unit>());
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
        for (int i = 0; i < enemyUnitData.Length; i++)
        {
            InstanceRenderer.Render(enemyUnitData[i].mesh, enemyUnitData[i].material, enemyUnitData[i].transforms, enemyUnitData[i].activeCount);
        }
    }

    private void UpdateUnitData(UnitData[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data[i].maxCount; j++)
            {
                Unit instance = data[i].instances[j];
                data[i].transforms[j] = Matrix4x4.TRS(instance.transform.position, instance.transform.rotation, instance.transform.localScale);
            }
        }
    }

    public void UnitTest(Vector3 target)
    {

        for (int i = 0; i < enemyUnitData.Length; i++)
        {
            for (int j = 0; j < enemyUnitData[i].maxCount; j++)
            {
                enemyUnitData[i].instances[j].PathTo(target);
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
