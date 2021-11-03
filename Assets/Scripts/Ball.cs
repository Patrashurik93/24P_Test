using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] float powerMultiplier = 5;

    private Rigidbody _rb;
    private Player _player;
    private bool _isPushed;

    public event Action onKilled; 

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        _player.PlayerUnTouched += Push;
        _player.PlayerTouched += ShotDirection;
        SpawnArrow();
    }

    private void OnDisable()
    {
        _player.PlayerUnTouched -= Push;
        _player.PlayerTouched -= ShotDirection;
        Destroy(arrow.gameObject);
    }
    private void Push()
    {
        Vector3 direction = GetDirection();
        float power = GetPower();
        _rb.AddForce(direction.normalized * power * powerMultiplier, ForceMode.Impulse);
        SetPushed(true);
    }

    private float GetPower()
    {
        return Vector3.Distance(_player.transform.position, transform.position);
    }

    private Vector3 GetDirection()
    {
        return _player.transform.position - transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            onKilled?.Invoke();
            other.GetComponent<Enemy>().Kill();
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "EnemyGate")
        {
            onKilled?.Invoke();
            other.GetComponent<Gates>().Kill();
            Destroy(gameObject);
        }
    }



    public bool GetPushed()
    {
        return _isPushed;
    }

    private void SetPushed(bool value)
    {
        _isPushed = value;
    }

    public void ShotDirection()
    {
        arrow.transform.LookAt(_player.transform);
        arrow.transform.localScale = new Vector3(1, 1, GetPower());
        arrow.transform.position = gameObject.transform.position;
    }

    public void SpawnArrow()
    {
        arrow = Instantiate(arrow) as GameObject;
        arrow.transform.localScale = new Vector3(0, 0, 0);
    }
}
