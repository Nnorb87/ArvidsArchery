using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour{

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnFrequency = 10f;
    [SerializeField] private GameObject spawnerVisual;
    private float spawnTimer = 0;

    private void Awake() {
        spawnerVisual.SetActive(false);
    }

    private void Update() {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f) {
            spawnTimer = spawnFrequency;
            Instantiate(enemyPrefab, transform.position, transform.rotation);
        }
    }

}
