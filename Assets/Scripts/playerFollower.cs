using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class playerFollower : NetworkBehaviour
{
    //test

    [SerializeField]
    Transform player;

    public float smoothSpeed = 10f;
    public Vector3 offset;

    // public override void OnStartAuthority(){
    //   player = FindObjectsOfType<PlayerControl>();
    // }

    [ClientCallback]
    void Update()
    {
        Debug.Log("in Update");
        CmdsetCamNetID();
    }

    [Command(ignoreAuthority = true)]
    private void CmdsetCamNetID()
    {
        NetworkIdentity ni = player.GetComponent<NetworkIdentity>().connectionToServer.identity;
        var nid = ni;
        Debug.Log("cam net id");
        RpcPlayerFollow(nid);
    }

    [ClientRpc]
    private void RpcPlayerFollow(NetworkIdentity ni)
    {
        if (hasAuthority)
        {
            return;
        }
    
        Debug.Log("Following player");
        Debug.Log("player position " + ni.connectionToServer.identity.gameObject.transform.position);
        Debug.Log("player name" + ni.connectionToServer.identity.gameObject.name);
        Debug.Log("ni" + ni);
        Vector3 desiredPosition = ni.connectionToServer.identity.gameObject.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(ni.connectionToServer.identity.gameObject.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        ni.connectionToServer.identity.gameObject.transform.position = smoothedPosition;
        //}
    }

}




// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Mirror;

// public class playerFollower : NetworkBehaviour {

//   [SerializeField]
//   PlayerControl player;

//   [SerializeField]
//   private float smoothSpeed = 10f;
//   [SerializeField]
//   private Vector3 offset;
  
//   public override void OnStartAuthority(){
//     enabled = true;

//     player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
//   }

//   [ClientCallback]
//   private void Update() {
//     Debug.Log("player position " + player.transform.position);
//     Vector3 desiredPosition = player.transform.position + offset;
//     Debug.Log("desiredPosition " + desiredPosition);
//     Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
//     Debug.Log("smoothedPosition " + smoothedPosition);      
//     transform.position = smoothedPosition;
//     Debug.Log("transform position " + transform.position);
//   }
//   // [ClientCallback]
//   // private void FixedUpdate() {
//   // }
// }
