using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour {

  public float delay = 3f;
  public float radius = 5f;
  public float force = 700f;

  float countdown;
  bool hasExploded;

  void Start(){
    countdown = delay;
  }

  void Update(){
    countdown -= Time.deltaTime;
    if(countdown <= 0f && !hasExploded){
      Explode();
      hasExploded = true;
    }
  }

  void Explode(){

    Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

    foreach(Collider nearbyObject in colliders){
      Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
      if(rb != null){
        rb.AddExplosionForce(force, transform.position, radius);
      }

      Enemy enemies = nearbyObject.GetComponent<Enemy>();
      if(enemies != null){
        enemies.TakeHit(10, new Vector3(0,0,0), Vector3.forward);
      }
    }

    Destroy(gameObject);
  }
}
