using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnim : MonoBehaviour
{
    private Animator _animator;
    private PlayerController _playerController;
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
    private static readonly int GameOver = Animator.StringToHash("GameOver");

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool(IsGrounded, _playerController.IsGrounded);
    }

    public void PlayDeathAnim()
    {
        _animator.SetBool(GameOver, true);
    }
}
