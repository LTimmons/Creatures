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


public class RegisterPassword : MasterPassword { // Declares the Register Password class,
                                                 // inheriting all of the methods from the master passworc class
    public string ConfPassword { get; set; } // Declares the Confirmation Password class (holds the text inputted
                                             // into the confpassword textbox)

    public RegisterPassword(GameObject newPassword, GameObject newConfPassword) { //Declaration for the class, taking two
                                                                                  //GameObject parameters (in this case textboxes)
        generateSalt(); // Triggers salt to be generated
        Password = newPassword.GetComponent<TMP_InputField>().text; // Extracts the text component of the Password textbox and saves it
        ConfPassword = newConfPassword.GetComponent<TMP_InputField>().text;// Extracts the text component of the Confirm Password textbox and saves it
    }
    
    public bool checkEqualPasses() { // Declaration for the validation method that checks if the confimation password matches the password
        if (Password == ConfPassword) return true; // Returns true if they are equal
        else return false; // Returns false if they aren't equal
    }
    public void UpdatePassword(GameObject newPassword, GameObject newConfPassword){ // Declaration for the update method, taking two GameObject
                                                                                    // parameters (In this case the password and confirm password textbox)
        Password = newPassword.GetComponent<TMP_InputField>().text; // Extracts inputted text from password textbox and saves it
        ConfPassword = newConfPassword.GetComponent<TMP_InputField>().text; // Extracts inputted text from confirm password textbox and saves it
        GenerateHash(); // Triggers the updated password to be hashed
    }
    public bool LenCheck(){ // Declaration for the validation method that checks if the inputted password
                            // is long enough. Taking no parameters and returning a boolean
        if (Password.Length < 8){ // Checks if the length of the password is less than 8
            return false; // If it is then the function will return false
        }
        else{
            return true; // If it isn't then the function will return true
        }
    }
    public bool HasCapitals(){ // Declaration for the validation method that checks if the inputted password
                               // contains a capital letter. Taking no parameters and returning a boolean
        foreach (char c in Password) // Loops through each character in the password, saving it in the variable c
        {
            if (char.IsUpper(c)) return true; // Checks if the character is uppercase
                                              // If it is it ends the loop and returns true
                                              // If it isnt it continues to the next character
        }
        return false; // Returns false once all characters are checked and none are uppercase
    }
    public bool hasSpecialChar() { // Declaration for the validation method that checks if the inputted password contains
                                   // a capital letter. Taking no parameters and returning a boolean
        string specials = @"\|!#$%&/()=?��@���{}.-;'<>_,"; // Defines all of the special characters accepted by the program
        foreach (var i in specials) // Loops through all of the special characters, defining them as i
        {
            if (Password.Contains(i)) return true;//Checks if the password contains each character, returning true if it does
        }

        return false;// If none of the characters are contained, it returns false
    }
    public bool validatePassword() { // Declares the overall password validation method, taking no parameters, and returning a boolean
        if (HasCapitals() & hasSpecialChar() & LenCheck()) return true; // If all other validations are passed, then this it returns true
        else return false; // If 1+ methods fail, it returns false
    }
    public void generateSalt() { //Declares the generate salt method, taking no parameters
        RNGCryptoServiceProvider rncCsp = new RNGCryptoServiceProvider(); // instantiates the RNGCryptoServiceProvider class and stores it 
        byte[] salts = new byte[5]; // Declares an array called salts of size 5 of datatype byte
        rncCsp.GetBytes(salts); // Generates 5 random bytes of data
        Salt = Convert.ToBase64String(salts);//Converts each byte into a string and combines the strings
    }
    

}
public class Register : MonoBehaviour // Declares the Login class and inherits from MonoBehabiour
                                      // (standard by unity as this class will be attached to a unity object)
                                      // This class handles everything related to regisration user input
{
    public GameObject username; // Declares a GameObject called username, this is linked to the username textbox on screen
    public MasterUsername LiveUsernameClass; // Declares a master username called LiveUsernameClass, used to hold info on
                                             // the username of the current registration session
    public RegisterPassword LivePasswordClass;// Declares a register password called LivePasswordClass, used to hold info on
                                              // the password of the current registration session
    public GameObject password; // Declares a GameObject called password, this is linked to the password textbox on screen
    public GameObject confPassword; // Declares a GameObject called confPassword, this is linked to the confirmation password textbox on screen
    public GameObject tips; // Declares a GameObject called tips, this is linked to the text element on screen surrounding the register button
    public Toggle studentToggle; // Declares a Toggle called studentToggle, linked to the selection tool responsible for choosing the student role
    public Toggle teacherToggle; // Declares a Toggle called teacherToggle, linked to the selection tool responsible for choosing the teacher role
    public GameObject PasswordCheckerChars; // Declares a GameObject called PasswordCheckerChars, linked to the "8+ characters" text on screen
    public GameObject PasswordCheckerUppers;// Declares a GameObject called PasswordCheckerUppers, linked to the "Uppercase character" text on screen
    public GameObject PasswordCheckerSpecial;// Declares a GameObject called PasswordCheckerSpecial, linked to the "Special character" text on screen
    private string form; // String declared called form, used to write to the user database
    private bool reset=false; // Boolean declared called reset and is set to false, used to communicate that the fields are being reset to prevent responses

    void Start() // Start method executes before the first frame
    {
        LiveUsernameClass = new MasterUsername(username); // Live username instantiated
        LivePasswordClass = new RegisterPassword(password, confPassword); // Live password instantiated
    }


    public void RegisterBtn() { // Method executes when register button is pressed
        tips.GetComponent<TMP_Text>().text = ""; // Tips text is reset to nothing
        if (!(LivePasswordClass.Password != "" & LiveUsernameClass.Username != "" &
            LivePasswordClass.ConfPassword != "" & 
            (studentToggle.isOn == true || teacherToggle.isOn == true))){ // If any of the fields needed are empty or unselected
            tips.GetComponent<TMP_Text>().text = "Please fill all fields"; // Alert the user to fill in the fields, and prevent registration
        }
        else{ // If all fields are filled, it continues
            if (LiveUsernameClass.existing){ // If the username exists in the database
                tips.GetComponent<TMP_Text>().text = "Username Taken"; // Alert the user that their username is taken, and prevent registration
            }
            else // If username not taken, it continues
            {
                if (!LivePasswordClass.checkEqualPasses()){ // Check if the password, and confirm passwords are not equal
                    tips.GetComponent<TMP_Text>().text = "Passwords don't match"; // Alerts the user that they are not, and prevents registration
                }
                else // If the passwords do match, it continues
                {
                    if (!LivePasswordClass.validatePassword()){ // Checks if the password does not meet the requirements
                        tips.GetComponent<TMP_Text>().text = "Weak Password"; // Alerts the user that their password is weak
                    }
                    else // If the password is strong enough, it continues
                    {
                        if (!Directory.Exists(@LiveUsernameClass.path + "/users/")){ // Checks if the users directory exists
                            Directory.CreateDirectory(@LiveUsernameClass.path + "/users/"); // It creates it if it doesnt exist
                        }
                        int roleId = 0; // Sets the role to student by deafault
                        if (teacherToggle.isOn == true) roleId = 1; // If teacher is selected, it switches it to teacher role
                        form = (LiveUsernameClass.Username + "\n" +LivePasswordClass.HashedPassword + "\n"+ 
                            LivePasswordClass.Salt+"\n"+Convert.ToString(roleId)); // Fills out the form to be saved with the users information
                        System.IO.File.WriteAllText(@LiveUsernameClass.path + "/users/" + LiveUsernameClass.Username + ".txt", form); // Saves the form
                                                                                                                                      // to the users file
                        reset=true; // Set to true in order to prevent the toggle handlers from triggering
                        teacherToggle.isOn = false; // Deselects the teacher toggle
                        studentToggle.isOn = false; // Deselects the student toggle
                        reset = false; // Allowing toggle handlers to trigger again
                        username.GetComponent<TMP_InputField>().text= ""; // Resets the username field
                        password.GetComponent<TMP_InputField>().text=""; // Resets the password field
                        confPassword.GetComponent<TMP_InputField>().text=""; // Resets the confirm password field
                        
                        tips.GetComponent<TMP_Text>().text = "Registration complete"; // Alerts the user that registration is complete
                        print("Registration complete to "+ @LiveUsernameClass.path + "/users/"); // Outputs the location of the user directory
                                                                                                 // to the console (for testing purposes)
                        LivePasswordClass.generateSalt();
                    }}}}}

    public void handleTeacherToggle() { // Method declared to handle a response
                                        // to the teacher toggle being pressed 
        if (reset == false) // Preventing a response if the toggles are being
                            // automatically reset
        {
            if (teacherToggle.isOn) // If the teacher toggle is ticked
            {
                studentToggle.isOn = false; // Untick the student toggle
            }
            if (!teacherToggle.isOn) // If the teacher toggle is untickec
            {
                studentToggle.isOn = true; // Tick the student toggle
            }
        }
        // These if statements disallow the user to delselect the toggles, if
        // one is delected, the other will automatically be selected
    }
    public void handleStudentToggle(){ // Method declared to handle a response
                                       // to the student toggle being pressed 
        if (reset == false) // Preventing a response if the toggles are being
                            // automatically reset
        {
            if (studentToggle.isOn) // If the student toggle is ticked
            {
                teacherToggle.isOn = false; // Untick the teacher toggle
            }
            if (!studentToggle.isOn) // If the student toggle is untickec
            {
                teacherToggle.isOn = true; // Tick the teacher toggle
            }
        }
        // These if statements disallow the user to delselect the toggles, if
        // one is delected, the other will automatically be selected
    }

    // Update is called once per frame
    void Update()
    {
        // Script allowing the user to press tab to move through UI elements
        if (Input.GetKeyDown(KeyCode.Tab)) { // If tab is being pressed
            if (username.GetComponent<TMP_InputField>().isFocused) { // If the username box is is selected
                password.GetComponent<TMP_InputField>().Select(); // Select the password box
            }
            if (password.GetComponent<TMP_InputField>().isFocused) // If the password box is selected
            {
                confPassword.GetComponent<TMP_InputField>().Select(); // Select the confirm password box
            }
        }
        LiveUsernameClass.UpdateUsername(username); // Updates the username
        LivePasswordClass.UpdatePassword(password, confPassword); // Updates the passwords
        livePasswordChecker(); // Updates the live password checker
    }

    public void livePasswordChecker() { // Method responsible for updating the live password checker
        if (LivePasswordClass.HasCapitals()) // If the password contains a capital
        {
            PasswordCheckerUppers.GetComponent<TMP_Text>().color = new Color(64f / 255f, 145f / 255f, 52f / 255f); 
            // Sets the "Uppercase character" text to green
        }
        else // If the password contains no capitals
        {
            PasswordCheckerUppers.GetComponent<TMP_Text>().color = Color.red;
            // Sets the "Uppercase character" text to red
        }
        if (LivePasswordClass.LenCheck()) // If the password has 8 or more charactors
        {
            PasswordCheckerChars.GetComponent<TMP_Text>().color = new Color(64f / 255f, 145f / 255f, 52f / 255f);
            // Sets the "8+ characters" text to green
        }
        else // If the password has less then 8 characters
        {
            PasswordCheckerChars.GetComponent<TMP_Text>().color = Color.red;
            // Sets the "8+ characters" text to red

        }
        if (LivePasswordClass.hasSpecialChar()) // If the password contains a special character
        {
            PasswordCheckerSpecial.GetComponent<TMP_Text>().color = new Color(64f / 255f, 145f / 255f, 52f / 255f);
            // Sets the "Special character" text to green
        }
        else // If the password does not contain a special character
        {
            PasswordCheckerSpecial.GetComponent<TMP_Text>().color = Color.red;
            // Sets the "Special character" text to red

        }
    }
}
