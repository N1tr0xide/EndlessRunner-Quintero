using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager _gameManager;
    private Rigidbody2D _rb;
    private LayerMask _groundLayer;
    private bool _isGrounded;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Vector2 _boxCastSize;
    [SerializeField] private float _boxCastOffset;

    public bool IsGrounded => _isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
        _rb = GetComponent<Rigidbody2D>();
        _groundLayer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();

        if(!IsGrounded && _rb.linearVelocityY < 2) 
        {
            _rb.AddForceY(-2);
        }

        if (IsGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGrounded()
    {
        _isGrounded = Physics2D.BoxCast(transform.position, _boxCastSize, 0, -transform.up, _boxCastOffset, _groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Diamond") || other.CompareTag("DiamondB"))
        {
            _gameManager.IncreaseScore(other.GetComponent<DiamondController>().Score);
            other.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * _boxCastOffset, _boxCastSize);
    }
}
