using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;                                //��������
    [SerializeField] private float _pivot;                                //���� ��������
    [SerializeField] private float _jumpHeight;                           //���� ������
    [SerializeField] private GameObject _player;
    private float screen;
    private bool isRoad;
                                                       //�������� �� ���������� � �������
    private void OnCollisionEnter(Collision collision) //�� �����
    {
        if (collision.gameObject.tag == "Road" || collision.gameObject.CompareTag("Road"))
            isRoad = true;
    }

    private void OnCollisionExit(Collision collision) //�����
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
        _rigidbody.AddForce(0, 0, _speed * Time.deltaTime); //��������� ������

        if (Input.GetKey("a"))                     //�������� �����
            Move(-1.0f);
        else if (Input.GetKey("d"))                     //�������� ������
            Move(1.0f);

        if (isRoad)                                //���� �� �����
        {
            if (Input.GetKey("e"))                 //������� �� "�"
                Jump();

            else if (Input.touchCount == 2)             //������� �� ��� �������
                Jump();
        }
    }

    private void Jump()                            //����� �� ������
    {
        _rigidbody.AddForce(0, _jumpHeight * Time.deltaTime, 0, ForceMode.VelocityChange);
    }

    private void Move(float HorizontalMovement)   //����� �� �������� 
    {
        _rigidbody.AddForce(HorizontalMovement * _pivot * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }
}