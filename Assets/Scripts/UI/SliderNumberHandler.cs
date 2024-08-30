using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SliderNumberHandler : MonoBehaviour // Class assigned to each slider
{
    public GameObject slider; // Creates a connection with the slider
    public GameObject numberValue; // Creates a connection with the number above the slider
    public int defaultValue= 0; // The default value of the slider
    public int value;
    public void ResetSlider() {
        slider.GetComponent<Slider>().value = defaultValue;
        // Sets the the value of the slider to the default value
        numberValue.GetComponent<TMP_InputField>().text = Convert.ToString(defaultValue);
        // Sets the value of the text input to the default value
    }
    // Start is called on the first frame
    public void Start()
    {
        //ResetSlider(); // Sets the slider to the default value
        SliderUpdate(); // Updates the slider on the first frame
    }
    public void SliderUpdate() // This function runs when the slider is updated
    {
        numberValue.GetComponent<TMP_InputField>().text = Convert.ToString(Convert.ToInt32(slider.GetComponent<Slider>().value)); 
        value = (Convert.ToInt32(slider.GetComponent<Slider>().value));
        // Sets the number above the slider to the value of the slider
        // Converts from float to integer to string to ensure that it displays whole numbers rather than decimals
    }
    public void textInputUpdate() { // Executes when the value in the textbox changes
        try
        {
            if (slider.GetComponent<Slider>().minValue > Convert.ToInt32(numberValue.GetComponent<TMP_InputField>().text))
            // If the input is lower than the minimum value
            {
                // Set all values to the minimum value
                slider.GetComponent<Slider>().value = slider.GetComponent<Slider>().minValue;
                numberValue.GetComponent<TMP_InputField>().text = Convert.ToString(slider.GetComponent<Slider>().minValue);
            }
            else if (slider.GetComponent<Slider>().maxValue < Convert.ToInt32(numberValue.GetComponent<TMP_InputField>().text))
            // If the input is higher than the maximum value
            {
                // Set all values to the maximum value
                slider.GetComponent<Slider>().value = slider.GetComponent<Slider>().maxValue;
                numberValue.GetComponent<TMP_InputField>().text = Convert.ToString(slider.GetComponent<Slider>().maxValue);
            }
            else // If the input is valid
            {
               // Set the slider value to the input
               slider.GetComponent<Slider>().value = Convert.ToInt32(numberValue.GetComponent<TMP_InputField>().text);
            }
        }
        catch // If the program fails to convert input to an integer (bad user input)
        {
            if (!(numberValue.GetComponent<TMP_InputField>().text == "")) // If the field isnt empty
            {
                // Sets the slider back to default
                ResetSlider();
            }
            else{ // If the field is empty
                // Set the values to 0
                slider.GetComponent<Slider>().value = 0;
                numberValue.GetComponent<TMP_InputField>().text = "0";
            }
        }
    }


    public void textInputDeselect() { // Executes when the user clicks off the textbox
        if ((numberValue.GetComponent<TMP_InputField>().text == "")) // If the textbox is empty
        {
            // Reset everything to default
            ResetSlider();
        }
    }
}
