using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using TMPro;
using System.Security.Cryptography; // Allows hashing algorithms to be used
using System.Text;
using System.IO;

public class MasterPassword{ // Declares the base class for passwords
    public string Password { get; set; } // Declares the password attribute, allowing it to be set or edited
    public string HashedPassword { get; set; } // Declares the hashed password attribiue, allowing it to be set
                                               // or edited
    public string Salt { get; set; } // Declares the salt attribiue, allowing it to be set or edited

    public void GenerateHash() // Declares the generate hash function, taking no paramaters, and returning nothng
    {
        byte[] bytes = Encoding.UTF8.GetBytes(Password + Salt); // Combines password and salt, and converts
                                                                // the result into an array of the byte data type
        SHA256Managed sHA256ManagedString = new SHA256Managed(); // Instantiates the SHA256 class which is
                                                                 // used as the hashing method
        byte[] hash = sHA256ManagedString.ComputeHash(bytes); // Generates the hash using SHA256 and the bytes
                                                              // previously generated
        HashedPassword = Convert.ToBase64String(hash); // Sets HashedPassword as the string result of this hash
    }



    public void UpdatePassword(GameObject newPassword) // Declares the update password function, taking a
                                                       // GameObject parameter (In this case a textbox)
    {
        Password = newPassword.GetComponent<TMP_InputField>().text; // Extracts the text inputted to
                                                                    // the textbox and stores it in Password
        GenerateHash(); // Triggers the newly inputted password to be hashed.
    }
}
