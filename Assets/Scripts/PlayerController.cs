using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;                                //Скорость
    [SerializeField] private float _pivot;                                //Сила поворота
    [SerializeField] private float _jumpHeight;                           //Сила прыжка
    [SerializeField] private GameObject _player;
    private float screen;
    private bool isRoad;
    private InputManager inputManager;

    public static Func<int, float> onTouched;

    private void OnCollisionEnter(Collision collision) //На земле
    {
        if (collision.gameObject.tag == "Road" || collision.gameObject.CompareTag("Road"))
            isRoad = true;
    }

    private void OnCollisionExit(Collision collision) //Летим
    {
        if (collision.gameObject.tag == "Road" || collision.gameObject.CompareTag("Road"))
            isRoad = false;
    }

    private void Start()
    {
        screen = Screen.width;
        _rigidbody = _player.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        int i = 0;
        
        while (i < Input.touchCount && i < 2)
        {
            if (onTouched?.Invoke(i) > screen / 2 )
                Move(1.0f);
            else if (onTouched?.Invoke(i) < screen / 2)
                Move(-1.0f);

            ++i;
        }
    }

    void FixedUpdate()
    {
        _rigidbody.AddForce(0, 0, _speed * Time.deltaTime);

        if (Input.GetKey("a"))
            Move(-1.0f);
        else if (Input.GetKey("d"))
            Move(1.0f);

        if (isRoad)
        {
            if (Input.GetKey("e"))
                Jump();

            else if (Input.touchCount == 2)
                Jump();
        }
    }

    private void Jump()
    {
        _rigidbody.AddForce(0, _jumpHeight * Time.deltaTime, 0, ForceMode.VelocityChange);
    }

    private void Move(float HorizontalMovement)   //Метод на скорость 
    {
        _rigidbody.AddForce(HorizontalMovement * _pivot * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }
}