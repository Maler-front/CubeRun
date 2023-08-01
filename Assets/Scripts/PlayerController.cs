using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    public float Speed;                                //Скорость
    public float Pivot;                                //Сила поворота
    public float JumpHeight;                           //Сила прыжка

    private bool isRoad;
                                                       //Проверка на нахождении в воздухе
    private void OnCollisionEnter(Collision collision) //На земле
    {
        if (collision.gameObject.tag == "Road")
        {
            isRoad = true;
        }
    }

    private void OnCollisionExit(Collision collision) //Летим
    {
        if (collision.gameObject.tag == "Road")
        {
            isRoad = false;
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(0, 0, Speed * Time.deltaTime); //Ускорение вперед

        if (Input.GetKey("a"))                     //Движение влево
        {
            Move(-1.0f);
        }
        if (Input.GetKey("d"))                     //Движение вправо
        {
            Move(1.0f);
        }
        if (isRoad)                                //Если на земле
        {
            if (Input.GetKey("e"))                 //Прыгаем на "е"
            {
                Jump();
            }

            if (Input.touchCount == 2)             //Прыгаем на два касания
            {
                Jump();
            }
        }

        OnLeftBottonDown();                        //Двигаемься влево
        OnRightBottonDown();                       //Двигаемься вправо

    }

    public void OnLeftBottonDown()                 //Кнопка на движение влево при нажатии
    {
        Move(-1.0f);
    }

    public void OnRightBottonDown()                //Кнопка на движение вправо при нажатии
    {
        Move(1.0f);
    }

    private void Jump()                            //Метод на прыжок
    {
        rb.AddForce(0, JumpHeight * Time.deltaTime, 0, ForceMode.VelocityChange);
    }

    private void Move(float HorizontalMovement)   //Метод на скорость 
    {
        rb.AddForce(HorizontalMovement * Pivot * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }
}