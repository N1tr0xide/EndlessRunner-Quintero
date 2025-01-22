using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
        _rb = GetComponent<Rigidbody2D>();
        _groundLayer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.IsGameOver) return;
        
        CheckGrounded();

        if(!IsGrounded && _rb.linearVelocityY < 2) 
        {
            _rb.AddForceY(-2);
        }
    }

    public void Jump()
    {
        if(!_isGrounded) return;
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGrounded()
    {
        _isGrounded = Physics2D.BoxCast(transform.position, _boxCastSize, 0, -transform.up, _boxCastOffset, _groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Diamond"))
        {
            GameManager.Instance.IncreaseScore(other.GetComponent<ObstacleController>().Score);
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Obstacle") || other.CompareTag("Alien"))
        {
            other.gameObject.SetActive(false);
            GetComponent<Collider2D>().enabled = false;
            GameManager.Instance.GameOver();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * _boxCastOffset, _boxCastSize);
    }
}
