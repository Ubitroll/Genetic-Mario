using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    // Variables
    public GameObject startPoint;
    public GameObject finishLine;
    public GameObject marioPrefab;
    public Transform parentGrid;

    public GameObject mario;

    // Start is called before the first frame update
    void Start()
    {
        SpawnMario();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Function to spawn a mario
    public void SpawnMario()
    {
        mario = Instantiate(marioPrefab, startPoint.transform.position, Quaternion.identity); //Spawn mario at set pos
        mario.transform.parent = parentGrid; //Set mario to be a child of the map
        mario.GetComponent<MarioFSM>().finishLine = finishLine;
    }

    // Function to load marios stats
    public void LoadMarioGenome(GenomeDataClass marioGenes)
    {
        

    }
}
