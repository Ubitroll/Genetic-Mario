using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OnMouseOver : MonoBehaviour
{
    public Color startColor;
    public Color highlightColor;
    public Tilemap tilemap;
    public Transform playerTransform;
    

    void OnMouseEnter()
    {
        startColor = tilemap.color;
        tilemap.color = highlightColor;
        print(tilemap.transform.TransformPoint(tilemap.cellBounds.center).y);
        
    }
    void OnMouseExit()
    {
        tilemap.color = startColor;
       
    }
    void OnMouseUp()
    {
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().focusTarget = playerTransform;
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().focus = true;
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().mapY = tilemap.transform.TransformPoint(tilemap.cellBounds.center).y;
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().minX = tilemap.transform.TransformPoint(tilemap.cellBounds.min).x;
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().maxX = tilemap.transform.TransformPoint(tilemap.cellBounds.max).x;

        ;
    }
}
