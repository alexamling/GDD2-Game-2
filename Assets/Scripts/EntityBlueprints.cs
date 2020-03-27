using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum MovementState
{
    BoundaryContact,
    Evading,
    Fleeing,
    Leading,
    Pursueing,
    Seeking,
    Wandering,
    None
}

public class EntityBlueprints : MonoBehaviour
{
    #region VARIABLES
    #region REFERENCES TO GAMEOBJECTS
    // Managers
    protected GameObject masterManager;
    protected EntityManager entityManager;
    protected FlockManager flockManager;
    #endregion

    #region OBJECT CONVERSION VARIABLES    
    private string conversionKey;
    private GameObject conversionObject;
    #endregion


    #region ENTITY TYPE DEFINITION DICTIONARY
    // Parent dictionary
    private Dictionary<string, object> entityTypeDefs = new Dictionary<string, object>();
    // Child dictionaries
    private Dictionary<string, object> flockerDefs = new Dictionary<string, object>();
    private Dictionary<string, object> followerDefs = new Dictionary<string, object>();
    #endregion
    #endregion

    #region AWAKE
    // Use this for initialization
    protected void Awake()
    {
        masterManager = GameObject.FindGameObjectWithTag("Managers");

        this.populateTypeDefinitions();
    }
    #endregion

    #region START
    // Use this for initialization also
    protected void Start()
    {
        entityManager = masterManager.GetComponentInChildren<EntityManager>();
        flockManager = masterManager.GetComponentInChildren<FlockManager>();
    }
    #endregion

    private void populateTypeDefinitions()
    {
        // Flocker Properties
        this.flockerDefs.Add("seeker", true);
        this.flockerDefs.Add("fleer", true);
        this.flockerDefs.Add("mass", 3f);
        this.flockerDefs.Add("maxSpeed", 5f);
        this.flockerDefs.Add("motion", MovementState.Seeking);

        // Follower Properties
        this.followerDefs.Add("seeker", true);
        this.followerDefs.Add("fleer", true);
        this.followerDefs.Add("mass", 3f);
        this.followerDefs.Add("maxSpeed", 5f);
        this.followerDefs.Add("motion", MovementState.Seeking);

        // Add all entity type definitions to the entity type dictionary
        this.entityTypeDefs.Add("flocker", this.flockerDefs);
        this.entityTypeDefs.Add("follower", this.followerDefs);
    }

    private void updateConversionProperties(string entityType)
    {
        this.conversionObject.GetComponent<ObjectProperties>().seeker = (bool)((Dictionary<string, object>)this.entityTypeDefs[entityType])["seeker"];
        this.conversionObject.GetComponent<ObjectProperties>().fleer = (bool)((Dictionary<string, object>)this.entityTypeDefs[entityType])["fleer"];
        this.conversionObject.GetComponent<ObjectProperties>().mass = (float)((Dictionary<string, object>)this.entityTypeDefs[entityType])["mass"];
        this.conversionObject.GetComponent<ObjectProperties>().maxSpeed = (float)((Dictionary<string, object>)this.entityTypeDefs[entityType])["maxSpeed"];
        this.conversionObject.GetComponent<ObjectProperties>().motion = (MovementState)((Dictionary<string, object>)this.entityTypeDefs[entityType])["motion"];
    }

    private void getStemCell()
    {

        // Get the object key and object from the entityManager dictionary
        this.conversionKey = this.entityManager.stemCells.First().Key;
        this.conversionObject = this.entityManager.stemCells.First().Value;

        // Remove the object from both the stemCells
        this.entityManager.stemCells.Remove(this.conversionKey);
        this.entityManager.allObjects.Remove(this.conversionKey);

    }

