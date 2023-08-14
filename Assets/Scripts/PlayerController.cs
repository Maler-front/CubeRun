using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;                                //Скорость
    [SerializeField] private float _pivot;                                //Сила поворота
    [SerializeField] private float _jumpHeight;                           //Сила прыжка
    [SerializeField] private GameObject _player;
    private float screen;
    private bool isRoad;
                                                       //Проверка на нахождении в воздухе
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
            if (Input.GetTouch(i).position.x > screen / 2 )
                Move(1.0f);
            else if (Input.GetTouch(i).position.x < screen / 2)
                Move(-1.0f);

            ++i;
        }
    }

    void FixedUpdate()
    {
        _rigidbody.AddForce(0, 0, _speed * Time.deltaTime); //Ускорение вперед

        if (Input.GetKey("a"))                     //Движение влево
            Move(-1.0f);
        else if (Input.GetKey("d"))                     //Движение вправо
            Move(1.0f);

        if (isRoad)                                //Если на земле
        {
            if (Input.GetKey("e"))                 //Прыгаем на "е"
                Jump();

            else if (Input.touchCount == 2)             //Прыгаем на два касания
                Jump();
        }
    }

    private void Jump()                            //Метод на прыжок
    {
        _rigidbody.AddForce(0, _jumpHeight * Time.deltaTime, 0, ForceMode.VelocityChange);
    }

    private void Move(float HorizontalMovement)   //Метод на скорость 
    {
        _rigidbody.AddForce(HorizontalMovement * _pivot * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }
}