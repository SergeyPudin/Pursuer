using UnityEngine;
using System;

[RequireComponent(typeof(PlayerInputHandler), typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    private CharacterController _characterController;
    private PlayerInputHandler _inputHandler;
    private Transform _transform;

    private void Awake()
    {
        if (_rotateSpeed < 0)
            throw new ArgumentOutOfRangeException(nameof(_rotateSpeed), "Speed cannot be negative.");

        if (_moveSpeed < 0)
            throw new ArgumentOutOfRangeException(nameof(_moveSpeed), "Speed cannot be negative.");

        _characterController = GetComponent<CharacterController>();
        _inputHandler = GetComponent<PlayerInputHandler>();
        _transform = transform;
    }

    private void Update()
    {
        if (_characterController != null)
        {
            Rotate();
            Move();
        }
    }

    private void Rotate()
    {
        _transform.Rotate(Vector3.up * _rotateSpeed * _inputHandler.GetHorizontal() * Time.deltaTime);
    }

    private void Move()
    {
            Vector3 playerSpeed =  _transform.forward * _inputHandler.GetVertical() * _moveSpeed * Time.deltaTime;

            if (_characterController.isGrounded)
                _characterController.Move(playerSpeed + Vector3.down);
            else
                _characterController.Move(_characterController.velocity + Physics.gravity * Time.deltaTime);       
    }
}