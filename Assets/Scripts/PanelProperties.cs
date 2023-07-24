using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PanelProperties : MonoBehaviour
{
    InteractiveObject currentObject;

    public ForceMode VelocityChange;

    public void ToggleGravity()
    {

        currentObject = InteractiveObject.SelectedObject;

        if (currentObject == null)
        {
            return;
        }

        currentObject.currentValues.Kinematic = !currentObject.currentValues.Kinematic;
        //Debug.Log($"Toggle gravity, current object: {currentObject.name}");    

        currentObject.GetComponent<Rigidbody>().isKinematic = !currentObject.currentValues.Kinematic;
        //Debug.Log($"After toggle gravity for {currentObject.gameObject.name} - Values: {currentObject.currentValues.Kinematic}, Actual: {!currentObject.GetComponent<Rigidbody>().isKinematic}. (Owner: {currentObject.currentValues.Owner})");

        if (currentObject.currentValues.ReverseGravity)
        {
            currentObject.GetComponent<Rigidbody>().AddForce(0, 1000, 0, VelocityChange);
        }
    }

    public void ReverseGravity()
    {
        currentObject = InteractiveObject.SelectedObject;

        if (currentObject != null)
        {
            currentObject.currentValues.ReverseGravity = !currentObject.currentValues.ReverseGravity;

            if (currentObject.currentValues.ReverseGravity)
            {
                currentObject.GetComponent<Rigidbody>().useGravity = false;
                currentObject.GetComponent<Rigidbody>().AddForce(0, 100, 0 , VelocityChange);
            }

            if (!currentObject.currentValues.ReverseGravity)
            {
                currentObject.GetComponent<Rigidbody>().useGravity = true;
                //currentObject.GetComponent<Rigidbody>().velo
            }
        }
    }

    public void ToggleSolid()
    {
        currentObject = InteractiveObject.SelectedObject;
        currentObject.currentValues.Solid = !currentObject.currentValues.Solid;

        currentObject.GetComponent<Collider>().isTrigger = !currentObject.currentValues.Solid;

    }



    public void ChangeSize()
    {

        currentObject = InteractiveObject.SelectedObject;
        Slider sizeSlider = FindFirstObjectByType<Slider>();
        currentObject.currentValues.Size = sizeSlider.value;

        currentObject.transform.localScale = new Vector3(currentObject.OGScale.x * currentObject.currentValues.Size, currentObject.OGScale.y * currentObject.currentValues.Size, currentObject.OGScale.z * currentObject.currentValues.Size);

    }

    // Time:
    // slider - only on moving objects (patrol) change movement speed...


}
