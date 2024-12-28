using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public float GetVertical()
    {
        return _playerInput.Player.Move.ReadValue<Vector2>().y;
    }

    public float GetHorizontal()
    {
        return _playerInput.Player.Move.ReadValue<Vector2>().x;
    }
}