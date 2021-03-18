using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    public Animator animator;
    public Vector2 position;
    public Vector2 velocity;
    public float jumpVel;

    public float faceDir;

    public float speed;
    public float frictionValue;
    public float opposingForce;

    public BoxCollider2D boxCollider;
    public Rigidbody2D rb;

    public float currentSpeed;
    public float maxSpeed;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;


    public bool jumpPressed = false;
    public bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, platformLayerMask);
        Color rayColor;

        if(raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0f), Vector2.down * (boxCollider.bounds.extents.y + 0.1f), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, 0f), Vector2.down * (boxCollider.bounds.extents.y + 0.1f), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(0f,boxCollider.bounds.extents.y), Vector2.right * (boxCollider.bounds.extents.y + 0.1f), rayColor);
        return raycastHit.collider != null;
    }

    public bool TouchingWallLeft()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.left, 0.3f, platformLayerMask);
        Color rayColor;

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.y, 0f), Vector2.left * (boxCollider.bounds.extents.y + 0.3f), rayColor);
        
        Debug.Log(raycastHit.collider);
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

        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.y, 0f), Vector2.left * (boxCollider.bounds.extents.y + 0.1f), rayColor);

        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.up * jumpVel;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        if( Input.GetAxisRaw("Horizontal") > 0 && !TouchingWallRight())
        {
            rb.velocity += Vector2.right * speed * Time.deltaTime;
            //faceDir = 1.0f;
        }
        if (Input.GetAxisRaw("Horizontal") < 0 && !TouchingWallLeft())
        {
            rb.velocity += Vector2.left * speed * Time.deltaTime;
            //faceDir = 2.0f;
        }

        if (Input.GetAxisRaw("Horizontal") == 0 )
        {
            //currentSpeed = rb.velocity.x;
            //opposingForce = -currentSpeed;

            animator.SetFloat("IdleType", faceDir);

            //rb.AddRelativeForce(new Vector2(opposingForce * frictionValue, 0));
        }

        Vector2 vel = rb.velocity;
        float jumpTemp = vel.y;
        vel.y = 0.0f;
        vel = Vector2.ClampMagnitude(vel,maxSpeed);
        vel.y = jumpTemp;

        rb.velocity = vel;

        animator.SetFloat("Speed", rb.velocity.x);


    }
}
