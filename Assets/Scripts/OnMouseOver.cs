using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OnMouseOver : MonoBehaviour
{
    public Color startColor; //Origional color
    public Color highlightColor; //Highlight color
    public Tilemap tilemap; //The map
    public Transform playerTransform; //The child player transform

    private void Start()
    {
        
    }
    private void Update()
    {
        playerTransform = this.gameObject.GetComponent<LevelScript>().mario.transform; //Set the player transform to mario object
    }

    void OnMouseEnter()//When hover over
    {
        startColor = tilemap.color; //Get origional color 
        tilemap.color = highlightColor; //Highlight map
        
    }
    void OnMouseExit() //On hover exit
    {
        tilemap.color = startColor;//Set map color back to normal
       
    }
    void OnMouseUp() //When clicked
    {
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().focusTarget = playerTransform; //Send transform to camera follow class
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().focus = true; //Set focus to true
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().mapY = tilemap.transform.TransformPoint(tilemap.cellBounds.center).y; //Get the center of the map clicked on
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().minX = tilemap.transform.TransformPoint(tilemap.cellBounds.min).x; //Get the bounds of the map clicked on
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().maxX = tilemap.transform.TransformPoint(tilemap.cellBounds.max).x; // ^^^^^^^

        ;
    }
}
