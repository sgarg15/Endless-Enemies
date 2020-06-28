using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class playerFollower : NetworkBehaviour {

  public Transform player;

  public float smoothSpeed = 10f;
  public Vector3 offset;

  [Command]
  public void CmdsetCamNetID() {
    uint ni = gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().netId;
    var nid = ni;
    CmdPlayerFollow(nid);
  }
  
  [Command]
  void CmdPlayerFollow(uint ni) {  
    RpcPlayerFollower(ni);//Target just one Player
  }

  [ClientRpc]
  private void RpcPlayerFollower(uint PlayerNetID) {
    var PlayerNetID_toCompare = gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().netId;
    if (PlayerNetID == PlayerNetID_toCompare) {
        FollowPlayer(PlayerNetID);
    }
  }

  public void FollowPlayer(uint PlayerNetID) {
    Vector3 desiredPosition = gameObject.transform.parent.gameObject.transform.position + offset;
    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    transform.position = smoothedPosition;
  }
}