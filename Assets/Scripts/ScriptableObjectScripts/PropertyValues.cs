using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Property Values", menuName = "Propery Values")]
public class PropertyValues : ScriptableObject
{

    //public string Owner;
    public bool Kinematic;
    public bool ReverseGravity;
    public float Size;
    public bool Solid;



    //...



    public PropertyValues(bool Kinematic , bool reverseGravity, float size, bool solid)
    {
        //Owner = owner;
        this.Kinematic = Kinematic;
        this.ReverseGravity = reverseGravity;
        this.Size = size;
        this.Solid = solid;

    }

}

