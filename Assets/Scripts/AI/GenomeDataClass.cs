using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenomeDataClass : MonoBehaviour
{

    // Genome Variables
    public float genomeDelayedTime = 0;
    public float genomeForwardRayLength = 0;
    public float genomeUpRightRayLength = 0;
    public float genomeDownRightRayLength = 0;
    public float genomePreferedForwardRayLength = 0;
    public float genomePreferedUpRightRayLength = 0;
    public float genomePreferedDownRightRayLength = 0;
    public float genomeFitnessScore = 0;

    // Default Constructor
    public GenomeDataClass()
    {
        
    }

    public GenomeDataClass(float delayedTime, float forwardRayLength, float upRightRayLength, float downRightRayLength, float preferedForwardRayLength, float preferedUpRightRaylength, float preferedDownRightRayLength, float theFitnessScore)
    {
        genomeDelayedTime = delayedTime;
        genomeForwardRayLength = forwardRayLength;
        genomeUpRightRayLength = upRightRayLength;
        genomeDownRightRayLength = downRightRayLength;
        genomePreferedForwardRayLength = preferedForwardRayLength;
        genomePreferedUpRightRayLength = preferedUpRightRaylength;
        genomePreferedDownRightRayLength = preferedDownRightRayLength;
        genomeFitnessScore = theFitnessScore;
    }

    public GenomeDataClass(float delayedTime, float forwardRayLength, float upRightRayLength, float downRightRayLength, float preferedForwardRayLength, float preferedUpRightRaylength, float preferedDownRightRayLength)
    {
        genomeDelayedTime = delayedTime;
        genomeForwardRayLength = forwardRayLength;
        genomeUpRightRayLength = upRightRayLength;
        genomeDownRightRayLength = downRightRayLength;
        genomePreferedForwardRayLength = preferedForwardRayLength;
        genomePreferedUpRightRayLength = preferedUpRightRaylength;
        genomePreferedDownRightRayLength = preferedDownRightRayLength;
    }

    // Delayed Time Getter and Setter
    public float GetDelayedTime()
    {
        return genomeDelayedTime;
    }

    public void SetDelayedTime(float delayedTime)
    {
        genomeDelayedTime = delayedTime;
    }

    // Forward Raycast Length Getter and Setter
    public float GetForwardRaycastLength()
    {
        return genomeForwardRayLength;
    }

    public void SetForwardRaycastLength(float forwardRaycastLength)
    {
        genomeForwardRayLength = forwardRaycastLength;
    }

    // Forward Up Raycast Length Getter and Setter
    public float GetForwardUpRaycastLength()
    {
        return genomeUpRightRayLength;
    }

    public void SetForwardUpRaycastLength(float forwardUpRaycastLength)
    {
        genomeUpRightRayLength = forwardUpRaycastLength;
    }

    // Forward Down Raycast Length Getter and Setter
    public float GetForwardDownRaycastLength()
    {
        return genomeDownRightRayLength;
    }

    public void SetForwardDownRayacastLength(float forwardDownRaycastLength)
    {
        genomeDownRightRayLength = forwardDownRaycastLength;
    }

    // Prefered Forward Raycast Length Getter and Setter
    public float GetPreferedForwardRaycastLength()
    {
        return genomePreferedForwardRayLength;
    }

    public void SetPreferedForwardRaycastLength(float forwardPreferedRaycastLength)
    {
        genomePreferedForwardRayLength = forwardPreferedRaycastLength;
    }

    // Forward Up Raycast Length Getter and Setter
    public float GetPreferedForwardUpRaycastLength()
    {
        return genomePreferedUpRightRayLength;
    }

    public void SetPreferedForwardUpRaycastLength(float forwardPreferedUpRaycastLength)
    {
        genomePreferedUpRightRayLength = forwardPreferedUpRaycastLength;
    }

    // Forward Down Raycast Length Getter and Setter
    public float GetPreferedForwardDownRaycastLength()
    {
        return genomePreferedDownRightRayLength;
    }

    public void SetPreferedForwardDownRayacastLength(float forwardPreferedDownRaycastLength)
    {
        genomePreferedDownRightRayLength = forwardPreferedDownRaycastLength;
    }

    // Fitness Score Getter and Setter
    public float GetFitnessScore()
    {
        return genomeFitnessScore;
    }

    public void SetFitnessScore(float theFitnessScore)
    {
        genomeFitnessScore = theFitnessScore;
    }

    // Method to get full gome output as a string
    public string genomeToString()
    {

        string temp;

        temp = genomeDelayedTime.ToString() + " " + genomeForwardRayLength.ToString() + " " + genomeUpRightRayLength.ToString() + " " + genomeDownRightRayLength.ToString() + " " + genomePreferedForwardRayLength.ToString() + " " + genomePreferedUpRightRayLength.ToString() + " " + genomePreferedDownRightRayLength.ToString();

        return temp;
    }
}