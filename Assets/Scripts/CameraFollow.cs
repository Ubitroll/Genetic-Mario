using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class CameraFollow : MonoBehaviour
{
    public Transform focusTarget;
    public Transform unfocusTarget;

    public float mapY;
    public float minX;
    public float maxX;
  
    public float cameraClampOffset;

    public Vector3 offset;
    public bool focus = false;

    public int focusPPU = 16;
    public int unfocusPPU = 4;

    public float transition;
    public float animTime;

    // Update is called once per frame
    void Update()
    {
        
        if(focus)
        {
            gameObject.GetComponent<PixelPerfectCamera>().assetsPPU = 14;
            //float targetX = Mathf.Max(minX, Mathf.Min(maxX, focusTarget.transform.position.x));
            cameraClampOffset = Camera.main.orthographicSize * Screen.width / Screen.height;
            transform.position = new Vector3(Mathf.Clamp(focusTarget.position.x, minX + cameraClampOffset, maxX - cameraClampOffset), mapY, focusTarget.position.z + offset.z);

        }
        if (!focus)
        { 
            gameObject.GetComponent<PixelPerfectCamera>().assetsPPU = 4;
            transform.position = unfocusTarget.position + offset;
        }

        
    }

    public void Focused()
    {
        focus = false;
    }

}
