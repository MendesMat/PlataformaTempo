using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D _playerRB;
    private Animator _playerAnimator;
    private BoxCollider2D _playerBoxCollider2D;

    [Header("Key Bindings")]
    //private KeyCode _attackKey = KeyCode.Z;
    //private KeyCode _specialKey = KeyCode.X;
    private KeyCode _jumpKey = KeyCode.Space;

    [Header("Horizontal Movement")]
    public float GroundedSpeed = 5f;
    public float SpeedUpgrade = 1f;

    [Header("Jump")]
    public float JumpStrength = 7f;
    public float MUltJumpStregth = 4.5f;
    public int TotalJumps = 2;
    private int _atJump;
    private bool _multipleJump;

    [Header("Ground Check")]
    private float _raycastSize = 0.1f;
    [SerializeField] private LayerMask _layerForeground;

    void Awake()
    {
        GetComponents();
    }

    private void Update()
    {
        //jump commands
        if (Input.GetKeyDown(_jumpKey))
        {
            Jump();
        }

        JumpAnimationController();
    }

    void FixedUpdate()
    {
        HorizontalMovement();

        //checking players collision with the ground
        _playerAnimator.SetBool("Grounded", isGrounded());

        //reseting total number of jumps if true
        if (isGrounded())
        {
            if(_playerRB.velocity.y < 0.1f) _atJump = TotalJumps;
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
        var movement = Input.GetAxis("Horizontal") * GroundedSpeed * SpeedUpgrade;
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
        if (isGrounded())
        {
            _atJump -= 1;
            _playerRB.velocity = new Vector2(_playerRB.velocity.x, JumpStrength);
            _multipleJump = true;
            Debug.Log(_atJump);
        }
        else if (_multipleJump && _atJump > 0)
        {
            _atJump -= 1;
            _playerRB.velocity = new Vector2(_playerRB.velocity.x, MUltJumpStregth);
            Debug.Log(_atJump);
        }
    }

    //changes the players animations between jumping and falling, depending on vertical velocity
    void JumpAnimationController()
    {
        _playerAnimator.SetFloat("VerticalVelocity", _playerRB.velocity.y);
    }

    private bool isGrounded()
    {
        bool grounded = Physics2D.BoxCast(_playerBoxCollider2D.bounds.center, _playerBoxCollider2D.bounds.size, 0f, Vector2.down, _raycastSize, _layerForeground);

        Color color = grounded ? Color.red : Color.green;

        Debug.DrawRay(_playerBoxCollider2D.bounds.center - new Vector3(_playerBoxCollider2D.bounds.extents.x, 0), Vector2.down * (_playerBoxCollider2D.bounds.extents.y + _raycastSize), color);
        Debug.DrawRay(_playerBoxCollider2D.bounds.center + new Vector3(_playerBoxCollider2D.bounds.extents.x, 0), Vector2.down * (_playerBoxCollider2D.bounds.extents.y + _raycastSize), color);
        Debug.DrawRay(_playerBoxCollider2D.bounds.center - new Vector3(_playerBoxCollider2D.bounds.extents.x, _playerBoxCollider2D.bounds.extents.y + _raycastSize), Vector2.right * (_playerBoxCollider2D.bounds.size), color);

        return grounded;
    }
}