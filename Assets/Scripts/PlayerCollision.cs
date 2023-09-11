using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public PlayerController movement;

    private float _timeLeft = 0.7f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Wall")
        {
            Debug.Log(collision.collider.name);
            movement.enabled = false;
        }
    }

    private void Update()
    {
        if(!movement.enabled)
        {
            _timeLeft -= Time.deltaTime;

           if (_timeLeft < 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        }

    }
}
