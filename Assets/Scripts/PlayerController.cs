using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    public float Speed;                                //��������
    public float Pivot;                                //���� ��������
    public float JumpHeight;                           //���� ������

    private bool isRoad;
                                                       //�������� �� ���������� � �������
    private void OnCollisionEnter(Collision collision) //�� �����
    {
        if (collision.gameObject.tag == "Road")
        {
            isRoad = true;
        }
    }

    private void OnCollisionExit(Collision collision) //�����
    {
        if (collision.gameObject.tag == "Road")
        {
            isRoad = false;
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(0, 0, Speed * Time.deltaTime); //��������� ������

        if (Input.GetKey("a"))                     //�������� �����
        {
            Move(-1.0f);
        }
        if (Input.GetKey("d"))                     //�������� ������
        {
            Move(1.0f);
        }
        if (isRoad)                                //���� �� �����
        {
            if (Input.GetKey("e"))                 //������� �� "�"
            {
                Jump();
            }

            if (Input.touchCount == 2)             //������� �� ��� �������
            {
                Jump();
            }
        }

        OnLeftBottonDown();                        //���������� �����
        OnRightBottonDown();                       //���������� ������

    }

    public void OnLeftBottonDown()                 //������ �� �������� ����� ��� �������
    {
        Move(-1.0f);
    }

    public void OnRightBottonDown()                //������ �� �������� ������ ��� �������
    {
        Move(1.0f);
    }

    private void Jump()                            //����� �� ������
    {
        rb.AddForce(0, JumpHeight * Time.deltaTime, 0, ForceMode.VelocityChange);
    }

    private void Move(float HorizontalMovement)   //����� �� �������� 
    {
        rb.AddForce(HorizontalMovement * Pivot * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }
}