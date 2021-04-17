using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    //Layer masks for collision checking
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask enemyMask;

    public Animator animator; //Animator
    
    public SpriteRenderer spriteRenderer;//Renderer

    public float speed;//Movement speed
    public bool isRight;//Direction bool
    public bool heDead;//Bool for death

    public bool shouldCollide;//Should collisions be checked


    public BoxCollider2D boxCollider;//Collider
    public Rigidbody2D rb;//Rigidbody

    public float fallMultiplier = 2.5f; //Gravity modifiers
    public float lowJumpMultiplier = 2f;


    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        shouldCollide = true;
    }

    private void Start()
    {
        
    }


    void GoombaMoveLeft() //Move left
    {
        //rb.velocity += Vector2.left * speed * Time.deltaTime;
        transform.position -= transform.right * (Time.deltaTime * speed);
    }
    void GoombaMoveRight() //Move right
    {
        //rb.velocity += Vector2.right * speed * Time.deltaTime;
        transform.position += transform.right * (Time.deltaTime * speed);
    }
    void GoombaStoppedBecauseHeIsDeadOhLordWhyDoTheGoodDieYoung() //Stop movement on death
    {
        rb.velocity = Vector2.zero;
    }

    public bool TouchingWallLeft()//Check if colliding with objects on layers
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.left, 0.05f);
        Color rayColor;

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;//If collision with layer, return true
            if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("platform") || raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("enemy"))
            {
                return true;
            }

            if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("player"))
            {//If collision with mario, kill mario
                raycastHit.transform.gameObject.GetComponent<Mario>().MarioDeath();
            }
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.y, 0f), Vector2.left * (boxCollider.bounds.extents.y + 0.01f), rayColor);
        return false;
        //Debug.Log(raycastHit.collider);
    }
    public bool TouchingWallRight()//Similar to above, but for the right direction
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.right, 0.05f);
        Color rayColor;

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
            if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("platform") || raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("enemy"))
            {
                return true;
            }
            if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("player"))
            {
                raycastHit.transform.gameObject.GetComponent<Mario>().MarioDeath();
            }
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.y, 0f), Vector2.left * (boxCollider.bounds.extents.y + 0.01f), rayColor);
        return false;

    }

    public bool MarioOnHead(bool active)//Check if mario has landed on top
    {
        if (active)
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.up, 0.01f, playerMask);
            Color rayColor;

            if (raycastHit.collider != null)
            {//If mario has landed on top, kill goomba
                rayColor = Color.green;
                raycastHit.collider.transform.gameObject.GetComponent<Mario>().GoombaSquished();
            }
            else
            {
                rayColor = Color.red;
            }

            //Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.y, 0f), Vector2.left * (boxCollider.bounds.extents.y + 0.01f), rayColor);

            Debug.Log(raycastHit.collider);
            return raycastHit.collider != null;
        }
        else return false;

    }


    public void Update()
    {
        if (isRight && !heDead) //If bool is true and not dead, move right
        {
            GoombaMoveRight();
            //spriteRenderer.flipX = true;
        }
        if (!isRight && !heDead)//If bool is false and not dead, move left
        {
            GoombaMoveLeft();
            //spriteRenderer.flipX = false;
        }

        if (heDead)//If dead, stop moving
        {
            GoombaStoppedBecauseHeIsDeadOhLordWhyDoTheGoodDieYoung();
        }


        if (TouchingWallLeft()) //If collision with wall or enemy, change direction
        {
            isRight = true;
        }
        if (TouchingWallRight())
        {
            isRight = false;
        }

        if (MarioOnHead(shouldCollide)) //If mario on head
        {
            heDead = true; //Kill goomba
            gameObject.layer = LayerMask.NameToLayer("dead"); //Change layer to stop collisions
            animator.SetBool("Dead", true); //Set animator bool
            shouldCollide = false; //Stop checking collisions
            //MarioOnHead(shouldCollide);
            GoombaKilled(); //Destory gameobject

        }



    }

    private void GoombaKilled()
    {
       Destroy(this.gameObject, 0.7f);
    }


   



}
