using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreatureSpawnerPrefab : MonoBehaviour
{
    public GameObject creaturePrefab; // Creates a link to the creature object
    public GameObject creatureSettings; // Creates a link to the creature settings
    CreatureSettings settings; // Creates a variable to store the settings record
    public int specieNumber; // Holds the specie number 1 to 3
    float speed; // Holds the speed of the creatures
    float size; // Holds the size of the creatures
    float sense; // Holds the sense range of the creatures
    int count; // Holds the number of creatures to be spawned
    bool alive; // Holds whether or not that specie is selected
    Color colour; // Holds the colour of the specie
    float energy; // Holds the starting amount of energy
    public TerrainGenerator terrain; // Creates a link to the terrain script to access the height map
    public List<GameObject> creatureList; // Holds a list of all living Creatures

    public void Start() // Executes when the program runs
    {
        settings = creatureSettings.GetComponent<CreatureSettings>(); // Instantiates the settings record

    }

    public void StartBtn() { // This method executes when the start button is pressed
        StartCoroutine(SpawnCreatureWave()); // Starts the loop that spawns a group of creatures
    }

    public void spawnCreature(float speed, float sense,float size,float energy, float xPos,float zPos) {
        // Method declared that takes five parameters, method used to spawn singular creature
        GameObject clone = Instantiate(creaturePrefab) as GameObject; // Instanttaies a new creature object using the prefab
        clone.GetComponent<Creature>().energy = energy; // Sets the starting energy of the creature
        clone.GetComponent<Creature>().speed = speed; // Sets the speed attribute
        clone.GetComponent<Creature>().senseRange = sense; // Sets the sense range attribute
        clone.GetComponent<Creature>().size = size; // Sets the size attribute
        clone.GetComponent<Creature>().specieNumber = specieNumber;
        clone.transform.SetParent(transform, true); // Places the new creature under the creature spawner catagory
        clone.GetComponent<Renderer>().material.color = colour; // Sets the colour attribute

        creatureList.Add(clone); // Adds the clone object to the creature list
        clone.transform.position = new Vector3(xPos, 0, zPos); // Sets the position of the creature to that in the parameters
    }
    public bool checkWater(float x, float z)
    { // Checks if the Resource is spawning underwater
        Vector3 scale = terrain.data.heightmapScale; // Extracts the scale of the terrain (set in the terrain generator class)
        float y = (float)terrain.data.GetHeight((int)(x / scale.x), (int)(z / scale.z));
        // Extracts the height and applies the scale multipliers to get the coordinate
        if (y <= 5) return false; // Returns false if it is underwater
        else return true; // Returns true if it is on land
    }
    IEnumerator SpawnCreatureWave() { // Enumerator method used to loop through a set of creature spawnings
        if (specieNumber == 1) { // If the specie being spawned is specie 1
            // Update the creature spawner attributes to the specie 1 record
            speed = settings.specie1.speed;
            sense = settings.specie1.sense;
            size = settings.specie1.size;
            alive = settings.specie1.alive;
            count = (int)settings.specie1.count;
            energy = settings.specie1.energy;
            colour = new Color(0, 255, 0);
        }
        else if (specieNumber == 2) // If the specie being spawned is specie 2
        {
            // Update the creature spawner attributes to the specie 2 record
            speed = settings.specie2.speed;
            sense = settings.specie2.sense;
            size = settings.specie2.size;
            alive = settings.specie2.alive;
            count = (int)settings.specie2.count;
            energy = settings.specie2.energy;
            colour = new Color(255, 0, 0);
        }
        else if (specieNumber == 3) // If the specie being spawned is specie 3
        {
            // Update the creature spawner attributes to the specie 3 record
            speed = settings.specie3.speed;
            sense = settings.specie3.sense;
            size = settings.specie3.size;
            alive = settings.specie3.alive;
            energy = settings.specie3.energy;
            count = (int)settings.specie3.count;
            colour = new Color(0, 0, 255);
        }
        for (int i = 0; i < count; i++) // A for loop that loops 10 times
        {
            float xPos = UnityEngine.Random.Range(1, 511); // Random x coordinate generated that is on terrain
            float zPos = UnityEngine.Random.Range(1, 511); // Randon y coordinate generated that is on terrain
            while (checkWater(xPos, zPos) == true)
            {
                xPos = UnityEngine.Random.Range(1, 511); // Random x coordinate generated that is on terrain
                zPos = UnityEngine.Random.Range(1, 511); // Randon y coordinate generated that is on terrain
            }
            yield return new WaitForEndOfFrame();// Waits a set amount of time 
            if (alive == true)
            spawnCreature(speed, sense, size, energy, xPos, zPos); // Spawns the creature with speed 20, and the random position
        }
    }
}
