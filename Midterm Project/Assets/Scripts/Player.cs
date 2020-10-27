using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 3f;
    [SerializeField] LayerMask _ground;
    [SerializeField] LayerMask _climbables;
    [SerializeField] float _jumpForce = 7f;
    [SerializeField] RuntimeData _runtimeData;

    private bool canMove = false;
    private bool canJump = true;
    private bool canClimb = false;
    private bool canDoubleJump;
    private bool isClimbing;

    private Rigidbody2D rigidbody;
    private CapsuleCollider2D collider2D;

    private int numJumps = 0;
    private void Awake()
    {
        rigidbody = transform.GetComponent<Rigidbody2D>();
        collider2D = transform.GetComponent<CapsuleCollider2D>();
        //reset runtime data
        _runtimeData._currentLevel = 1;
        //_runtimeData._upgradesCollected = new List<string>();
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        isGrounded();
        if (canMove)
        {
            CheckJump();
            CheckUpgrades();
            Movement();
        }
    }
    public void Movement()
    {
        float move = Input.GetAxis("Horizontal");
        Vector2 movementVector = new Vector2(move * _movementSpeed, rigidbody.velocity.y);
        rigidbody.velocity = movementVector;
        flipSprite(movementVector);
        RaycastHit2D ladderHit = Physics2D.Raycast(collider2D.bounds.center, Vector2.up, collider2D.bounds.extents.y + 1f, _climbables);
        if (ladderHit.collider != null)
            ClimbLadder();
        else
            isClimbing = false;
    }
    private void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            if(canDoubleJump)
            {
                if(numJumps <=1)
                {
                    rigidbody.velocity = Vector2.up * _jumpForce;
                    numJumps++;
                }
            }
            else
            {
                if(isGrounded())
                {
                    rigidbody.velocity = Vector2.up * _jumpForce;
                    numJumps++;
                    canJump = false;
                }
            }
            
        }
    }
    private void ClimbLadder()
    {
        if(canClimb)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                isClimbing = true;
        }
      
        if(isClimbing)
        {
            float vertSpeed = Input.GetAxis("Vertical") * 4f;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, vertSpeed);
        }
        if (!isClimbing)
            rigidbody.gravityScale = 1;
    }
    private void CheckUpgrades()
    {
        canClimb = _runtimeData._upgradesCollected.Contains("ClimbLadderUpgrade");
        canDoubleJump = _runtimeData._upgradesCollected.Contains("DoubleJumpUpgrade");
    }
    private void flipSprite(Vector2 movementVector)
    {
        if (movementVector.x < 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else if (movementVector.x > 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

    private bool isGrounded()
    {
        float padding = 1f;
        RaycastHit2D hit = Physics2D.Raycast(collider2D.bounds.center, Vector2.down, collider2D.bounds.extents.y + padding,_ground);
        Color ray;
        if (hit.collider != null)
            ray = Color.green;
        else
            ray = Color.red;
        Debug.DrawRay(collider2D.bounds.center, Vector2.down * collider2D.bounds.extents.y,ray);
        return hit.collider != null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground") && isGrounded())
        {
            canMove = true;
            canJump = true;
            numJumps = 0;
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Death"))
        {
            Respawn.RespawnInstance.RespawnAtLevel(_runtimeData._currentLevel, gameObject);
        }
    }

}
