using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float _groundedSpeed = 5f;
    public float _jumpStrength = 7f;
    private bool _isGrounded;

    [Header("Keybinds")]
    private KeyCode _attackKey = KeyCode.Z;
    private KeyCode _specialKey = KeyCode.X;
    private KeyCode _jumpKey = KeyCode.Space;

    [Header("Player Components")]
    private Rigidbody2D _playerRB;
    private Animator _animator;

    private bool _grounded;
    

    // Start is called before the first frame update
    private void Awake()
    {
        GetPlayerComponents();
    }

    // Update is called once per frame
    void Update()
    {
        //Jump if press jumpKey
        if (Input.GetKeyDown(_jumpKey) && _grounded)
        {
            Jump();  
        }
    }

    private void FixedUpdate()
    {
        HorizontalMovement();
    }

    //get the player's rigidbody2d
    private void GetPlayerComponents()
    {
        //get player rb2d
        _playerRB = GetComponent<Rigidbody2D>();

        //get player animator
        _animator = GetComponent<Animator>();
    }

    //control player's horizontal movement and animation
    private void HorizontalMovement()
    {
        //player rb receives velocity
        var movement = Input.GetAxis("Horizontal") * _groundedSpeed;
        _playerRB.velocity = new Vector2(movement, _playerRB.velocity.y);

        //animation control
        if (movement != 0)
        {
            //flip sprite left and right
            transform.localScale = new Vector3(Mathf.Sign(movement), 1f, 1f); //Mathf.Sign retorna 1 se o valor >=0, senão retorna -1
        }

        //if true, set player to walking animation. Else, set the player to idle animation
        _animator.SetBool("Moving", movement != 0);
    }

    //control player's jumping movement and animation
    private void Jump()
    {
        //jumping action
        _grounded = false;
        _playerRB.velocity = new Vector2(_playerRB.velocity.x, _jumpStrength);

        //animation control
        _animator.SetBool("Grounded", _grounded);
        _animator.SetFloat("VerticalVelocity", _playerRB.velocity.y);
    }

    //checking collision with the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _grounded = true;
            _animator.SetBool("Grounded", _grounded);
        }
    }
}
