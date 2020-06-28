using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class playerFollower : NetworkBehaviour {

  public Transform player;

  public float smoothSpeed = 10f;
  public Vector3 offset;

  [ClientCallback]
  void Update() {
    Debug.Log("in Update");
    CmdsetCamNetID();
  }

  [Command]
  private void CmdsetCamNetID() {
    uint ni = player.GetComponent<NetworkIdentity>().netId;
    var nid = ni;
    Debug.Log("cam net id");
    RpcPlayerFollow(nid);
  }

  [ClientRpc]
  private void RpcPlayerFollow(uint ni) {
    // var PlayerNetID_toCompare = gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().netId;
    // if (PlayerNetID == PlayerNetID_toCompare) {
      Debug.Log("Following player");
      Vector3 desiredPosition = player.position + offset;
      Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
      transform.position = smoothedPosition;
    //}
  }

}