using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public InputAction inputAction;
    private Vector2 _inputVec;

    // to 'Send Messages'
    public void OnMove(InputValue value)
    {
        _inputVec = value.Get<Vector2>();
    }
    // to 'Invoke Unity Events'
    public void MoveEvent(InputAction.CallbackContext context)
    {
        _inputVec = context.ReadValue<Vector2>();

        if(context.started)
        {
            // 입력이 시작되었다.
        }
        else if(context.performed)
        {
            // 입력이 인정, 성공되었다.
        }
        else if(context.canceled)
        {
            // 입력이 취소되었다.
        }
    }
}
