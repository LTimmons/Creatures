using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class MenuButtons : MonoBehaviour
{
    public GameObject settingsPanel; // Creates a connection to the settings panel
    public GameObject overallUI; // Creats a connection to the main UI
    public GameObject startBtn; // Creates a link to the start button
    public GameObject pauseButton; // Creates a link to the pause button
    public GameObject terrain;
    public GameObject CreatureSpawner1;
    public GameObject CreatureSpawner2;
    public GameObject CreatureSpawner3;
    public GameObject ResourceSpawner;
    public GameObject speedSlider;
    public GameObject senseSlider;
    public GameObject sizeSlider;
    public GameObject countSlider;
    public GameObject energySlider;
    public GameObject resourceSlider;
    public GameObject lifespanSlider;
    public GameObject creatureSettings;
    public GameObject enabledSwitch;

    public bool gameState = false;
    public bool paused = false;
    
    private void Start() // Executes on the first frame
    {
        settingsPanel.SetActive(false); // Hides the settings panel
        pauseButton.SetActive(false); // Hides the pause button
    }
    public void settingsBtnPress() { // Executes when settings button is pressed
        settingsPanel.SetActive(true); // Shows the settings panel
    }
    public void simulationBtnPress() // Executes when the simulation button is pressed
    {
        settingsPanel.SetActive(false); // Hides the settings panel
    }
    public void fullScreenButton() { // Executes when the full screen button is pressed
        if (overallUI.activeSelf == true) // If the program is not in fullscreen mode
        {
            overallUI.SetActive(false); // Hides all of the ui (fullscreen mode)
        }
        else { // If the program is in fullscreen mode
            overallUI.SetActive(true); // Shows all of the ui
        }
    }
    public void defaultsBtn() { // Executes when the default button is pressed
        // Reset specie 1
        creatureSettings.GetComponent<CreatureSettings>().specie1.speed = speedSlider.GetComponent<SliderNumberHandler>().defaultValue;
        creatureSettings.GetComponent<CreatureSettings>().specie1.sense = senseSlider.GetComponent<SliderNumberHandler>().defaultValue;
        creatureSettings.GetComponent<CreatureSettings>().specie1.size = sizeSlider.GetComponent<SliderNumberHandler>().defaultValue;
        creatureSettings.GetComponent<CreatureSettings>().specie1.count = countSlider.GetComponent<SliderNumberHandler>().defaultValue;
        creatureSettings.GetComponent<CreatureSettings>().specie1.energy = energySlider.GetComponent<SliderNumberHandler>().defaultValue;
        creatureSettings.GetComponent<CreatureSettings>().specie1.alive = true;
        // Reset specie 2
        creatureSettings.GetComponent<CreatureSettings>().specie2.speed = speedSlider.GetComponent<SliderNumberHandler>().defaultValue;
        creatureSettings.GetComponent<CreatureSettings>().specie2.sense = senseSlider.GetComponent<SliderNumberHandler>().defaultValue;
        creatureSettings.GetComponent<CreatureSettings>().specie2.size = sizeSlider.GetComponent<SliderNumberHandler>().defaultValue;
        creatureSettings.GetComponent<CreatureSettings>().specie2.count = countSlider.GetComponent<SliderNumberHandler>().defaultValue;
        creatureSettings.GetComponent<CreatureSettings>().specie2.energy = energySlider.GetComponent<SliderNumberHandler>().defaultValue;
        creatureSettings.GetComponent<CreatureSettings>().specie2.alive = true;
        // Reset specie 3
        creatureSettings.GetComponent<CreatureSettings>().specie3.speed = speedSlider.GetComponent<SliderNumberHandler>().defaultValue;
        creatureSettings.GetComponent<CreatureSettings>().specie3.sense = senseSlider.GetComponent<SliderNumberHandler>().defaultValue;
        creatureSettings.GetComponent<CreatureSettings>().specie3.size = sizeSlider.GetComponent<SliderNumberHandler>().defaultValue;
        creatureSettings.GetComponent<CreatureSettings>().specie3.count = countSlider.GetComponent<SliderNumberHandler>().defaultValue;
        creatureSettings.GetComponent<CreatureSettings>().specie3.energy = energySlider.GetComponent<SliderNumberHandler>().defaultValue;
        creatureSettings.GetComponent<CreatureSettings>().specie3.alive = true;
        // Reset resources
        resourceSlider.GetComponent<Slider>().value = resourceSlider.GetComponent<SliderNumberHandler>().defaultValue;
        lifespanSlider.GetComponent<Slider>().value = lifespanSlider.GetComponent<SliderNumberHandler>().defaultValue;
        // Reset creature sliders
        speedSlider.GetComponent<Slider>().value = speedSlider.GetComponent<SliderNumberHandler>().defaultValue;
        senseSlider.GetComponent<Slider>().value = senseSlider.GetComponent<SliderNumberHandler>().defaultValue;
        sizeSlider.GetComponent<Slider>().value = sizeSlider.GetComponent<SliderNumberHandler>().defaultValue;
        countSlider.GetComponent<Slider>().value = countSlider.GetComponent<SliderNumberHandler>().defaultValue;
        energySlider.GetComponent<Slider>().value = energySlider.GetComponent<SliderNumberHandler>().defaultValue;
        enabledSwitch.GetComponent<Toggle>().isOn = true;
    }
    public void pauseBtn() { // Function triggers when pause button pressed
        paused = !paused; // Inverts the boolean state of paused
        switch (paused) // Selects a statement based on the value of paused
        {
            case true: // When paused is true
                pauseButton.GetComponentInChildren<TMP_Text>().text = "Unpause";
                // Set button text to unpause
                break;
            default: // When paused is false or undefined
                pauseButton.GetComponentInChildren<TMP_Text>().text = "Pause";
                // Set button text to pause
                break;
        }
    }
    public void startButton() { // Triggers when the start button is pressed
        gameState = !gameState; // Boolean game state inverted
        if (gameState == true) // If the simulation is starting
        {
            ResourceSpawner.GetComponent<ResourceSpawner>().StartBtn(); // Spawn resources
            CreatureSpawner1.GetComponent<CreatureSpawnerPrefab>().StartBtn(); // Spawn creatures
            CreatureSpawner2.GetComponent<CreatureSpawnerPrefab>().StartBtn();
            CreatureSpawner3.GetComponent<CreatureSpawnerPrefab>().StartBtn();
            terrain.GetComponent<TerrainGenerator>().StartBtn(); // Generate the terrain
            startBtn.GetComponentInChildren<TMP_Text>().text = "Stop"; // Switch button text to stop

            pauseButton.SetActive(true); // Show the pause button
        }
        else{ // If the simulation is ending

            pauseButton.SetActive(false); // Hide the pause button
            // Despawn all entities
            foreach (GameObject clone in ResourceSpawner.GetComponent<ResourceSpawner>().resourceList) // For every resource
            {
                Destroy(clone); // Kill the resource
            }
            foreach (GameObject clone in CreatureSpawner1.GetComponent<CreatureSpawnerPrefab>().creatureList) // For every specie 1

                Destroy(clone); // Kill the creature

            foreach (GameObject clone in CreatureSpawner2.GetComponent<CreatureSpawnerPrefab>().creatureList) // For every specie 2
            {

                Destroy(clone); // Kill the creature
            }
            foreach (GameObject clone in CreatureSpawner3.GetComponent<CreatureSpawnerPrefab>().creatureList) // For every specie 3
            {

                Destroy(clone); // Kill the creature
            }
            ResourceSpawner.GetComponent<ResourceSpawner>().resourceList = new List<GameObject>(); // Clears the resource list
            CreatureSpawner1.GetComponent<CreatureSpawnerPrefab>().creatureList = new List<GameObject>(); // Clears the creature lists
            CreatureSpawner2.GetComponent<CreatureSpawnerPrefab>().creatureList = new List<GameObject>();
            CreatureSpawner3.GetComponent<CreatureSpawnerPrefab>().creatureList = new List<GameObject>();
            startBtn.GetComponentInChildren<TMP_Text>().text = "Start"; // Sets the button text back to start
        }
    }
}
