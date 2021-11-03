using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action OnKilled;
    private Ball _ball;
    private GameManager _gamemanager;

    private void Awake()
    {
        _gamemanager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        MoveToBall();
    }


    private void MoveToBall()
    {
        _ball = FindObjectOfType<Ball>();
        if (_ball.GetPushed() == true)
        {
            Vector3 targetposition = new Vector3(_ball.transform.position.x, transform.position.y, transform.position.z);
            float delta = _gamemanager.GetSpeed() * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetposition, delta);
        }   
    }

    public void Kill()
    {
        OnKilled?.Invoke();
        //Destroy(this.gameObject);
    }
}
