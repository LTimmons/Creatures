using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using TMPro;
using System.Text;
using System.IO;
public class MasterUsername {  // Declares the base Username class
    public string Username { get; set; } // Declares the Username attribute, allowing it to be
                                         // set or edited
    public string path { get; set; } // Declares the path attribute, allowing it to be set or
                                     // edited
    public bool existing { get; set; } // Declares the existing attribute, allowing it to be
                                       // set or edited
    public MasterUsername(GameObject newUsername) // Declares the constructor method for the
                                                  // class, taking a GameObject as a parameter
                                                  // (In this case a TextBox)
    {
        path = Convert.ToString(Path.GetDirectoryName(Application.dataPath)); // Located where
                                                                              // the program is running
                                                                              // and stores the location
        UpdateUsername(newUsername); // Executes the Update Username method in order to save the lastest
                                     // inputted username
            
      }
    public void UpdateUsername(GameObject newUsername) // Declares the update username method for the class,
                                                       // taking a GameObject as a parameter (In this case a TextBox)
    {
        Username = newUsername.GetComponent<TMP_InputField>().text; // Extracts and saves the text inputted
                                                                    // to the textbox and stores it in Username
        checkExisting(); // Executes the checkExisting method to ensure that the existing attribute is always up to date
    }
    public void checkExisting() // Declares the checke existing method, taking no parameters
    {
        if (System.IO.File.Exists(@path + "/users/" + Username + ".txt")) // Uses a c# function "Exists"
                                                                          // to find out if the given username
                                                                          // has an existing save
        {
            existing = true; // Existing is true if it does
        }
        else
        {
            existing = false; // Existing is false if it doesn't
        }

    }
}
