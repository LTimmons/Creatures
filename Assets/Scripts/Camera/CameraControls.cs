using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public CharacterController characterController; // Links the camera to its XYZ 
    float horizontalRotation = 0.00f; // Float variable declared, set to 0f
    float verticalRotation = 0.00f; // Float variable declared, set to 0f
    bool locked = true; // Boolean variable declared, set to true
    [Header("Camera Settings")]
    public float mouseSensitivity = 5f; // Float variable declared, set to 5f
    public float MovementSpeed = 15; // Float variable declared, set to 15


    void Start()  // Start is called before the first frame update
    {   
        transform.position = new Vector3(256, this.transform.position.y, this.transform.position.z); 
        // Default position of camera set
    }

    void cameraMove() { // Method responsible for camera movement, executes every frame
        float horizontalMove = Input.GetAxis("Horizontal") * MovementSpeed; // A and D input taken, D
                                                                            // increases the number, A decreases it
        float verticalMove = Input.GetAxis("Vertical") * MovementSpeed; // W and S input taken, W
                                                                        // increases the number, S decreases it
        Vector3 Move = (transform.right * horizontalMove + transform.forward * verticalMove); // Creates a new vector with the movements
                                                                                              // added to the current location
                                                                                              // Relative to camera direction
        characterController.Move(Move * MovementSpeed * Time.deltaTime); // Makes the camera move to the new position
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -256, 768), 177, Mathf.Clamp(transform.position.z, -256, 768));
        // Clamps the cameras position so that it cannot move outside of the allowed region.
    }
    void cameraRotate(){ // Method responsible for camera rotation, executes every frame
        horizontalRotation += mouseSensitivity * Input.GetAxis("Mouse X"); // Adds the new mouse x position to the current rotation
        verticalRotation -= mouseSensitivity * Input.GetAxis("Mouse Y"); // Adds the new mouse y position to the current rotation
        if (verticalRotation > 90f) { // If statement to prevent the camera from rotating too far vertically
            verticalRotation = 90f;
        }
        else if(verticalRotation < -90f){
            verticalRotation = -90f;
        }
        transform.eulerAngles = new Vector3(verticalRotation, horizontalRotation, 0f); // Sets the camera angle to the newly generated one
    }


    void Update(){ // This function executes every frame
        if (Input.GetKeyUp(KeyCode.F5)) { // If F5 is being pressed
            if (locked == false) // if locked is false
            {
                Cursor.lockState = CursorLockMode.None; // unlock the mouse
            }
            else {
                Cursor.lockState = CursorLockMode.Locked; // lock the mouse
            }
            locked = !locked; // invert the state of locked
        }
        if (locked == false) // if camera is unlocked
        {
            cameraMove(); // move the camera
            cameraRotate(); // rotate the camera
        }
    }
}
