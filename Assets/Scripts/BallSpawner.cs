using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform startingBallPos;
    [SerializeField] Transform startingEnemyPos;
    [SerializeField] Transform playerStartPos;
    private GameObject _ball;
    private GameObject _enemy;
    private Player _player;


    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }
    void Start()
    {
        SpawnBall();
        SpawnEnemy();
    }
    public void SpawnBall()
    {
        _player.gameObject.SetActive(true);
        _player.transform.position = playerStartPos.position;
        var instance = Instantiate(ballPrefab, startingBallPos.position, Quaternion.identity);
        _ball = instance;
        _ball.GetComponent<Ball>().onKilled += DeleteBall;
    }

    private void SpawnEnemy()
    {
        var instance = Instantiate(enemyPrefab, startingEnemyPos.position, Quaternion.identity);
        _enemy = instance;
        //_enemy.GetComponent<Enemy>().OnKilled += DeleteEnemy;
    }

    public void DeleteBall()
    {
        _ball.GetComponent<Ball>().onKilled -= DeleteBall;
        _ball = null;
        SpawnBall();
    }

    /*private void DeleteEnemy()
    {
        _enemy.GetComponent<Enemy>().OnKilled -= DeleteEnemy;
        _enemy = null;
        SpawnEnemy();
    }*/
}
