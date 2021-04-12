using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    public float jumpVel;

    public float speed;

    public BoxCollider2D boxCollider;
    public Rigidbody2D rb;

    public float maxSpeed;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public bool marioDead = false;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.onGoombaSquished += GoombaSquished;
        EventSystem.current.onMarioKilled += MarioDeath;

    }

    private void GoombaSquished(int id)
    {
        rb.velocity = Vector2.up * jumpVel;
    }

    private void MarioDeath()
    {
        animator.SetBool("MarioDead", true);
        rb.velocity = Vector2.up * jumpVel;
        gameObject.layer = 10;
        marioDead = true;
    }

    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, platformLayerMask);
        Color rayColor;

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0f), Vector2.down * (boxCollider.bounds.extents.y + 0.1f), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, 0f), Vector2.down * (boxCollider.bounds.extents.y + 0.1f), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(0f, boxCollider.bounds.extents.y), Vector2.right * (boxCollider.bounds.extents.y + 0.1f), rayColor);
        return raycastHit.collider != null;
    }

    public bool TouchingWallLeft()
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
    public bool TouchingWallRight()
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

    // Update is called once per frame
    void Update()
    {
        //animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal")); //Set animator var to horizontal input

        if (IsGrounded() && Input.GetButtonDown("Jump")) //Check if grounded, if grounded, jump
        {
            rb.velocity = Vector2.up * jumpVel;
        }

        if (IsGrounded()) { animator.SetBool("Ground", true); }
        else { animator.SetBool("Ground", false); }

        if (rb.velocity.y < 0.001)//If player has reached peak of jump or is falling, apply the fall grav modifier.
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0.001 && !Input.GetButton("Jump")) //If button is not held, use the short jump grav modifier
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        if (Input.GetAxisRaw("Horizontal") > 0) //If the player is not touching a wall and the player is pressing right, apply force.
        {
            spriteRenderer.flipX = false;

            if (!TouchingWallRight()) rb.velocity += Vector2.right * speed * Time.deltaTime;
        }
        if (Input.GetAxisRaw("Horizontal") < 0) //Similar to above
        {
            spriteRenderer.flipX = true;

            if (!TouchingWallLeft()) rb.velocity += Vector2.left * speed * Time.deltaTime;
        }

        if (Input.GetAxisRaw("Horizontal") == 0)
        {

        }

        //This section takes the current velocity, removes the current y vel and stores it for later use.
        //Only the x vel is clamped and the stored y is applied to the clamped velocity
        Vector2 vel = rb.velocity;
        float jumpTemp = vel.y;
        vel.y = 0.0f;
        vel = Vector2.ClampMagnitude(vel, maxSpeed);
        vel.y = jumpTemp;

        rb.velocity = vel;

        //Debug.Log(rb.velocity.y);

        //Set animator var to the x vel
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("VerticalSpeed", rb.velocity.y);


    }
}
