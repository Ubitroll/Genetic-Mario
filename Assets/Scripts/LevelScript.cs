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

    public GameObject marioClone;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnMario();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Function to spawn a mario
    public void SpawnMario()
    {
        marioClone = Instantiate(marioPrefab, startPoint.transform.position, Quaternion.identity); //Spawn mario at set pos
        marioClone.transform.parent = parentGrid; //Set mario to be a child of the map
        marioClone.GetComponent<MarioFSM>().finishLine = finishLine;
    }

    // Function to load marios stats
    public void LoadMarioGenome(GenomeDataClass marioGenes)
    {
        marioClone.GetComponent<MarioFSM>().jumpDelayTime = marioGenes.GetDelayedTime();
        marioClone.GetComponent<MarioFSM>().forwardRaycastLength = marioGenes.GetForwardRaycastLength();
        marioClone.GetComponent<MarioFSM>().upRightRaycastLength = marioGenes.GetForwardUpRaycastLength();
        marioClone.GetComponent<MarioFSM>().downRightRaycastLength = marioGenes.GetForwardDownRaycastLength();
        marioClone.GetComponent<MarioFSM>().preferedForwardRayDistance = marioGenes.GetPreferedForwardRaycastLength();
        marioClone.GetComponent<MarioFSM>().preferedUpRightRayDistance = marioGenes.GetPreferedForwardUpRaycastLength();
        marioClone.GetComponent<MarioFSM>().preferedDownRightRayDistance = marioGenes.GetPreferedForwardDownRaycastLength();

    }
}
