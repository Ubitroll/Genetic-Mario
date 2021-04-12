using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioFSM : MonoBehaviour
{
    // Variables

    // Raycast Variables
    private int forwardRaycastLength;
    private int upRightRaycastLength;
    private int downRightRaycastLength;
    private int verticalRaycastLength;
    Rigidbody2D rigidBody;

    // Fuzzy logic Variables
    // Make new Random
    private float randomGeneratedNumber;
    public int jumpFuzzyStart;
    public int jumpFuzzyEnd;

    // AI states
    protected enum state
    {
        WalkRight,
        MeetEnemy,
        MeetObstacle,
        Jump,
        Dead
    }

    // Denotes the current state
    private state currentState;

    
    // Start is called before the first frame update
    void Start()
    {
        SetStartRandomValues();

        // Set Rigid Body
        rigidBody = GetComponent<Rigidbody2D>();

        // Set starting state
        currentState = state.WalkRight;
    }

    // Update is called once per frame
    void Update()
    {
        // If in the walking right state
        if (currentState == state.WalkRight)
        {
            //// If forward raycast hits an obstacle
            //if ()
            //{

            //}

            // If Mario dies
            if (this.GetComponent<Mario>().marioDead == true)
            {
                currentState = state.Dead;
            }
        }
        // If AI meets an enemy
        else if (currentState == state.MeetEnemy)
        {

            // If Mario dies
            if (this.GetComponent<Mario>().marioDead == true)
            {
                currentState = state.Dead;
            }
        }
        // If AI meets an obstacle
        else if (currentState == state.MeetObstacle)
        {

            // If Mario dies
            if (this.GetComponent<Mario>().marioDead == true)
            {
                currentState = state.Dead;
            }
        }
        // If the AI has to jump
        else if (currentState == state.Jump)
        {

            // If Mario dies
            if (this.GetComponent<Mario>().marioDead == true)
            {
                currentState = state.Dead;
            }
        }
        // If the AI dies
        else if (currentState == state.Dead)
        {

        }

        UpdateRaycast();
    }

    private void UpdateRaycast()
    {
        RaycastHit2D hitForward = Physics2D.Raycast(transform.position, Vector2.right, forwardRaycastLength);
        RaycastHit2D hitUpward = Physics2D.Raycast(transform.position, Vector2.up, verticalRaycastLength);
        RaycastHit2D hitUpForward = Physics2D.Raycast(transform.position, Vector2.right + Vector2.up, upRightRaycastLength);
        RaycastHit2D hitDownForward = Physics2D.Raycast(transform.position, Vector2.right + Vector2.down, downRightRaycastLength);

        Debug.DrawRay(transform.position, Vector2.right, Color.red);
        Debug.DrawRay(transform.position, Vector2.up, Color.red);
        Debug.DrawRay(transform.position, Vector2.right + Vector2.up, Color.red);
        Debug.DrawRay(transform.position, Vector2.right + Vector2.down, Color.red);
    }

    private void SetStartRandomValues()
    {
        // Set random jump 
        jumpFuzzyStart = Mathf.RoundToInt(Random.Range(0, 100));
        jumpFuzzyEnd = 100 - jumpFuzzyStart;
    }

    private void RandomNumberGeneration(int startNumber, int endNumber)
    {
        // generate random number
        randomGeneratedNumber = Random.Range(startNumber, endNumber);
    }
}
