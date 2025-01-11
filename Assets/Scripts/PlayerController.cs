using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private Vector2 _boxCastSize;
    [SerializeField] private float _boxCastOffset;
    private Rigidbody2D _rb;
    private LayerMask _groundLayer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _groundLayer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        print(Grounded());
        
        if (Grounded() && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool Grounded()
    {
        return Physics2D.BoxCast(transform.position, _boxCastSize, 0, -transform.up, _boxCastOffset, _groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * _boxCastOffset, _boxCastSize);
    }
}