    private string getFlocker()
    {
        if (this.entityManager.stemCells.Count > 0)
        {
            this.getStemCell();

            // Rename the StemCell to its new name
            this.conversionObject.name = this.conversionObject.name.Replace("StemCell", "Flocker");
            this.conversionKey = this.conversionObject.name;

            // Add the new item to the correct dictionaries
            this.entityManager.flockers.Add(this.conversionKey, this.conversionObject);
            this.entityManager.allObjects.Add(this.conversionKey, this.conversionObject);

            this.updateConversionProperties("flocker");
        }

        return this.conversionKey;
    }

    private string getFollower()
    {
        if (this.entityManager.stemCells.Count > 0)
        {
            this.getStemCell();

            // Rename the StemCell to its new name
            this.conversionObject.name = this.conversionObject.name.Replace("StemCell", "Follower");
            this.conversionKey = this.conversionObject.name;

            // Add the new item to the correct dictionaries
            this.entityManager.followers.Add(this.conversionKey, this.conversionObject);
            this.entityManager.allObjects.Add(this.conversionKey, this.conversionObject);

            this.updateConversionProperties("follower");
        }

        return this.conversionKey;
    }

    public string getMinion(string entityOwner, string entityType)
    {
        string minionKey = "";

        entityType = entityType.ToLower();
        entityOwner = entityOwner.ToLower();

        switch (entityType)
        {
            case "follower":

                minionKey = this.getFollower();

                this.updateOwner(entityOwner);                

                break;
            case "flocker":

                minionKey = this.getFlocker();

                this.updateOwner(entityOwner);

                break;
        }

        this.conversionKey = "";
        this.conversionObject = null;

        return minionKey;
    }

    private void updateOwner(string entityOwner)
    {
        switch (entityOwner)
        {
            case "player":
                this.conversionObject.transform.parent = GameObject.Find("PlayerMinions").transform;

                break;
            case "general":
                this.conversionObject.transform.parent = GameObject.Find("EnemyGenerals").transform;

                break;
            case "boss":
                this.conversionObject.transform.parent = GameObject.Find("EnemyBosses").transform;

                break;
            case "minion":
                this.conversionObject.transform.parent = GameObject.Find("EnemyMinions").transform;

                break;
        }
    }

    public void makeStemCell(GameObject obj)
    {
        this.conversionKey = obj.name;
        this.conversionObject = obj;

        int counterIndex = this.conversionKey.IndexOfAny("0123456789".ToCharArray());
        string entityType = this.conversionKey.Substring(0, counterIndex).ToLower();

        // Remove the object from the allObjects dictionary
        this.entityManager.allObjects.Remove(this.conversionKey);

        // Remove the object from its home dictionary
        switch (entityType)
        {
            case "flocker":
                this.entityManager.flockers.Remove(this.conversionKey);

                // Rename the Flocker to StemCell
                this.conversionObject.name = this.conversionObject.name.Replace("Flocker", "StemCell");
                this.conversionKey = this.conversionObject.name;

                break;
            case "follower":
                this.entityManager.followers.Remove(this.conversionKey);

                // Rename the Follower to StemCell
                this.conversionObject.name = this.conversionObject.name.Replace("Follower", "StemCell");
                this.conversionKey = this.conversionObject.name;

                break;
        }

        this.entityManager.stemCells.Add(this.conversionKey, this.conversionObject);
        this.entityManager.allObjects.Add(this.conversionKey, this.conversionObject);

        // Reset the ObjectProperties variables
        this.conversionObject.GetComponent<ObjectProperties>().seeker = false;
        this.conversionObject.GetComponent<ObjectProperties>().fleer = false;
        this.conversionObject.GetComponent<ObjectProperties>().mass = 0f;
        this.conversionObject.GetComponent<ObjectProperties>().maxSpeed = 0f;
        this.conversionObject.GetComponent<ObjectProperties>().motion = MovementState.None;

        // Move the object back to the StemCells game object
        this.conversionObject.transform.parent = GameObject.Find("StemCells").transform;
        this.conversionObject.transform.SetAsFirstSibling();

        this.conversionKey = "";
        this.conversionObject = null;
    }
}
