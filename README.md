# GDD2-Game-2
## New-Master
The New-Master branch is a redesign of the original branch.  Inside the New-Master branch, inside the Assets folder is a folder called OLD.  The OLD folder contains all the items that were inside the original master.

## Purpose Of Redesign
It was noted during the first sprint that the team ran into some trouble with regards to implementing new features.  This new branch is an attempt to consolidate the work into a new working set which is structured and consists purely out of components.  

## Structures
### Assets Folder Structure
There are some elements included that I was uncertain whether we needed going forward, they can be removed as we see fit.

```
─── Assets
    ├── Images
    ├── Materials
    ├── New Terrain.asset
    ├── OLD
    │   ├── Animations
    │   ├── Effects
    │   │   └── Sprites
    │   ├── Materials
    │   ├── NavMesh
    │   │   ├── Gizmos
    │   │   └── NavMeshComponents
    │   │       ├── Editor
    │   │       └── Scripts
    │   ├── Prefabs
    │   ├── Scenes
    │   │   ├── TestEnvironmentBenS
    │   │   ├── TestEnvironment_Alex
    │   │   └── Transitions
    │   ├── Scripts
    │   │   └── Transitions
    │   └── TextMesh Pro
    ├── Prefabs
    │   ├── Cameras
    │   ├── General
    │   ├── Projectiles
    │   └── Units
    ├── Scenes
    │   ├── BossRooms
    │   ├── Levels
    │   │   └── Level1
    │   ├── Menus
    │   └── Transitions
    └── Scripts
```
### Hierarchy - Level1
```
─── Level1
    ├── Directional Light
    ├── EventSystem 
    ├── Managers
    │   ├── EntityManager
    │   │   ├── Player
    │   │	├── PlayerMinions
    │   │	├── EnemyBosses
    │   │	├── EnemyGenerals
    │   │	└── EnemyMinions
    │   ├── CameraManager
    │   ├── FlockManager
    │   ├── TerrainManager 
    │   │	└── Terrain
    │   ├── InstanceManager
    │   └── CollisionManager
    └── Canvas
        └── Panel
```
### Included Scripts
All the new scripts within the Scripts folder are component-based.  This means most of them can be simply dragged and dropped onto the requirement component for the script to work.  These include:
```
├── CameraManager.cs
├── CollisionDetection.cs
├── CollisionManager.cs
├── DebugLines.cs
├── Entity.cs
├── EntityManager.cs
├── FlockManager.cs
├── Flocker.cs
├── Forces.cs
├── Forces_Alignment.cs
├── Forces_Avoidance.cs
├── Forces_Boundary.cs
├── Forces_Cohesion.cs
├── Forces_Evade.cs
├── Forces_Flee.cs
├── Forces_Pursue.cs
├── Forces_Seek.cs
├── Forces_Separation.cs
├── Forces_Wandering.cs
├── ObjectProperties.cs
└── TerrainManager.cs
```
All the scripts are commented and should be self explanatory.  I was uncertain whether we would be using the collisions or whether we are using the built-in Unity collisions, so these scripts are also included.  If we remove them some refractoring may be required in other scripts.

## Purposes of Hierarchy Structure Components
Within the hierarchy, each manager is in charge of creation and destruction (if required) of their related elements.  That means, the EntityManager would contain the scripts that instantiate the Player, PlayerMinions, EnemyBosses, EnemyGenerals and EnemyMinions.

As examples, the Player and Flockers are created inside the EntityManager.
```
#region PLAYER INSTANTIATION
    /// <summary>
    /// Instantiates a player prefab on the terrain
    /// </summary>
    /// <param name="player">Sphere of tremendousness or something. It's what they chase</param>
    private void CreatePlayer(GameObject player)
    {
        // Specifies the position where the player character will be instantiated
        Vector3 playerPosition = new Vector3(-24f, 1f, -37f);

        // Instantiate a new player
        this.newObject = Instantiate(player, playerPosition, Quaternion.Euler(0, 0, 0));
        // Assign the object to the appropriate location in the Hierarchy
        // This refers to the Hierarchy as specified inside the scene in Unity. i.e. the player will be
        // created in Level1 -> Managers -> EntityManager -> Player
        this.newObject.transform.parent = GameObject.Find("Player").transform;
        // Get the prefab name of the object
        this.newObject.name = "Player";

        //Set object specific properties
        this.newObject.GetComponent<ObjectProperties>().seeker = false;
        this.newObject.GetComponent<ObjectProperties>().fleer = false;
        this.newObject.GetComponent<ObjectProperties>().mass = 0f;
        this.newObject.GetComponent<ObjectProperties>().maxSpeed = 0f;
        this.newObject.GetComponent<ObjectProperties>().motion = MovementState.None;
        this.newObject.GetComponent<ObjectProperties>().position = this.newObject.transform.position;

        // Add the new object to the appropriate lists
        allObjects.Add(this.newObject.name, this.newObject);
    }
    #endregion
```
The name <b>flocker</b> refers to a minion.  We can rename this if it is required.
```
#region FLOCKER INSTANTIATION
    /// <summary>
    /// Instantiates a flocker prefab on the terrain
    /// </summary>
    /// <param name="flocker">Human gameobject</param>
    private void CreateFlocker(GameObject flocker)
    {
        // Instantiate a new monster
        this.newObject = Instantiate(flocker, InstantiationPosition(flocker), Quaternion.Euler(0, 0, 0));
        // Assign the object to the appropriate location in the Hierarchy
        // This refers to the Hierarchy as specified inside the scene in Unity. i.e. the player will be
        // created in Level1 -> Managers -> EntityManager -> EnemyMinions
        this.newObject.transform.parent = GameObject.Find("EnemyMinions").transform;
        // Get the prefab name of the object
        this.newObject.name = this.newObject.name.Substring(0, this.newObject.name.IndexOf("(")) + flockers.Count;

        //Set object specific properties
        this.newObject.GetComponent<ObjectProperties>().seeker = true;
        this.newObject.GetComponent<ObjectProperties>().fleer = true;
        this.newObject.GetComponent<ObjectProperties>().mass = this.flockerMass;
        this.newObject.GetComponent<ObjectProperties>().maxSpeed = this.flockerMaxSpeed;
        this.newObject.GetComponent<ObjectProperties>().motion = MovementState.Seeking;
        this.newObject.GetComponent<ObjectProperties>().position = this.newObject.transform.position;

        // Add the new object to the appropriate lists
        flockers.Add(this.newObject.name, this.newObject);
        allObjects.Add(this.newObject.name, this.newObject);
    }
    #endregion
```
