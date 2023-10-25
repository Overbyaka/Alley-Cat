using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask ropeLayer;
    [SerializeField] private LayerMask barrelLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float ropeJumpCooldown;
    private float horizontalInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //flipping
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //set anim
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded() || onRopeDown() || onBarrel());
        
        if (ropeJumpCooldown > 0.2f)
        {
            if(!boxCollider.enabled)
            {
                boxCollider.enabled = true;
            }
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onRopeUp() && !onRopeDown())
            {
                body.gravityScale = 0;
                //body.velocity = Vector2.zero;
            }
            if (Input.GetKey(KeyCode.Space) && (isGrounded() || onRopeDown() || onRopeUp() || onBarrel()))
            {
                Jump();
                body.gravityScale = 1;
            }
            if (Input.GetKey(KeyCode.S) && (onRopeUp() || onRopeDown()))
            {
                JumpOff();
                body.gravityScale = 1;
            }
        }
        else
        {
            ropeJumpCooldown += Time.deltaTime;
        }

    }

    private void Jump()
    {
        if(isGrounded() || onRopeDown() || onBarrel())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onRopeUp())
        {
            boxCollider.enabled = false;
            ropeJumpCooldown = 0;
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
    }

    private void JumpOff()
    {
        body.velocity = new Vector2(body.velocity.x, -jumpPower/2);
        anim.SetTrigger("jump");
        ropeJumpCooldown = 0;
        boxCollider.enabled = false;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onRopeUp()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.up, 0.1f, ropeLayer);
        return raycastHit.collider != null;
    }
    private bool onRopeDown()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, ropeLayer);
        return raycastHit.collider != null;
    }
    private bool onBarrel()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, barrelLayer);
        return raycastHit.collider != null;
    }
}
