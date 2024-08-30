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
public class TerrainSettings : MonoBehaviour
{
    public GameObject maxResourceSlider; // Links the code to the max resources slider
    public GameObject resourceLifespanSlider; // Links the code to the life span slider
    public resourceData resource; // Record used to hold the inputted resource settings


    public record resourceData() // Record used to store the resource settings
    {
        public int max = 30;
        public int lifespan = 30;
    }

    // Start is called before the first frame update
    void Start()
    {
        resource = new resourceData(); // Instantiates a record for resource settings
    }
    public void maxresourceUpater() // Triggers when max slider is touched
    {
        // Updates the max value in the record
        int value = Convert.ToInt32(maxResourceSlider.GetComponent<Slider>().value);
        resource.max = value;
    }
    public void resourceLifespanUpdater() // Triggers when lifespan slider is touched
    {
        // Updates the lifespan value in the record
        int value = Convert.ToInt32(resourceLifespanSlider.GetComponent<Slider>().value);
        resource.lifespan = value;
    }

}
