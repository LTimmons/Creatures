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
using System.Linq;
using UnityEngine.SceneManagement; 
public class LoginPassword : MasterPassword{ // Declares the login password class that
                                             // inherits from the master password class
    public LoginPassword(GameObject newPassword) // Declares the class constructor, taking
                                                 // one GameObject parameter (in this case a textbox)
    {
        UpdatePassword(newPassword); // Triggers the update password function (found in the base class),
                                     // giving newPassword as the parameter
    }
    public bool CompareHashes(string hashedInput) // Declares the function compare
                                                  // hashes, taking a string parameter.
    {
        GenerateHash(); // Triggers a the most recently inputted password to be hashed

        return HashedPassword.Equals(hashedInput); // Checks if the new hash is equal to the parameter, and
                                                   // returns boolean statement based on that.
    }
}
public class Login : MonoBehaviour // Declares the Login class and inherits from MonoBehabiour
                                   // (standard by unity as this class will be attached to a unity object)
{
    public GameObject username; // Declares public username GameObject, this is attached to the username textbox in unity
    public GameObject password; // Declares public password GameObject, this is attached to the password textbox in unity
    public MasterUsername LiveUsernameClass; // Declares the object used to handle username information and methods
    public LoginPassword LivePasswordClass; // Declares the object used to handle passworrd information and methods
    public GameObject tips; // Declares the public tips GameObject, this is attached to a piece of text above the login button
    private string form; // Declares a private string called form


    void Start(){ // Declares Start method, Start is called before the first frame update
        // Instantiates both Username and Password classes 
        
        LiveUsernameClass = new MasterUsername(username);
        LivePasswordClass = new LoginPassword(password);
        Debug.Log("ins");
    }

    public void loginBtn() { // Declares login button method, called when login button pressed
        if (LiveUsernameClass.Username == "" || LivePasswordClass.Password == "") // Validating if there is any missing data
        {
            tips.GetComponent<TMP_Text>().text = "Please fill both fields"; // Alerting the user of any missing data if it is found, and preventing login
        }
        else { // Moving on if there is no missing data
            if (!LiveUsernameClass.existing) // Validating that the username inputted has been registered
            {
                tips.GetComponent<TMP_Text>().text = "That user doesn't exist"; // Alerting the if the user doesn't exist, and preventing login
            }
            else { // Moving on if user does exist
                
                String CorrectHashedPassword = File.ReadLines(LiveUsernameClass.path + "/users/"
                    + LiveUsernameClass.Username + ".txt").Skip(1).Take(1).First(); // Combines the username with the path of the user files
                                                                                    // Reads the contents of the users file
                                                                                    // Skips the first line, takes the second like (Essentially extracting the password)
                                                                                    // Stores the result as a string

                LivePasswordClass.Salt = File.ReadLines(LiveUsernameClass.path + "/users/" // Does the same, however skips the first two lines to extract the saved salt
                    + LiveUsernameClass.Username + ".txt").Skip(2).Take(1).First();        // Stores the salt in the Live Password salt attribute

                if (!LivePasswordClass.CompareHashes(CorrectHashedPassword)) // Validates that the user inputted the correct password
                {
                    tips.GetComponent<TMP_Text>().text = "Incorrect password"; // Alerting the user if the password is incorrect, and prevents login
                }
                else { // Allows login
                    tips.GetComponent<TMP_Text>().text = "Login successful"; // Alerts the user that their login was successful
                    username.GetComponent<TMP_InputField>().text=""; // Empties all of the text components
                    password.GetComponent<TMP_InputField>().text="";
                    SceneManager.LoadScene("Main Menu"); // Loads the main menu

                }
            }
        }
    }

    
    void Update() // This is the clock of the login system, it is executed every frame
    {
        // Script allowing the user to press tab to move through UI elements
        if (Input.GetKeyDown(KeyCode.Tab)) // Checks if tab is being pressed
        {
            if (username.GetComponent<TMP_InputField>().isFocused) // If it is, it checks if the username textbox is focused
            {
                password.GetComponent<TMP_InputField>().Select(); // If it is, if moves focus to the password textbox
            }
           
        }
        LiveUsernameClass.UpdateUsername(username); // Updates the username
        LivePasswordClass.UpdatePassword(password); // Updates the password
    }
}
