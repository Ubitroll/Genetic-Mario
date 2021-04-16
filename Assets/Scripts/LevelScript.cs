using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    // Variables
    public GameObject startPoint;
    public GameObject finishLine;
    public GameObject mario;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Function to spawn a mario
    private void SpawnMario()
    {
        Instantiate(mario, startPoint.transform);
    }

    // Function to load marios stats
    private void LoadMarioGenome(int delayedThreshold, int longThreshold, int delayedTime, int forwardRayLength, int upRightRayLength, int downRightRayLength)
    {
        mario.GetComponent<MarioFSM>().isGenerationZero = false;

        mario.GetComponent<MarioFSM>().delayedJumpThreshold = delayedThreshold;
        mario.GetComponent<MarioFSM>().longJumpThreshold = longThreshold;
        mario.GetComponent<MarioFSM>().jumpDelayTime = delayedTime;
        mario.GetComponent<MarioFSM>().forwardRaycastLength = forwardRayLength;
        mario.GetComponent<MarioFSM>().upRightRaycastLength = upRightRayLength;
        mario.GetComponent<MarioFSM>().downRightRaycastLength = downRightRayLength;

    }
}
