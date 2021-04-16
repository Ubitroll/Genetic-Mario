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

    // Default Constructor
    GenomeDataClass()
    {
        genomeDelayedThreshold = 0;
        genomeLongThreshold = 0;
        genomeDelayedTime = 0;
        genomeForwardRayLength = 0;
        genomeUpRightRayLength = 0;
        genomeDownRightRayLength = 0;
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
    public int getDelayedThreshold()
    {
        return genomeDelayedThreshold;
    }

    public void setDelayedThreshold( int delayedThreshold)
    {
        genomeDelayedThreshold = delayedThreshold;
    }

    // Long Jump Threshold Getter and Setter
    public int getLongJumpThreshold()
    {
        return genomeLongThreshold;
    }

    public void setLongJumpThreshold(int longJumpThreshold)
    {
        genomeLongThreshold = longJumpThreshold;
    }

    // Delayed Time Getter and Setter
    public int getDelayedtime()
    {
        return genomeDelayedTime;
    }

    public void setDelayedTime(int delayedTime)
    {
        genomeDelayedTime = delayedTime;
    }

    // Forward Raycast Length Getter and Setter
    public int getForwardRaycastLength()
    {
        return genomeForwardRayLength;
    }

    public void setForwardRaycastLength(int forwardRaycastLength)
    {
        genomeForwardRayLength = forwardRaycastLength;
    }

    // Forward Up Raycast Length Getter and Setter
    public int getForwardUpRaycastLength()
    {
        return genomeUpRightRayLength;
    }

    public void setForwardUpRaycastLength(int forwardUpRaycastLength)
    {
        genomeUpRightRayLength = forwardUpRaycastLength;
    }

    // Forward Down Raycast Length Getter and Setter
    public int getForwardDownRaycastLength()
    {
        return genomeDownRightRayLength;
    }

    public void setForwardDownRayacastLength(int forwardDownRaycastLength)
    {
        genomeDownRightRayLength = forwardDownRaycastLength;
    }

    // Method to get full gome output as a string
    public string genomeToString()
    {

        string temp;

        temp = genomeDelayedThreshold.ToString() + " " + genomeLongThreshold.ToString() + " " + genomeDelayedTime.ToString() + " " + genomeForwardRayLength.ToString() + " " + genomeUpRightRayLength.ToString() + " " + genomeDownRightRayLength.ToString();

        return temp;
    }
}
