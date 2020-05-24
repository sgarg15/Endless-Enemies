﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

  public LayerMask collisionMask;
  float speed = 10;
  float damage = 1;

  float lifeTime = 2;
  float skinWidth = 0.1f;

  void Start(){
    Destroy(gameObject, lifeTime);

    Collider[] initialCollision = Physics.OverlapSphere(transform.position, 0.1f, collisionMask);
    if(initialCollision.Length > 0){
      OnHitObject(initialCollision[0]);
    }
  }

  public void SetSpeed(float newSpeed){
    speed = newSpeed;
  }

  // Update is called once per frame
  void Update() {
    float moveDistance = speed * Time.deltaTime;
    CheckCollisions(moveDistance);
    transform.Translate(Vector3.forward * moveDistance);
  }

  void CheckCollisions(float moveDistance){
    Ray ray = new Ray(transform.position, transform.forward);
    RaycastHit hit;

    if(Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide)){
      OnHitObject(hit);
    }
  }

  void OnHitObject(RaycastHit hit){
    IDamageable damageableObject = hit.collider.GetComponent<IDamageable> ();
    if(damageableObject != null){
      damageableObject.TakeHit(damage, hit);
    }
    GameObject.Destroy (gameObject);
  }

  void OnHitObject(Collider c) {
    IDamageable damageableObject = c.GetComponent<IDamageable> ();
    if(damageableObject != null){
      damageableObject.TakeDamage(damage);
    }
    GameObject.Destroy (gameObject);
  }
}
