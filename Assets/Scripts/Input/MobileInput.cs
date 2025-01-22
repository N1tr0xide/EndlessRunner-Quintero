using System;
using UnityEngine;
//using UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(PlayerController))]
public class MobileInput : MonoBehaviour
{
    private InputSystem_Actions _inputs;
    private PlayerController _playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputs = new InputSystem_Actions();
        _inputs.Enable();
        _inputs.Player.Jump.started += Jump;
        _playerController = GetComponent<PlayerController>();
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }

    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!GameManager.Instance.GameStarted)
        {
            GameManager.Instance.StartGame();
        }
        
        if(GameManager.Instance.IsGameOver) return;
        _playerController.Jump();
    }
}
