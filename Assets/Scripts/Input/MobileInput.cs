using UnityEngine;
//using UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(PlayerController))]
public class MobileInput : MonoBehaviour
{
    private InputSystem_Actions inputs;
    private PlayerController playerController;
    [SerializeField] GameObject particleEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputs = new InputSystem_Actions();
        inputs.Enable();
        inputs.Player.Jump.started += Jump;
        playerController = GetComponent<PlayerController>();
    }

    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //Jump
        playerController.Jump();

        //particle effect
        Vector2 pos = Camera.main.ScreenToWorldPoint(inputs.Player.JumpWithPos.ReadValue<Vector2>());
        Instantiate(particleEffect, pos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        foreach(Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began)
            {
                //Jump
                playerController.Jump();

                //particle effect
                Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);
                Instantiate(particleEffect, pos, Quaternion.identity);
            }
        }*/
    }
}
