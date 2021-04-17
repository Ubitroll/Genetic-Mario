using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenomeDataClass : MonoBehaviour
{

    // Genome Variables
    public int genomeDelayedThreshold;
    public int genomeLongThreshold;
    public int genomeDelayedTime;
    public int genomeForwardRayLength;
    public int genomeUpRightRayLength;
    public int genomeDownRightRayLength;
    public float genomeFitnessScore;

    // Default Constructor
    public GenomeDataClass()
    {
        
    }

    public GenomeDataClass(int delayedThreshold, int longThreshold, int delayedTime, int forwardRayLength, int upRightRayLength, int downRightRayLength, float theFitnessScore)
    {
        genomeDelayedThreshold = delayedThreshold;
        genomeLongThreshold = longThreshold;
        genomeDelayedTime = delayedTime;
        genomeForwardRayLength = forwardRayLength;
        genomeUpRightRayLength = upRightRayLength;
        genomeDownRightRayLength = downRightRayLength;
        genomeFitnessScore = theFitnessScore;
    }

    public GenomeDataClass(int delayedThreshold, int longThreshold, int delayedTime, int forwardRayLength, int upRightRayLength, int downRightRayLength)
    {
        genomeDelayedThreshold = delayedThreshold;
        genomeLongThreshold = longThreshold;
        genomeDelayedTime = delayedTime;
        genomeForwardRayLength = forwardRayLength;
        genomeUpRightRayLength = upRightRayLength;
        genomeDownRightRayLength = downRightRayLength;
    }

    // Delayed Threshold Getter and Setter
    public int GetDelayedThreshold()
    {
        return genomeDelayedThreshold;
    }

    public void SetDelayedThreshold( int delayedThreshold)
    {
        genomeDelayedThreshold = delayedThreshold;
    }

    // Long Jump Threshold Getter and Setter
    public int GetLongJumpThreshold()
    {
        return genomeLongThreshold;
    }

    public void SetLongJumpThreshold(int longJumpThreshold)
    {
        genomeLongThreshold = longJumpThreshold;
    }

    // Delayed Time Getter and Setter
    public int GetDelayedtime()
    {
        return genomeDelayedTime;
    }

    public void SetDelayedTime(int delayedTime)
    {
        genomeDelayedTime = delayedTime;
    }

    // Forward Raycast Length Getter and Setter
    public int GetForwardRaycastLength()
    {
        return genomeForwardRayLength;
    }

    public void SetForwardRaycastLength(int forwardRaycastLength)
    {
        genomeForwardRayLength = forwardRaycastLength;
    }

    // Forward Up Raycast Length Getter and Setter
    public int GetForwardUpRaycastLength()
    {
        return genomeUpRightRayLength;
    }

    public void SetForwardUpRaycastLength(int forwardUpRaycastLength)
    {
        genomeUpRightRayLength = forwardUpRaycastLength;
    }

    // Forward Down Raycast Length Getter and Setter
    public int GetForwardDownRaycastLength()
    {
        return genomeDownRightRayLength;
    }

    public void SetForwardDownRayacastLength(int forwardDownRaycastLength)
    {
        genomeDownRightRayLength = forwardDownRaycastLength;
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

        temp = genomeDelayedThreshold.ToString() + " " + genomeLongThreshold.ToString() + " " + genomeDelayedTime.ToString() + " " + genomeForwardRayLength.ToString() + " " + genomeUpRightRayLength.ToString() + " " + genomeDownRightRayLength.ToString();

        return temp;
    }
}