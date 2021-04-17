using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask, enemyLayerMask; //Platform layer mask, for collisions

    public Animator animator; //Mario animations

    public List<int> funny = new List<int>();

    public SpriteRenderer spriteRenderer; //Sprite render

    public float jumpVel; //The initial velocity of marios jump

    public float speed; //Movement speed

    public BoxCollider2D boxCollider;//The box collider, used for colliding with enviroment
    public Rigidbody2D rb;//Mario basic physics

    public float maxSpeed;//X axis speed clamp value

    //This section is uses to change the gravity effect depending on whether a short or long jump is done
    public float fallMultiplier = 2.5f; //General gravity multiplier, makes mario plummet faster when jump peak is reached
    public float lowJumpMultiplier = 2f; //Short jumping creates a less violent plummet, used since the

    public bool marioDead = false; //Mario is dead checker

    public bool shortJump = false; //AI bool for short jumping

    public bool levelFinished = false;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    public void GoombaSquished() //When landing on goomba, jump.
    {
        rb.velocity = Vector2.up * jumpVel;
    }

    public void MarioReset()
    {

    }

    public void MarioDeath() //Used when a goomba collides with mario
    {
        animator.SetBool("MarioDead", true);//Animatior bool
        rb.velocity = Vector2.zero;//Stop moving
        rb.bodyType = RigidbodyType2D.Kinematic;//No physics applied
        gameObject.layer = 10;//Change layer so no collisions occur
        marioDead = true;//Bool
    }

    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    public bool IsGrounded() //Check if mario is grounded using a box cast to detect each edge of mario on the bottom, this means that mario can be on the edge of a block and still jump
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, platformLayerMask);
        Color rayColor;
        //Debug color change
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        //Draw box rays for debugging
        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0f), Vector2.down * (boxCollider.bounds.extents.y + 0.1f), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, 0f), Vector2.down * (boxCollider.bounds.extents.y + 0.1f), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(0f, boxCollider.bounds.extents.y), Vector2.right * (boxCollider.bounds.extents.y + 0.1f), rayColor);
        return raycastHit.collider != null; //Only return true if a collision occurs with the platforms
    }

    public bool TouchingWallRight() //Same as above but for the right side
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.right, 0.2f, platformLayerMask);
        Color rayColor;

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.y, 0f), Vector2.left * (boxCollider.bounds.extents.y + 0.2f), rayColor);

        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }
    public bool TouchingWallLeft()//Check if mario is touching a wall on the left, similar to above
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.left, 0.2f, platformLayerMask);
        Color rayColor;

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.y, 0f), Vector2.left * (boxCollider.bounds.extents.y + 0.2f), rayColor);

        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }
   

    

    // Update is called once per frame
    void Update()
    {
        //animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal")); //Set animator var to horizontal input

        if (IsGrounded() && Input.GetButtonDown("Jump")) //Check if grounded, if grounded, jump
        {
            LongJump();
        }

        if (IsGrounded()) { animator.SetBool("Ground", true); } //Berry important pls move over xoxo
        else { animator.SetBool("Ground", false); } //Same with this one

        if (rb.velocity.y < 0.001 )//If player has reached peak of jump or is falling, apply the fall grav modifier.
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0.001 && shortJump) //If short jump has been chosen, use the short jump grav modifier
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        if (Input.GetAxisRaw("Horizontal") > 0) //If the player is not touching a wall and the player is pressing right, apply force.
        {
            MoveRight();
        }
        if (Input.GetAxisRaw("Horizontal") < 0) //Similar to above
        {
            //MoveLeft();
        }
        

        //This section takes the current velocity, removes the current y vel and stores it for later use.
        //Only the x vel is clamped and the stored y is applied to the clamped velocity
        
        //Set animator var to the x vel
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("VerticalSpeed", rb.velocity.y);
        float clampedXVelocity = Mathf.Min(Mathf.Abs(rb.velocity.x), maxSpeed) * Mathf.Sign(rb.velocity.x);
        float velocityY = rb.velocity.y;
        rb.velocity = new Vector2(clampedXVelocity, velocityY);

    }


    public void MoveLeft()//Move left function for AI
    {
        spriteRenderer.flipX = true;

        if (!TouchingWallLeft() && !marioDead) rb.velocity += Vector2.left * speed * Time.deltaTime;
    }
    public void MoveRight()//Move right function for AI
    {
        spriteRenderer.flipX = false;

        if (!TouchingWallRight() && !marioDead) rb.velocity += Vector2.right * speed * Time.deltaTime;
    }
    public void LongJump()//Long jump funtion for AI
    {
        if(!marioDead)
        {
            rb.velocity = Vector2.up * jumpVel;
            shortJump = false;
        }
        
    }
    public void ShortJump()//Short jump function for AI
    {
        if(!marioDead)
        {
            rb.velocity = Vector2.up * jumpVel;
            shortJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//Used to check if mario has got to the finish
    {
        if(collision.gameObject.layer ==  LayerMask.NameToLayer("finish"))
        {
            levelFinished = true;
        }
    }
}
