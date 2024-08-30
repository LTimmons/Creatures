using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour // Class assigned to the creatures
{

    public TerrainGenerator terrain; // Creates a link to the terrain script to access the height map
    public NavMeshAgent nav; // Creates a link to the movement of the creature
    public Vector3 target; // Declares a vector used to determine the point the creature is travelling towards
    private ResourceSpawner resourseSpawner; // Points to the live resource spawner object
    private CreatureSpawnerPrefab creatureSpawner; // Points to the live creature spawner object
    public MenuButtons menuButtons; // Points to the live menu buttons object

    public float speed { get; set; }  // Holds the creatures speed and encapsulates it
    public float energy { get; set; } // Holds the creatures energy and encapsulates it

    public float startingEnergy; // Holds the creatures starting energy 
    public float senseRange { get; set; } // Holds the creatures sense range and encapsulates it
    public float size { get; set; } // Holds the creatures size and encapsulates it
    public float colour { get; set; } // Holds the creatures colour and encapsulates it
    public float specieNumber { get; set; } // Holds the creatures specie number and encapsulates it
    public bool isAlive { get; set; } // Holds the creatures speed and encapsulates it

    private Vector3 closestResource = new Vector3(-999, -999, -999); // Holds the location of the closest resource
    private float pathfindTimer = 0; // Holds the seconds until the creature can pathfind again
    private float birthTimer = 10; // Holds the seconds until the creature can give birth again
    private GameObject closestObject = null; // Holds the closest resource in object form
    private GameObject resourceTracked = null; // Holds the gameobject of the resource being pathfinded


    // Start is called before the first frame update
    async void Start() // This method runs when a creature is spawned
    {
        startingEnergy = energy; // Sets the starting energy as a permanent variable
        menuButtons = GameObject.Find("Overall UI").GetComponent<MenuButtons>(); // Creates a link to the menu buttons
        terrain = GameObject.Find("Terrain").GetComponent<TerrainGenerator>(); // Links the terrain to the grass terrain
        resourseSpawner = GameObject.Find("Resource Spawner").GetComponent<ResourceSpawner>(); // Creates a link to the resource spawner
        //Locates the correct creature spawner using the specie number and creates a link to it
        if (specieNumber == 1) creatureSpawner = GameObject.Find("Specie 1 spawner").GetComponent<CreatureSpawnerPrefab>(); 
        else if (specieNumber == 2) creatureSpawner = GameObject.Find("Specie 2 spawner").GetComponent<CreatureSpawnerPrefab>();
        else creatureSpawner = GameObject.Find("Specie 3 spawner").GetComponent<CreatureSpawnerPrefab>();
        nav = gameObject.GetComponent<NavMeshAgent>(); // Extracts the data from the movement module
        nav.speed = speed; // Sets the movement modules speed to the user set speed
        await Task.Delay(1000); // Waits one second to ensure that resources have began spawning
        pathfind(); // Starts the movement cycle
    }
    public float getY(Vector2 point) {
        // The function responsible for converting the height map to coordinates, takes the x and z coordinates as parameters
        Vector3 scale = terrain.data.heightmapScale; // Extracts the scale of the terrain (set in the terrain generator class)
        float y =  (float)terrain.data.GetHeight((int)(point.x / scale.x), (int)(point.y / scale.z));
        // Extracts the height and applies the scale multipliers to get the coordinate
        return y+4; // Returns the coordinate + 4 (to account for the height of the creature)
    }

    
    // Update is called once per frame
    void Update(){
        
        transform.position = new Vector3(transform.position.x,
                getY(new Vector2(transform.position.x, transform.position.z))
                , transform.position.z); // Sets y of the creature coordinate to that of the terrain
                                            // (Essentially applying gravity to the creature by sticking
                                            // it to the terrain)
        if (menuButtons.paused == false) // If the simulation isnt paused
        {
            birthTimer -= Time.deltaTime; // Tick the birth timer
            if (energy > 100) // If energy is above 100
            {
                if (birthTimer < 0) // If the birth timer has reached 0
                {
                    birthTimer = 10; // Reset the birth timer
                    birth(); // Birth a new creature
                    energy = energy - 50; // Decrease energy by 50
                }}
            findClosestResource(); // Locate the closest resource to the creature
            pathfindTimer -= Time.deltaTime; // Tick the pathfinding timer
            if (pathfindTimer <= 0) // If the pathfinding timer reaches 0
            {
                pathfindTimer = 0.5f; // Reset the pathfinding timer
                pathfind(); // Pathfind to the closest resource
            }
            energy -= Time.deltaTime * speed / 4; // Decrease energy based on time
            Debug.Log(energy);
            if (energy <= 0.0f) // If energy hits 0
            {
                die(); // Kill creature
            }}
        else { // If paused
            nav.SetDestination(new Vector3(transform.position.x, transform.position.y, transform.position.z)); 
            // Freeze the creature in place
        }
    }
    public void die() { // Die function
        creatureSpawner.creatureList.Remove(gameObject); // Remove the creature from the creature list
        Destroy(gameObject); // Remove the creature object
    }
    public float calculateDistance(Vector3 item1, Vector3 item2) {
        return Mathf.Sqrt((item1.x - item2.x) * (item1.x - item2.x) + (item1.z - item2.z) * (item1.z - item2.z) + (item1.y - item2.y) * (item1.y - item2.y));
    }
    public float mutateGene(double chanceOfMutation, float gene) {
        // Validate the chance of mutation
        if (chanceOfMutation > 1 || chanceOfMutation < 0) {
            //If the mutation chance is invalid
            //Display error in console
            return gene;
            //Dont mutate
        }
        int randomMutationChance = Random.Range(0, 100);
        // Generate a number 0 to 100

        if (randomMutationChance < chanceOfMutation * 100)
        // If the generated number is larger than the chance of mutation
        {
            // Randomly select a negative or positive direction of the mutation
            int negOrPos = Random.Range(0, 1);
            if( negOrPos==0) negOrPos = -1;
            // Select a scale of mutation 0 - 40 % increase/decrease
            float mutationPercentage = (Random.Range(0, 40))*negOrPos;

            mutationPercentage += 100;
            // Return the mutated gene
            return (gene * (mutationPercentage / 100));
        }
        else {
            //Dont mutate
            return gene;
        }
    }
    public void birth() {
        // Birth Function
        float newSpeed = mutateGene(0.5, speed); // Mutate speed with a 50% chance of mutation
        float newSense = mutateGene(0.5, senseRange); // Mutate speed with a 50% chance of mutation
        float newEnergy = mutateGene(0.5, startingEnergy); // Mutate speed with a 50% chance of mutation
        float newSize = mutateGene(0.5, size); // Mutate speed with a 50% chance of mutation
        Debug.Log("Creature born");
        Debug.Log("Speed:" + newSpeed);
        Debug.Log("Sense:" + newSense);
        Debug.Log("Size:" + newSize);
        Debug.Log("Energy:" + newEnergy);

        // Spawn creature with mutated genes
        creatureSpawner.spawnCreature(newSpeed, newSense, newSize, newEnergy, transform.position.x, transform.position.z);
    }
    public bool waterRayCast(Vector3 start, Vector3 end) { // Raycast and search for water between start and end point
        Vector3 movementVector = end - start; // Find a vector between the two points
        Vector3 unitVector = movementVector / Mathf.Sqrt(movementVector.x* movementVector.x +
            movementVector.y* movementVector.y + movementVector.z*movementVector.z);
        // The unit vector of the vector found it calculated. This means that the magnitude of this vector is 1
        float count = Mathf.Ceil(Mathf.Sqrt(movementVector.x * movementVector.x + movementVector.y * movementVector.y 
            + movementVector.z * movementVector.z));
        // Calculates the actual distance
        // The actual distance between the two points is calculated
        while (Mathf.Sqrt(unitVector.x * unitVector.x + unitVector.y * unitVector.y + unitVector.z * unitVector.z) < count)  {
            // Loops until every point along the ray has been checked
            unitVector += 15*unitVector; // Adds the unit vector to itself
            float y = getY(new Vector2(unitVector.x + start.x, unitVector.z+start.z))-4; // Gets the y level of the point
            if (y <= 5) { // If the point is under water
                return false; // returns false
            }
        }
        return true; // If none of the points are underwater return true

    }
    public Vector3 findRandomPosition (){
        int xPos = Random.Range(2, 510); // Generate a random x coordinate on the terrain
        int zPos = Random.Range(2, 510); // Generate a random z coordinate on the terrain
        return new Vector3(xPos, getY(new Vector2(xPos, zPos)), zPos); // Returns the new
                                                                       // random vector position
    }
    public void findClosestResource() // Function used to locate the closest resource
    {
        List<GameObject> resourceList = resourseSpawner.resourceList; // Locally saves the resource list
        float smallestDistance = 99999999; // Sets the smallest distance to an unreasonably large distance
        Vector3 tempClosestResource = closestResource; // Creates a temporary variable for the closest resource
        foreach (GameObject i in resourceList) // Searches through the resource list linearly
        {
            Resource resource = i.GetComponent<Resource>(); // Locally stores the resource in question
            float distance = calculateDistance(new Vector3(i.transform.position.x,
                getY(new Vector2(i.transform.position.x, i.transform.position.z)), i.transform.position.z), transform.position);
            // Calculates the distance to the resource
            if ((smallestDistance > distance) && waterRayCast(transform.position, new Vector3(resource.X,
                getY(new Vector2(resource.X, resource.Z)), resource.X)))
            // If the distance is less than the previous closest resource and there is no water found on the path
            {
                // Sets the closest resource to the newly selected one
                tempClosestResource = new Vector3(i.transform.position.x, getY(new Vector2(i.transform.position.x,
                    i.transform.position.z)), i.transform.position.z);
                smallestDistance = distance;
                closestObject = i;

            }

        }
        closestResource = tempClosestResource; // Sets the closest resource to be used in pathfinding
    }

    void pathfind() // Pathfinding script
    {
        // If tracking a resource and the resource has been reached
        if ((resourceTracked!=null)&& (nav.remainingDistance <= 2))
        {
            // Kill the resource in question
            resourceTracked.GetComponent<Resource>().die();
            resourceTracked = null;
            // Consume the energy from the resource
            energy += 50;

        }
        // If the distance of the path is less than the sense range, 
        if (resourceTracked==null && (calculateDistance(closestResource,
            transform.position) <= senseRange))
        {
            // Set the resource to be tracked to the closest object
            resourceTracked = closestObject;
            target = closestResource;
            // Move to the resource
            nav.SetDestination(target);
        }
        // If the closest resource is too far away
        else if ((nav.remainingDistance <= 2))
        {
            // Alert the class that there is no resource being tracked
            resourceTracked = null;
            target = findRandomPosition(); // Generate a random point
            while (waterRayCast(transform.position, target) == false) { 
                // Regenerate until the random point doesnt include water
                target = findRandomPosition(); 
            }
            nav.SetDestination(target); // move towards that point       
        }  
    }

    


}
