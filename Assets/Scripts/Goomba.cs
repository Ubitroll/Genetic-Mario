using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask enemyMask;


    public int id;

    public Animator animator;
    
    public SpriteRenderer spriteRenderer;

    public float speed;
    public bool isRight;
    public bool heDead;
    public bool marioKilled;

    public bool shouldCollide;


    public BoxCollider2D boxCollider;
    public Rigidbody2D rb;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;


    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        marioKilled = false;
        shouldCollide = true;
    }

    private void Start()
    {
        EventSystem.current.onGoombaSquished += GoombaKilled;
        EventSystem.current.onMarioKilled += MarioDeaded;
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
    void GoombaStoppedBecauseHeIsDeadOhLordWhyDoTheGoodDieYoung()
    {
        rb.velocity = Vector2.zero;
    }

    public bool TouchingWallLeft()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.left, 0.05f);
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
                EventSystem.current.MarioKilled();
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
    public bool TouchingWallRight()
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
                EventSystem.current.MarioKilled();
            }
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.y, 0f), Vector2.left * (boxCollider.bounds.extents.y + 0.01f), rayColor);
        return false;

    }

    public bool MarioOnHead(bool active)
    {
        if (active)
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.up, 0.01f, playerMask);
            Color rayColor;

            if (raycastHit.collider != null)
            {
                rayColor = Color.green;
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
        if (isRight && !heDead && !marioKilled)
        {
            GoombaMoveRight();
            //spriteRenderer.flipX = true;
        }
        if (!isRight && !heDead && !marioKilled)
        {
            GoombaMoveLeft();
            //spriteRenderer.flipX = false;
        }

        if (heDead)
        {
            GoombaStoppedBecauseHeIsDeadOhLordWhyDoTheGoodDieYoung();
        }


        if (TouchingWallLeft())
        {
            isRight = true;
        }
        if (TouchingWallRight())
        {
            isRight = false;
        }

        if (MarioOnHead(shouldCollide))
        {
            heDead = true;
            EventSystem.current.GoombaSquished(id);
            animator.SetBool("Dead", true);
            shouldCollide = false;
            //MarioOnHead(shouldCollide);
            gameObject.layer = LayerMask.NameToLayer("dead");

        }



    }

    private void GoombaKilled(int id)
    {
        if (id == this.id) Destroy(gameObject, 0.7f);

    }

    private void MarioDeaded()
    {
        marioKilled = true; Debug.Log("He ded");
        
    }

    IEnumerator kill()
    {

        yield return new WaitForSeconds(0.7f);
    }



}
