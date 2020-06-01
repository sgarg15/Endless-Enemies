using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour {

  public Transform player;
  

  public float smoothSpeed = 10f;
  public Vector3 offset;
  bool playerIsDead;
  LivingEntity _playerEntity;

  void Start(){
    _playerEntity = FindObjectOfType<Player> ();
    _playerEntity.OnDeath += PlayerDeath;
  }

  void FixedUpdate() {
    if(!playerIsDead){
      Vector3 desiredPosition = player.position + offset;
      Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
      transform.position = smoothedPosition;
    }
  }

  void PlayerDeath(){
    playerIsDead = true;
  }
}
