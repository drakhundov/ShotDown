using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    // looking for enemy at scene
    private GameObject _enemy;

    void Update()
    {
        // if enemy killed, spawn new enemy
        if (_enemy == null)
        {
            _enemy = Instantiate(enemyPrefab) as GameObject;
            _enemy.transform.position = transform.position;

            _enemy.transform.Rotate (0, Random.Range (0, 360), 0);
        }
    }
}
