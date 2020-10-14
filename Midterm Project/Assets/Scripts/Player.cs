using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 3f;
    [SerializeField] LayerMask _ground;
    [SerializeField] float _slopeCheckDistance;
    [SerializeField] float _jumpForce = 7f;
    private bool canMove = false;
    private bool canJump = true;
    private bool isJumping;
    private bool onGround;
    private bool isOnSlope;

    private Rigidbody2D rigidbody;

    private CapsuleCollider2D capCollider;

    private Vector2 colliderSize;
    private Vector2 slopeNormalPerp;

    private int numJumps = 0;

    private float slopeDownAngle;
    private float slopeDownAngleOld;
    private void Awake()
    {
        rigidbody = transform.GetComponent<Rigidbody2D>();
        capCollider = transform.GetComponent<CapsuleCollider2D>();
        colliderSize = capCollider.size;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            SlopeCheck();
            CheckJump();
            Movement();
        }
    }
    public void Movement()
    {
        Vector3 movementVector = new Vector3(0f,0f,0f);
        float move = Input.GetAxis("Horizontal");

        if (onGround && !isOnSlope)
        {
            movementVector = new Vector3(move * _movementSpeed * Time.deltaTime, 0f, 0f);
            transform.position += movementVector;
        }
        else if(onGround && isOnSlope)
        {
            movementVector = new Vector3(-move * _movementSpeed *1.5f *slopeNormalPerp.x* Time.deltaTime, -move * _movementSpeed *1.5f* slopeNormalPerp.y * Time.deltaTime, 0f);
            transform.position += movementVector;
        }
        else if(!onGround)
        {
            movementVector = new Vector3(move * _movementSpeed * Time.deltaTime, 0f, 0f);
            transform.position += movementVector;
        }
        flipSprite(movementVector);
        canJump = numJumps < 1;

    }
    private void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Debug.Log("Jumping");
            numJumps++;
            canJump = false;
            rigidbody.velocity = Vector2.up * _jumpForce;
            isJumping = true;
        }
    }
    private void flipSprite(Vector3 movementVector)
    {
        if (movementVector.x < 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else if (movementVector.x > 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }
    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0f, colliderSize.y / 2f));
        SlopeCheckVertical(checkPos);
    }
    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down,_slopeCheckDistance,_ground);
        if(hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
            if(slopeDownAngle != slopeDownAngleOld)
            {
                isOnSlope = true;
            }
            slopeDownAngleOld = slopeDownAngle;
            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            Debug.Log("Landed");
            canMove = true;
            onGround = true;
            isJumping = false;
            numJumps = 0;
        }
    }

}
