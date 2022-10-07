using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D _playerRB;
    private Animator _playerAnimator;
    private BoxCollider2D _playerBoxCollider2D;
    [SerializeField] private LayerMask _layerForeground;
    private float _raycastSize = 0.5f;

    [Header("Key Bindings")]
    //private KeyCode _attackKey = KeyCode.Z;
    //private KeyCode _specialKey = KeyCode.X;
    private KeyCode _jumpKey = KeyCode.Space;

    [Header("Movement")]
    public float groundedSpeed = 5f;
    public float speedUpgrade = 1f;
    public float jumpStrength = 7f;
    public int totalJumps = 1;
    private int _atJump = 0; 

    void Awake()
    {
        GetComponents();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_jumpKey) && isGrounded() && _atJump < totalJumps)
        {
            Jump();
            Debug.Log(_atJump);
        }

        JumpAnimationController();
    }

    void FixedUpdate()
    {
        HorizontalMovement();

        //checking players collision with the ground
        _playerAnimator.SetBool("Grounded", isGrounded());

        //reseting total number of jumps
        if (isGrounded())
        {
            _atJump = 0;
        }
    }

    void GetComponents()
    {
        _playerRB = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _playerBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    //control player's horizontal movement and animation
    void HorizontalMovement()
    {
        //player rb receives velocity
        var movement = Input.GetAxis("Horizontal") * groundedSpeed * speedUpgrade;
        _playerRB.velocity = new Vector2(movement, _playerRB.velocity.y);

        //sprite flipper
        if (movement != 0)
        {
            //flip sprite left and right
            transform.localScale = new Vector3(Mathf.Sign(movement), 1f, 1f); //Mathf.Sign returns 1 if vector X >=0. else, returns -1. Fliping the sprite scale
        }

        //animations switch between idle and walk
        _playerAnimator.SetBool("Moving", movement != 0); //if moving == true, returns true. if not, returns false. If true, walks. If false, idle. 
    }

    //control player's jumping movement and animation
    void Jump()
    {
        //jumping action
        _atJump++;
        _playerRB.velocity = new Vector2(_playerRB.velocity.x, jumpStrength);
    }

    void JumpAnimationController()
    {
        _playerAnimator.SetFloat("VerticalVelocity", _playerRB.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            //_playerAnimator.SetBool("Grounded", false);
        }
    }

    private bool isGrounded()
    {
        bool grounded = Physics2D.Raycast(_playerBoxCollider2D.bounds.center, Vector2.down, _raycastSize, _layerForeground);

        Color color;
        if (grounded)
        {
            color = Color.red;
        }
        else
        {
            color = Color.green;
        }

        Debug.DrawRay(_playerBoxCollider2D.bounds.center, Vector2.down*_raycastSize, color);
        return grounded;
    }
}