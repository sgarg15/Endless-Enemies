using Mirror;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnSystem : NetworkBehaviour {

  [SerializeField]
  private GameObject playerPrefab = null;

  // MapGenerator map;

  public override void OnStartServer(){
    // map  = FindObjectOfType<MapGenerator>();
    NetworkManagerLobby.OnServerReadied += SpawnPlayer;
  }

  [ServerCallback]
  private void OnDestroy(){
    NetworkManagerLobby.OnServerReadied -= SpawnPlayer;
  }

  [Server]
  public void SpawnPlayer(NetworkConnection conn){
    // Transform playerSpawnPos = map.GetRandomOpenTile();

    GameObject playerInstance = Instantiate(playerPrefab, new Vector3(0,0,0), new Quaternion(0,0,0,0));
    NetworkServer.Spawn(playerInstance, conn);
  }
}
