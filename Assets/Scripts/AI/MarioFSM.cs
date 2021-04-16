using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioFSM : MonoBehaviour
{
    // Variables

    // Using Loaded File Variables
    public bool isUsingLoadedFiles;

    // Raycast Variables
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private LayerMask enemyLayerMask;
    private int forwardRaycastLength;
    private int upRightRaycastLength;
    private int downRightRaycastLength;
    Rigidbody2D rigidBody;

    // Fuzzy logic Variables
    // Make new Random
    private float randomGeneratedNumber;
    public float jumpDelayTime;
    public int delayedJumpThreshold;
    public int longJumpThreshold;
    public int jumpFuzzyEnd;
    public int floorDistance = 2;

    // Gentic Algorithm
    public float fitnessScore;
    public float maxTime = 300f;
    public float currentTime;
    public float maxDistance;
    public float distanceToFinish;
    public GameObject finishLine;
    

    // AI states
    protected enum state
    {
        WalkRight,
        MeetEnemy,
        MeetObstacle,
        Jump,
        Dead,
        CompletedLevel
    }

    // Denotes the current state
    private state currentState;

    
    // Start is called before the first frame update
    void Start()
    {
        if (isUsingLoadedFiles == false)
        {
            SetStartRandomValues();
        }

        // Set Rigid Body
        rigidBody = GetComponent<Rigidbody2D>();

        maxDistance = Vector2.Distance(this.transform.position, finishLine.transform.position);

        // Set starting state
        currentState = state.WalkRight;
    }

    // Update is called once per frame
    void Update()
    {
        DebugRaycast();

        UpdateDistanceTravelledAndTime();

        Vector3 offset = new Vector2(0f, 0.7f);


        RaycastHit2D hitForward = Physics2D.Raycast(transform.position - offset, Vector2.right, forwardRaycastLength);
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

            // If hitUpForward hits something
            if (hitUpForward.collider != null)
            {
                // If hitUpForward raycast hits an obstacle
                if (hitUpForward.transform.gameObject.layer == LayerMask.NameToLayer("platform"))
                {
                    currentState = state.MeetObstacle;
                }

                // If hitUpForward raycast hits an enemy
                if (hitUpForward.transform.gameObject.layer == LayerMask.NameToLayer("enemy"))
                {
                    currentState = state.MeetEnemy;
                }
            }

            // If hitDownForward hits something
            if (hitDownForward.collider != null)
            {
                // If hitDownForward raycast hits an obstacle
                if (hitDownForward.transform.gameObject.layer == LayerMask.NameToLayer("platform"))
                {
                    // If the hit range is grater than a set value (To catch pits)
                    if (hitDownForward.distance > floorDistance)
                    {
                        currentState = state.MeetObstacle;
                    }
                }

                // If hitDownForward raycast hits an enemy
                if (hitDownForward.transform.gameObject.layer == LayerMask.NameToLayer("enemy"))
                {
                    currentState = state.MeetEnemy;
                }
            }

            // If Mario dies
            if (this.GetComponent<Mario>().marioDead == true)
            {
                currentState = state.Dead;
            }
        }
        // If AI meets an enemy
        else if (currentState == state.MeetEnemy)
        {
            RandomNumberGeneration(jumpFuzzyEnd);

            // Walk right
            this.GetComponent<Mario>().MoveRight();

            // decide whether to make a delayed jump or an instant jump
            if (randomGeneratedNumber >= delayedJumpThreshold)
            {
                // Delay then Jump
                DelayJump(jumpDelayTime);
            }
            else if (randomGeneratedNumber < delayedJumpThreshold)
            {
                // Trigger instant jump
                currentState = state.Jump;
            }

            // If Mario dies
            if (this.GetComponent<Mario>().marioDead == true)
            {
                currentState = state.Dead;
            }
        }
        // If AI meets an obstacle
        else if (currentState == state.MeetObstacle)
        {
            RandomNumberGeneration(jumpFuzzyEnd);

            // Walk right
            this.GetComponent<Mario>().MoveRight();

            // decide whether to make a delayed jump or an instant jump
            if (randomGeneratedNumber >= delayedJumpThreshold)
            {
                // Delay then Jump
                DelayJump(jumpDelayTime);
            }
            else if (randomGeneratedNumber < delayedJumpThreshold)
            {
                // Trigger instant jump
                currentState = state.Jump;
            }

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

            if (currentTime => maxTime)
            {
                // Kill mario
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
            UpdateFitnessScore();
        }
        // If level is finished
        else if (currentState == state.CompletedLevel)
        {
            UpdateFitnessScore();
        }
    }

    private void DebugRaycast()
    {
        Vector3 offset = new Vector2(0f, 0.7f);

        Debug.DrawRay(transform.position - offset, Vector2.right, Color.red);
        Debug.DrawRay(transform.position, Vector2.right + Vector2.up, Color.red);
        Debug.DrawRay(transform.position, Vector2.right + Vector2.down, Color.red);
    }

    private void SetStartRandomValues()
    {
        // Set random jump thresholds
        longJumpThreshold = Mathf.RoundToInt(Random.Range(0, 100));
        delayedJumpThreshold = Mathf.RoundToInt(Random.Range(0, 100));
        jumpFuzzyEnd = 100;

        // Set random time delays
        jumpDelayTime = Random.Range(0, 8);
        
        // Set random raycast length
        forwardRaycastLength = Mathf.RoundToInt(Random.Range(3, 10));
        upRightRaycastLength = Mathf.RoundToInt(Random.Range(3, 10));
        downRightRaycastLength = Mathf.RoundToInt(Random.Range(3, 10));
    }

    private void RandomNumberGeneration(int endNumber)
    {
        // generate random number
        randomGeneratedNumber = Random.Range(0, endNumber);
    }

    private void UpdateDistanceTravelledAndTime()
    {
        // Measures distance between mario and finish line
        distanceToFinish = Vector2.Distance(this.transform.position, finishLine.transform.position);

        

        
        currentTime = Time.deltaTime;
    }

    private void UpdateFitnessScore()
    {
        // If not reached end yet use just distance travelled
        if(this.GetComponent<Mario>().levelFinished != true)
        {
            fitnessScore = maxDistance - distanceToFinish; 
        }
        // Else if reached end multiply by time remaining
        else if (this.GetComponent<Mario>().levelFinished == true)
        {
            fitnessScore = maxDistance * (maxTime - currentTime);
        }
    }

    IEnumerator DelayJump(float time)
    {
        yield return new WaitForSeconds(time);

        currentState = state.Jump;
    }
}
