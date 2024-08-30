using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;


public class Resource : MonoBehaviour
{

    public TerrainGenerator terrain; // Creates a link to the terrain script to access the height map
    public MenuButtons menuButtons; // Holds the menu buttons as a variable
    private float x { get; set; } // Holds the x coordinate of the resource
    private float z { get; set; } // Holds the z coordinate of the resource
    private float lifespan { get; set; } // Holds the lifespan of the resource

    private float timer; // Holds a timer (seconds) of how long until the resource dies

    private bool paused;
    public float Lifespan   // lifespan encapsulation method
    {
        get { return lifespan; }   // get method
        set { lifespan = value; }  // set method
    }
    public float X   // x encapsulation method
    {
        get { return x; }   // get method
        set { x = value; }  // set method
    }
    public float Z   // z encapsulation method
    {
        get { return z; }   // get method
        set { z = value; }  // set method
    }

    // Start is called before the first frame update
    void Start() // This method runs when a creature is spawned
    {
        terrain = GameObject.Find("Terrain").GetComponent<TerrainGenerator>(); // Links the terrain to the grass terrain
        menuButtons = GameObject.Find("Overall UI").GetComponent<MenuButtons>(); // Links the menu buttons to the specific
                                                                                 // ui buttons
        transform.position = new Vector3(transform.position.x,
        getY(new Vector2(transform.position.x, transform.position.z))
        , transform.position.z); // Sets y of the creature coordinate to that of the terrain
                                 // (Essentially applying gravity to the creature by sticking
                                 // it to the terrain)
        int variation = UnityEngine.Random.Range(-25, 25); // Generates a number betweeen -25 and 25
        timer = lifespan + variation; // The timer is set at lifespan + variation
        if (timer <= 25) { // If the timer is below 25
            timer = 25; // Timer is set to 25
        }

    }
    public bool checkWater(float x, float z)
    { // Checks if the Resource is spawning underwater
        Vector3 scale = terrain.data.heightmapScale; // Extracts the scale of the terrain
                                                     // (set in the terrain generator class)
        float y = (float)terrain.data.GetHeight((int)(x / scale.x), (int)(z / scale.z));
        // Extracts the height and applies the scale multipliers to get the coordinate
        if (y <= 5) return false; // Returns false if it is underwater
        else return true; // Returns true if it is on land
    }
    public float getY(Vector2 point)
    {
        // The function responsible for converting the height map to coordinates, takes the x and z coordinates as parameters
        Vector3 scale = terrain.data.heightmapScale; // Extracts the scale of the terrain (set in the terrain generator class)
        float y = (float)terrain.data.GetHeight((int)(point.x / scale.x), (int)(point.y / scale.z));
        // Extracts the height and applies the scale multipliers to get the coordinate
        return y; // Returns the y coordinate 
    }
    public void die() { // Die function re-spawns the resource in a new location
        int xPos = UnityEngine.Random.Range(1, 511); // Random x coordinate generated that is on terrain
        int zPos = UnityEngine.Random.Range(1, 511); // Randon y coordinate generated that is on terrain
        while (checkWater(xPos, zPos) == false) // While the coordinates are in water
        {
            xPos = UnityEngine.Random.Range(1, 511); // Random x coordinate generated that is on terrain
            zPos = UnityEngine.Random.Range(1, 511); // Randon y coordinate generated that is on terrain
        }
        transform.position = new Vector3(xPos, getY(new Vector2(xPos, zPos)), zPos); // Sets the position
                                                                                     // to the generated values
        x = xPos; // Updates the position in the resources attributes
        z = zPos;
    }
        
    // Update is called once per frame
    void Update()
    {
        if (menuButtons.paused == false) // If the simulation isnt paused
        {
            if (paused == true) paused = false;
                        
            else (timer) -= Time.deltaTime; // Decrease the timer by the time since the last frame

            if (timer <= 0.0f) // When times reaches zero
            {
                int variation = UnityEngine.Random.Range(-25, 25); // Generate a new variation
                // Reset the timer
                timer = lifespan + variation; // Add the variation to the lifespan and set the timer
                if (timer <= 25) // If timer less than 25
                {
                    timer = 25; // Set timer to 25
                }
                die(); // Kill the resource
            }
        }
        else {
            paused = true;
        }
    }

}
