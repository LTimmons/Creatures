using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceSpawner : MonoBehaviour
{
    public GameObject ResourcePrefab; // Creates a link to the resource object
    public TerrainSettings settings; // Links the script to the terrain settings
    public TerrainGenerator terrain; // Creates a link to the terrain script to access the height map

    private int maxResources;  // Holds the maximum amount of Resources on the terrain
    private int lifeSpan; // Holds the lifespan (seconds) of the Resources
    public List<GameObject> resourceList; // Holds a list of all living Resources


    public void StartBtn() { // This method executes when the start button is pressed
        StartCoroutine(SpawnResourceWave()); // Starts the loop that spawns a group of resources
    }
    public bool checkWater(float x, float z) { // Checks if the Resource is spawning underwater
        Vector3 scale = terrain.data.heightmapScale; // Extracts the scale of the terrain
        float y = (float)terrain.data.GetHeight((int)(x / scale.x), (int)(z / scale.z));
        // Extracts the height and applies the scale multipliers to get the coordinate
        if (y <= 5) return false; // Returns false if it is underwater
        else return true; // Returns true if it is on land
    }

    public void spawnResource(float xPos,float zPos) {
        // Method declared that takes two parameters, method used to spawn singular resource
        GameObject clone = Instantiate(ResourcePrefab) as GameObject; // Creates a new resource object using the prefab
        clone.transform.position = new Vector3(xPos, 0, zPos); // Sets the position of the resource to that in the parameters
        clone.GetComponent<Resource>().Lifespan = settings.resource.lifespan; // Sets the lifespan of the clone to the user
                                                                              // inputted lifespan
        clone.GetComponent<Resource>().X = xPos; // Stores the x value as an attribute
        clone.GetComponent<Resource>().Z = zPos; // Stores the y value as an attribute
        clone.transform.SetParent(transform, true); // Stores the clone under the resource spawner in the heirarchy
        resourceList.Add(clone);
    }

    IEnumerator SpawnResourceWave() { // Enumerator method used to loop through a set of resource spawnings
        for (int i = 0; i < settings.resource.max; i++) // A for loop that loops until max resources spawned
        {
            float xPos = UnityEngine.Random.Range(1, 511); // Random x coordinate generated that is on terrain
            float zPos = UnityEngine.Random.Range(1, 511); // Randon y coordinate generated that is on terrain
            while (checkWater(xPos, zPos) == false) {
                xPos = UnityEngine.Random.Range(1, 511); // Random x coordinate generated that is on terrain
                zPos = UnityEngine.Random.Range(1, 511); // Randon y coordinate generated that is on terrain
            }
            yield return new WaitForEndOfFrame();// Waits a set amount of time 
            spawnResource(xPos, zPos); // Spawns the resource with speed 20, and the random position
        }
    }
}
