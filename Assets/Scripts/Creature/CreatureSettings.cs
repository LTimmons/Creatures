using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using TMPro;
using System.Security.Cryptography; 
using System.Text;
using System.IO;


public record creatureData(){ // Record used to hold the settings related to creatures
    public float speed; // Stores the inputted speed
    public float sense; // Stores the inputted sense range
    public float size; // Stores the inputted size
    public float count; // Stores the inputted amount of creatures
    public float energy; // Stores the inputted starting energy of the creatures
    public bool alive = true ; // Stores the inputted value of the alive checkbox
}
public class CreatureSettings : MonoBehaviour
{
    public creatureData specie1; // Creature object for specie 1
    public creatureData specie2; // Creature object for specie 2
    public creatureData specie3; // Creature object for specie 3

    public Toggle aliveToggle; // Links the code to the alive toggle
    public GameObject speedSlider; // Links the code to the speed slider
    public GameObject senseSlider; // Links the code to the sense range slider
    public GameObject sizeSlider; // Links the code to the size slider
    public GameObject countSlider; // Links the code to the count slider
    public GameObject energySlider; // Links the code to the energy slider
    public GameObject infoText; // Links the code to the text above the creature settings
    int specieSelected = 1; // Holds the specie that is currently being modified
    
    void Start() // Start is called before the first frame update
    {
        specie1 = new creatureData(); // Instantiates the specie 1 record
        specie2 = new creatureData(); // Instantiates the specie 2 record
        specie3 = new creatureData(); // Instantiates the specie 3 record
        // Sets all of the sliders to their default value
        specie1.speed = speedSlider.GetComponent<SliderNumberHandler>().defaultValue; 
        specie2.speed = speedSlider.GetComponent<SliderNumberHandler>().defaultValue;
        specie3.speed = speedSlider.GetComponent<SliderNumberHandler>().defaultValue;
        specie1.size = sizeSlider.GetComponent<SliderNumberHandler>().defaultValue;
        specie2.size = sizeSlider.GetComponent<SliderNumberHandler>().defaultValue;
        specie3.size = sizeSlider.GetComponent<SliderNumberHandler>().defaultValue;
        specie1.sense = senseSlider.GetComponent<SliderNumberHandler>().defaultValue;
        specie2.sense = senseSlider.GetComponent<SliderNumberHandler>().defaultValue;
        specie3.sense = senseSlider.GetComponent<SliderNumberHandler>().defaultValue;
        specie1.count = countSlider.GetComponent<SliderNumberHandler>().defaultValue;
        specie2.count = countSlider.GetComponent<SliderNumberHandler>().defaultValue;
        specie3.count = countSlider.GetComponent<SliderNumberHandler>().defaultValue;
        specie1.energy = energySlider.GetComponent<SliderNumberHandler>().defaultValue;
        specie2.energy = energySlider.GetComponent<SliderNumberHandler>().defaultValue;
        specie3.energy = energySlider.GetComponent<SliderNumberHandler>().defaultValue;
    }
    public void specie1Btn() { // Method triggers when the specie 1 button is pressed
        // Selects specie 1
        specieSelected = 1;
        // Updates the text to specie 1
        infoText.GetComponent<TMP_Text>().text = "Specie 1 selected";
        // Updates all of the input components to the specie 1 settings
        speedSlider.GetComponent<Slider>().value = specie1.speed;
        senseSlider.GetComponent<Slider>().value = specie1.sense;
        sizeSlider.GetComponent<Slider>().value = specie1.size;
        countSlider.GetComponent<Slider>().value = specie1.count;
        energySlider.GetComponent<Slider>().value = specie1.energy;
        aliveToggle.GetComponent<Toggle>().isOn = specie1.alive;
        
    }
    public void specie2Btn(){ // Method triggers when the specie 2 button is pressed
        // Selects specie 2
        specieSelected = 2;
        // Updates the text to specie 2
        infoText.GetComponent<TMP_Text>().text = "Specie 2 selected";
        // Updates all of the input components to the specie 2 settings
        speedSlider.GetComponent<Slider>().value = specie2.speed;
        senseSlider.GetComponent<Slider>().value = specie2.sense;
        sizeSlider.GetComponent<Slider>().value = specie2.size;
        countSlider.GetComponent<Slider>().value = specie2.count;
        energySlider.GetComponent<Slider>().value = specie2.energy;
        aliveToggle.GetComponent<Toggle>().isOn = specie2.alive;
        
    }
    public void specie3Btn(){ // Method triggers when the specie 3 button is pressed
        // Selects specie 3
        specieSelected = 3;
        // Updates the text to specie 3
        infoText.GetComponent<TMP_Text>().text = "Specie 3 selected";
        // Updates all of the input components to the specie 3 settings
        speedSlider.GetComponent<Slider>().value = specie3.speed;
        senseSlider.GetComponent<Slider>().value = specie3.sense;
        sizeSlider.GetComponent<Slider>().value = specie3.size;
        countSlider.GetComponent<Slider>().value = specie3.count;
        energySlider.GetComponent<Slider>().value = specie3.energy;
        aliveToggle.GetComponent<Toggle>().isOn = specie3.alive;
        
    }
    public void toggleUpdater() { // Runs when the enables toggle changes state
        bool value = aliveToggle.GetComponent<Toggle>().isOn; // Gets the state of the toggle

        switch (specieSelected) // Checks which specie is selected
        {
            // Updates the alive value of the selected specie to the new value
            case 1:
                specie1.alive = value;
                break;
            case 2:
                specie2.alive = value;
                break;
            default:
                specie3.alive = value;
                break;
        }
    }
    public void speedUpdater() // Runs when the speed slider is updated
    {
        float value = speedSlider.GetComponent<Slider>().value; // Gets the value in the speed slider

        switch (specieSelected) // Checks which specie is selected
        {
            //Updates the speed value of the selected specie to the new value
            case 1:
                specie1.speed = value;
                break;
            case 2:
                specie2.speed = value;
                break;
            default:
                specie3.speed = value;
                break;
        }
    }
    public void senseUpdater() // Runs when the sense range slider is updated
    {
        float value = senseSlider.GetComponent<Slider>().value; // Gets the value in the sense range slider

        switch (specieSelected) // Checks which specie is selected
        {
            case 1:
                specie1.sense = value;
                break;
            case 2:
                specie2.sense = value;
                break;
            default:
                specie3.sense = value;
                break;
        }
    }
    public void sizeUpdater() // Runs when the size slider is updated
    {
        float value = sizeSlider.GetComponent<Slider>().value; // Gets the value in the size slider

        switch (specieSelected) // Checks which specie is selected
        {
            //Updates the size value of the selected specie to the new value
            case 1:
                specie1.size = value;
                break;
            case 2:
                specie2.size = value;
                break;
            default:
                specie3.size = value;
                break;
        }
    }
    public void countUpdater() // Runs when the creature count slider is updated
    {
        float value = countSlider.GetComponent<Slider>().value; // Gets the value in the creature count slider

        switch (specieSelected) // Checks which specie is selected
        {
            //Updates the count value of the selected specie to the new value
            case 1:
                specie1.count = value;
                break;
            case 2:
                specie2.count = value;
                break;
            default:
                specie3.count = value;
                break;
        }
    }
    public void energyUpdater() // Runs when the creature energy slider is updated
    {
        float value = energySlider.GetComponent<Slider>().value; // Gets the value in the creature count slider

        switch (specieSelected) // Checks which specie is selected
        {
            //Updates the count value of the selected specie to the new value
            case 1:
                specie1.energy = value;
                break;
            case 2:
                specie2.energy = value;
                break;
            default:
                specie3.energy = value;
                break;
        }
    }
}
