using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject camera; // camera follow
    public float speed = 0.01f;
    private float originalY;
    public float delay = 3;
    private float lastPress;

    // Update is called once per frame
    void Update()
    {
        originalY = camera.transform.position.y;

        if (Input.GetKey(KeyCode.H))
        {
            lastPress = Time.time;
            var pos = camera.gameObject.transform.position;
            camera.transform.position = new Vector3(pos.x, pos.y + speed, pos.z);
        }

        if (Input.GetKey(KeyCode.J))
        {
            var pos = camera.gameObject.transform.position;
            camera.transform.position = new Vector3(pos.x, pos.y - speed, pos.z);
        }

        //if(Time.time - lastPress > delay)
        //{
        //    var pos = camera.gameObject.transform.position;
        //    camera.transform.position = new Vector3(pos.x, originalY, pos.z);
        //}
    }
}
