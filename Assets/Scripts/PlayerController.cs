using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedForward;                               
    [SerializeField] private float _jumpHeight;                           
    [SerializeField] private GameObject _player;
    [SerializeField] private float _rayDistance;
    private bool onHit;

    private void Awake()
    {
        _rigidbody = _player.GetComponent<Rigidbody>();
        InputManager.OnOneFingerScreenTouched += (x) => Move(x);
        InputManager.OnTwoFingerScreenTouched += Jump;
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        Debug.DrawRay(transform.position, -transform.up * _rayDistance, Color.blue);
        onHit = Physics.Raycast(ray, _rayDistance);

        _rigidbody.AddForce(0, 0, _speedForward * Time.deltaTime);
    }

    private void Jump()
    {
        if (onHit)
            _rigidbody.AddForce(0, _jumpHeight * Time.deltaTime, 0, ForceMode.Impulse);
    }

    private void Move(float HorizontalMovement)
    {
        if (onHit)
        {
            int sign = Screen.width / 2 > HorizontalMovement ? -1 : 1;
            _rigidbody.MovePosition(_rigidbody.position + Vector3.right * sign * _speed * Time.fixedDeltaTime);
        }
    }
}