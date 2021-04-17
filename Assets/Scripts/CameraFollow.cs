using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class CameraFollow : MonoBehaviour
{
    public Transform focusTarget; //What mario does the camera focus on
    public Transform unfocusTarget;//The overview view

    public float mapY; //The center of the map in focus view
    public float minX; //The min bound of the map
    public float maxX; //The max bound of the map

    public float cameraClampOffset; //The distance from the cameraview to the edge of the screen

    public Vector3 offset;
    public bool focus = false;

    public int focusPPU = 16; //Zoom level
    public int unfocusPPU = 4;

    public float transition;
    public float animTime;

    // Update is called once per frame
    void Update()
    {
        
        if(focus) //When focused on one mario
        {
            gameObject.GetComponent<PixelPerfectCamera>().assetsPPU = 14; //Change zoom level
            //float targetX = Mathf.Max(minX, Mathf.Min(maxX, focusTarget.transform.position.x));
            cameraClampOffset = Camera.main.orthographicSize * Screen.width / Screen.height; //Set the clamp to the new camera width
            transform.position = new Vector3(Mathf.Clamp(focusTarget.position.x, minX + cameraClampOffset, maxX - cameraClampOffset), mapY, focusTarget.position.z + offset.z); //Set the position to follow mario

        }
        if (!focus)//When not focused
        { 
            gameObject.GetComponent<PixelPerfectCamera>().assetsPPU = 0; //Change zoom level
            transform.position = unfocusTarget.position + offset; //Set position
        }

        
    }

    public void Focused()
    {
        focus = false;
    }

}
