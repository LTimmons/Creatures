using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Links to the tool tip text

    // Start is called before the first frame update
    void Start()
    {
        // Ensures that the cursor is active on screen 
        Cursor.visible = true;
        // Ensures that the tooltip box is not visible
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Moves the tooltip to 3 pixels above the location of the mouse every frame
        transform.position = Input.mousePosition + new Vector3(0, 3, 0);
    }

    // Function used to activate the tooltip
    public void set(string text) {
        // Sets the correct text
        textComponent.text = text;
        // Shows the tooltip
        gameObject.SetActive(true);
    }

    // Function used to deactivate the tooltip
    public void hide() {
        // Removes all text from the tooltip
        textComponent.text = string.Empty;
        // Hides the tooltip
        gameObject.SetActive(false);
        
    }
}
