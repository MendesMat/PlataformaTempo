using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private TrailRenderer tr;
    public Rigidbody2D rb;  //referencia o rigidbody do player
    public Transform groundCheck;   //referencia o objeto que ira checar o chão
    public LayerMask groundLayer; //uma layer especifica para o piso

    private float horizontal;
    public float speed =0f;
    public float jumpingPower = 0f;
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = .2f;
    private float dashingCooldown = 0.8f;

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if(!isFacingRight && horizontal > 0f)
        {
            Flip();
        }else if(isFacingRight && horizontal < 0f)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Z) && canDash)
        {
            StartCoroutine(Dash());
        }

    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        /*
        if (!isDashing)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        */
    }

   

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        //tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        //tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    #region Dash com input system não funcional
    /* public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(transform.localScale.x * dashingPower, 0f), ForceMode2D.Impulse);
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    */
    #endregion
}
