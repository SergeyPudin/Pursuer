using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class BotMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _distance;
    [SerializeField] private Player _player;
    [SerializeField] private float _groundOffsetDistance = 0.5f ;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody _rigidbody;
    private Transform _transform;

    private void Awake()
    {
        if (_distance < 0)
            throw new ArgumentOutOfRangeException(nameof(_distance), "Distance cannot be negative.");

        if (_moveSpeed < 0)
            throw new ArgumentOutOfRangeException(nameof(_moveSpeed), "Speed cannot be negative.");

        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
    }

    private void Update()
    {
        if (_player == null)
            return;

        Vector3 target = _player.transform.position;
        float currentDistance = Vector3.Distance(_rigidbody.position, target);

        if (currentDistance > _distance && IsGrounded())
        {
            Vector3 direction = (target - _transform.position).normalized;
            Vector3 currentVelocity = _rigidbody.velocity;

            _rigidbody.velocity = new Vector3(direction.x * _moveSpeed, currentVelocity.y, direction.z * _moveSpeed);

            _transform.LookAt(new Vector3(target.x, _transform.position.y, target.z));
        }
        else if (IsGrounded())
        {
            Vector3 currentVelocity = _rigidbody.velocity;
            _rigidbody.velocity = new Vector3(0, currentVelocity.y, 0);
        }
    }

    private bool IsGrounded()
    {
        if (_groundOffsetDistance < 0)
            throw new ArgumentOutOfRangeException("GroundOffset is negative");

        return Physics.Raycast(_transform.position, Vector3.down, _groundOffsetDistance, _groundLayer);
    }
}