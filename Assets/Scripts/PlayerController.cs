using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private LayerMask _groundLayer;
    private bool _isGrounded, _isShielded;
    private Coroutine _shieldCoroutine;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Vector2 _boxCastSize;
    [SerializeField] private float _boxCastOffset;
    [SerializeField] private GameObject _shieldSprite;
    
    [Header("Sound Effects")]
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _diamondSound;
    [SerializeField] private AudioClip _powerUpSound;
    [SerializeField] private AudioClip _shieldDestroySound;
    public bool IsGrounded => _isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _groundLayer = LayerMask.GetMask("Ground");
        _isShielded = false;
        _shieldSprite.SetActive(false);
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
        GameManager.Instance.PlayOneShot(_jumpSound);
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
            GameManager.Instance.PlayOneShot(_diamondSound);
        }
        else if (other.CompareTag("Obstacle") || other.CompareTag("Alien"))
        {
            if (_isShielded)
            {
                ShieldSetActive(false);
                StopCoroutine(_shieldCoroutine);
                GameManager.Instance.PlayOneShot(_shieldDestroySound);
                return;
            }
            
            other.gameObject.SetActive(false);
            GetComponent<Collider2D>().enabled = false;
            GameManager.Instance.GameOver();
            GameManager.Instance.PlayOneShot(_deathSound);
        }
        else if (other.CompareTag("Powerup"))
        {
            other.gameObject.SetActive(false);
            if(_isShielded) StopCoroutine(_shieldCoroutine);
            
            ShieldSetActive(true);
            _shieldCoroutine = StartCoroutine(SetShieldOff(10));
            GameManager.Instance.PlayOneShot(_powerUpSound);
        }
    }

    private void ShieldSetActive(bool isActive)
    {
        _isShielded = isActive;
        _shieldSprite.SetActive(isActive);
    }

    private IEnumerator SetShieldOff(float inSeconds)
    {
        yield return new WaitForSeconds(inSeconds);
        ShieldSetActive(false);
        GameManager.Instance.PlayOneShot(_shieldDestroySound);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * _boxCastOffset, _boxCastSize);
    }
}
