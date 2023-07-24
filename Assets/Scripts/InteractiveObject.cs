using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class InteractiveObject : MonoBehaviour
{
    private static InteractiveObject selectedObject;
    private bool isHovered;
    private Color originalColor;
    private Renderer objectRenderer;
    private GameObject panel;
    private TextMeshProUGUI objectName;

    public PropertyValues OGValues;
    public PropertyValues currentValues;
    public Vector3 OGScale;
    public PlayerMovement playerMovement;
    public List<InteractableProperties> currentPowers;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
        panel = GameObject.FindWithTag("Panel");
        objectName = panel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        OGScale = this.transform.localScale;

        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();

    }

    void Awake()
    {
        currentValues = new PropertyValues(
            //gameObject.name,
            OGValues.Kinematic,
            OGValues.ReverseGravity,
            OGValues.Size,
            OGValues.Solid);

    }

    private void Update()
    {

        if (selectedObject == null)
        {
            panel.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeselectAll();
        }

        if (isHovered && selectedObject != this)
        {
            // Light up the object when hovered over, unless it is the selected object
            objectRenderer.material.color = Color.yellow;
        }
        else if (selectedObject == this)
        {
            // Set the color to red for the selected object
            objectRenderer.material.color = Color.red;
        }
        else
        {
            // Restore the original color when not hovered over or selected
            objectRenderer.material.color = originalColor;
        }

    }

    private void OnMouseEnter()
    {
        isHovered = true;
    }

    private void OnMouseExit()
    {
        isHovered = false;
    }

    private void OnMouseDown()
    {
        if (selectedObject != null)
        {
            // Restore the original color of the previously selected object
            selectedObject.RestoreColor();
        }

        // Change color when clicked
        objectRenderer.material.color = Color.red;
        selectedObject = this;
        BuildPanel(selectedObject);

    }

    private void RestoreColor()
    {
        // Restore the original color
        objectRenderer.material.color = originalColor;
    }

    private void BuildPanel(InteractiveObject currentObject)
    {
        currentPowers = playerMovement.GetList();
        //Debug.Log($"{currentValues.Owner} - {currentValues.Kinematic}");
        ResetPanel();
        panel.SetActive(true);
        objectName.text = currentObject.name;

        //for each create a "button" for the property that is there...
        foreach(InteractableProperties powerUp in currentPowers)
        {
            var prop = panel.transform.GetChild((int)powerUp);
            prop.gameObject.SetActive(true);

            if (currentObject != null && currentObject.currentValues!=null)
            {
                switch (powerUp)
                {
                    case InteractableProperties.Gravity:
                        Toggle gravityToggle = prop.GetComponentInChildren<Toggle>();
                        //Debug.Log($"Toggle is on: {gravityToggle.isOn}, values: {currentObject.currentValues.Kinematic} for {currentObject.gameObject.name}");
                        gravityToggle.SetIsOnWithoutNotify(currentObject.currentValues.Kinematic);
                        break;

                    case InteractableProperties.ReverseGravity:
                        Toggle reverseGravityToggle = prop.GetComponentInChildren<Toggle>();
                        reverseGravityToggle.SetIsOnWithoutNotify(currentObject.currentValues.ReverseGravity);
                        break;

                    case InteractableProperties.Size:
                        Slider sizeSlider = prop.GetComponentInChildren<Slider>();
                        sizeSlider.SetValueWithoutNotify(currentObject.currentValues.Size);
                        break;


                    case InteractableProperties.Solid:
                        Toggle solidToggle = prop.GetComponentInChildren<Toggle>();
                        solidToggle.SetIsOnWithoutNotify(currentObject.currentValues.Solid);
                        break;
                }
            }
        }
    }

    private void ResetPanel()
    {
        panel.SetActive(false);
        for (int i = 1; i < panel.transform.childCount; i++)
        {
            panel.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public static void DeselectAll()
    {
        if (selectedObject != null)
        {
            selectedObject.RestoreColor();
            selectedObject = null;
        }
    }

    public static InteractiveObject SelectedObject
    {
        get { return selectedObject; }
    }
}


public enum InteractableProperties
{
    Gravity = 1,        //turn on off (kinematic)
    ReverseGravity = 2, // reverse gravity
    Solid = 4,          // turn off/on solidity (istrigger)
    Size = 3            // change size of object...
    //...
}

