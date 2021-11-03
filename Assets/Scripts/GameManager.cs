using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gatePrefab;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float incrasingEnemySpeedByLevel = 0.5f;
    [SerializeField] float enemySpeed;
    int currentLevel = 1;

    private List<GameObject> spawnedPrefabs = new List<GameObject>();

    void Start()
    {
        SpawnGates();
    }

    private void SpawnGates()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            var instance = Instantiate(gatePrefab, transform.position, Quaternion.identity);
            instance.transform.position = spawnPoints[i].transform.position;

            instance.GetComponent<Gates>().OnKilled += DeleteGates;
            spawnedPrefabs.Add(instance);
        }
    }

    private void LevelUp()
    {
        SpawnGates();
        Enemy enemy = FindObjectOfType<Enemy>();

        SetSpeedByLevel(incrasingEnemySpeedByLevel);
        currentLevel++;
    }

    private void DeleteGates(GameObject gate)
    {
        gate.GetComponent<Gates>().OnKilled -= DeleteGates;
        spawnedPrefabs.Remove(gate);
        if (spawnedPrefabs.Count == 0)
        {
            LevelUp();
        }
    }

    public void SetSpeedByLevel(float addspeed)
    {
        enemySpeed += addspeed;
    }
    public float GetSpeed()
    {
        return enemySpeed;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
