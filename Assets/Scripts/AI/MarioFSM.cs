using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioFSM : MonoBehaviour
{
    // Variables

    // Raycast Variables
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private LayerMask enemyLayerMask;
    private int forwardRaycastLength;
    private int upRightRaycastLength;
    private int downRightRaycastLength;
    private int verticalRaycastLength;
    Rigidbody2D rigidBody;

    // Fuzzy logic Variables
    // Make new Random
    private float randomGeneratedNumber;
    public int delayedJumpThreshold;
    public int longJumpThreshold;
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
        DebugRaycast();

        print(currentState);

        RaycastHit2D hitForward = Physics2D.Raycast(transform.position, Vector2.right, forwardRaycastLength);
        RaycastHit2D hitUpward = Physics2D.Raycast(transform.position, Vector2.up, verticalRaycastLength);
        RaycastHit2D hitUpForward = Physics2D.Raycast(transform.position, Vector2.right + Vector2.up, upRightRaycastLength);
        RaycastHit2D hitDownForward = Physics2D.Raycast(transform.position, Vector2.right + Vector2.down, downRightRaycastLength);

        // If in the walking right state
        if (currentState == state.WalkRight)
        {
            // Walk right
            this.GetComponent<Mario>().MoveRight();

            // Conditions to Swap States

            // If hitForward hits something
            if (hitForward.collider != null)
            {
                // If forward raycast hits an obstacle
                if (hitForward.transform.gameObject.layer == LayerMask.NameToLayer("platform"))
                {
                    currentState = state.MeetObstacle;
                }

                // If forward raycast hits an enemy
                if (hitForward.transform.gameObject.layer == LayerMask.NameToLayer("enemy"))
                {
                    currentState = state.MeetEnemy;
                }
            }

            //// If hitUpward hits something
            //if (hitUpward.collider != null)
            //{
            //    // If forward raycast hits an obstacle
            //    if (hitUpward.transform.gameObject.layer == LayerMask.NameToLayer("platform"))
            //    {
            //        currentState = state.MeetObstacle;
            //    }

            //    // If forward raycast hits an enemy
            //    if (hitUpward.transform.gameObject.layer == LayerMask.NameToLayer("enemy"))
            //    {
            //        currentState = state.MeetEnemy;
            //    }
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
            currentState = state.Jump;


            // If Mario dies
            if (this.GetComponent<Mario>().marioDead == true)
            {
                currentState = state.Dead;
            }
        }
        // If the AI has to jump
        else if (currentState == state.Jump)
        {
            // Move to right
            this.GetComponent<Mario>().MoveRight();

            // If mario is grounded
            if (this.GetComponent<Mario>().IsGrounded())
            {
                RandomNumberGeneration(jumpFuzzyEnd);

                // Then decide which jump to make
                if (randomGeneratedNumber >= longJumpThreshold)
                {
                    this.GetComponent<Mario>().LongJump();
                }
                else if (randomGeneratedNumber < longJumpThreshold)
                {
                    this.GetComponent<Mario>().ShortJump();
                }
                
            }
            else if (this.GetComponent<Mario>().IsGrounded() == false)
            {
                currentState = state.WalkRight;
            }

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
    }

    private void DebugRaycast()
    {
        Debug.DrawRay(transform.position, Vector2.right, Color.red);
        Debug.DrawRay(transform.position, Vector2.up, Color.red);
        Debug.DrawRay(transform.position, Vector2.right + Vector2.up, Color.red);
        Debug.DrawRay(transform.position, Vector2.right + Vector2.down, Color.red);
    }

    private void SetStartRandomValues()
    {
        // Set random jump 
        longJumpThreshold = Mathf.RoundToInt(Random.Range(0, 100));
        jumpFuzzyEnd = 100;

        forwardRaycastLength = Mathf.RoundToInt(Random.Range(3, 10));
        upRightRaycastLength = Mathf.RoundToInt(Random.Range(3, 10));
        downRightRaycastLength = Mathf.RoundToInt(Random.Range(3, 10));
        verticalRaycastLength = Mathf.RoundToInt(Random.Range(3, 10));
    }

    private void RandomNumberGeneration(int endNumber)
    {
        // generate random number
        randomGeneratedNumber = Random.Range(0, endNumber);
    }
}