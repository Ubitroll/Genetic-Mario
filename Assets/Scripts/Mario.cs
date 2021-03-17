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

    public float speed;
    public float frictionValue;
    public float opposingForce;

    public BoxCollider2D boxCollider;

    public float currentSpeed;

    public bool jumpPressed = false;
    public bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
        jumpPressed = Input.GetButtonDown("Jump");


        if (IsGrounded() && jumpPressed )
        {
          
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, jumpVel);
        }
        if( Input.GetAxisRaw("Horizontal") > 0 && !TouchingWallRight())
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, this.GetComponent<Rigidbody2D>().velocity.y);
        }
        if (Input.GetAxisRaw("Horizontal") < 0 && !TouchingWallLeft())
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, this.GetComponent<Rigidbody2D>().velocity.y);
        }

        if (Input.GetAxisRaw("Horizontal") == 0 )
        {
            currentSpeed = this.GetComponent<Rigidbody2D>().velocity.x;
            opposingForce = -currentSpeed;

            this.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(opposingForce * frictionValue, 0));
        }




    }
}
