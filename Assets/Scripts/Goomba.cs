using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;

    public Animator animator;

    public float speed;
    public float maxSpeed;

    public bool isRight;

    public BoxCollider2D boxCollider;
    public Rigidbody2D rb;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void GoombaMoveLeft()
    {
        //rb.velocity += Vector2.left * speed * Time.deltaTime;
        transform.position -= transform.right * (Time.deltaTime * speed);
    }
    void GoombaMoveRight()
    {
        //rb.velocity += Vector2.right * speed * Time.deltaTime;
        transform.position += transform.right * (Time.deltaTime * speed);
    }

    public bool TouchingWallLeft()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.left, 0.01f, platformLayerMask);
        Color rayColor;

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.y, 0f), Vector2.left * (boxCollider.bounds.extents.y + 0.01f), rayColor);

        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }
    public bool TouchingWallRight()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.right, 0.01f, platformLayerMask);
        Color rayColor;

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.y, 0f), Vector2.left * (boxCollider.bounds.extents.y + 0.01f), rayColor);

        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    public void Update()
    {
        if(isRight)
        {
            GoombaMoveRight();
        }
        else
        {
            GoombaMoveLeft();
        }

        if(TouchingWallLeft())
        {
            isRight = true;
        }  
        if(TouchingWallRight())
        {
            isRight = false;
        }
    }
}
