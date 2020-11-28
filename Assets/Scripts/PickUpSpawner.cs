using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour {

    public Transform[] pickUpObjects;
    float timeBetweenSpawnObject;
    float spawnTime;
    public float minPickUpSpawnTime;
    public float maxPickUpSpawnTime;
    int pickUpObjectIndex;

    MapGenerator map;

    void Start() {
        map = FindObjectOfType<MapGenerator>();
        spawnTime = Time.time + 5;
    }

    void Update() {
        if (Time.time > spawnTime) {
            pickUpObjectIndex = Random.Range(0, pickUpObjects.Length);
            timeBetweenSpawnObject = Random.Range(minPickUpSpawnTime, maxPickUpSpawnTime);
            Transform objectSpawnPos = map.GetRandomOpenTile();
            Instantiate(pickUpObjects[pickUpObjectIndex], objectSpawnPos.position + Vector3.up, Quaternion.identity);

            spawnTime = Time.time + timeBetweenSpawnObject;
        }
    }
}
