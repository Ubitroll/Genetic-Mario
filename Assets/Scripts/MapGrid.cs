using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGrid : MonoBehaviour
{
    public GameObject mapPrefab;
    public Transform parentGrid;

    public float xBound;
    public float yBound;

    public int gridSizeX;
    public int gridSizeY;

    public Vector2 originPos;

    // Start is called before the first frame update
    void Start()
    {
        Tilemap tilemap = mapPrefab.GetComponent<Tilemap>();
        var bounds = tilemap.cellBounds;
        xBound = (Mathf.Abs(bounds.xMax) + Mathf.Abs(bounds.xMin));
        yBound = (Mathf.Abs(bounds.yMax) + Mathf.Abs(bounds.yMin));

        for (int y = 0; y < gridSizeY; y++ )
        {
            for(int x = 0; x < gridSizeX; x++)
            {
                Vector2 pos = new Vector2(originPos.x+(x * xBound), originPos.y+(y * yBound));
                var newMap = Instantiate(mapPrefab, pos, Quaternion.identity);
                newMap.transform.parent = parentGrid;
            }
        }



    }


}
