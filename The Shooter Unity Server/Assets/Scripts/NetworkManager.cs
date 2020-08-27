using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapGenerator;

[RequireComponent(typeof(MapGenerator))]
public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;
    public int numberOfPlayersAlive = 0;

    private MapGenerator map;

    public GameObject playerPrefab;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            map = Object.FindObjectOfType<MapGenerator>();
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying objects!");
            Destroy(this);
        }
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

        Server.Start(50, 7777);
    }

    private void OnApplicationQuit()
    {
        Server.Stop();
    }

    public Player InstantiatePlayer()
    {
        Transform playerPosition = map.GetRandomOpenTile();
        numberOfPlayersAlive++;
        return Instantiate(playerPrefab, playerPosition.position, Quaternion.identity).GetComponent<Player>();
    }
}
