using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Class attatched to objects requiring a tip upon hover
public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Holds the text element of the tool tip
    public string message;
    // Links to the tool tip manager
    private ToolTipManager manager;
    private void Start()
    {
        // Locates the current manager
        manager = GameObject.Find("toolTipBox").GetComponent<ToolTipManager>();
    }
    // Executes when mouse enters the bounds of the element
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Shows the tip and sets the message
        manager.set(message);
    }
    // Executes when the mouse leaves the bounds
    public void OnPointerExit(PointerEventData eventData)
    {
        // Hides the tip
        manager.hide();
    }


}
