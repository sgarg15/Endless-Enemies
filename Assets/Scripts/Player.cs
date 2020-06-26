using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour {

public float moveSpeed = 5;
public Rigidbody2D rigidbody3d;

void Update() {
  if (!isLocalPlayer){
    return;
  }

  //Movement Input
  Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
  Vector3 moveVelocity = moveInput.normalized * moveSpeed;
  rigidbody3d.velocity = moveVelocity * Time.fixedDeltaTime;
  }
}
