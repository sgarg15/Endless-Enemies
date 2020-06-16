using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour {

  public float delay = 3f;
  public float radius = 5f;
  public float force = 700f;

  public GameObject explosionEffect;
  public AudioClip explosionAudio;

  Transform player;

  float countdown;
  bool hasExploded;

  void Start(){
    countdown = delay;
    player = GameObject.FindGameObjectWithTag("Player").transform;
  }

  void Update(){
      countdown -= Time.deltaTime;
      if(countdown <= 0f && !hasExploded){
        hasExploded = true;
        Explode();
      }
  }

  void Explode(){

    GameObject explosionEffectVar = Instantiate(explosionEffect, transform.position, transform.rotation);

    Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

    foreach(Collider nearbyObject in colliders){
      Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
      if(rb != null){
        rb.AddExplosionForce(force, transform.position, radius);
      }

      Enemy enemies = nearbyObject.GetComponent<Enemy>();
      Transform enemiesPos = nearbyObject.GetComponent<Transform>();

      if(enemies != null && enemiesPos != null){
        Vector3 dirBetweenGrenadeEnemy = enemiesPos.position - transform.position;
        enemies.TakeHit(10, enemiesPos.position, dirBetweenGrenadeEnemy.normalized);
      }
    }
    AudioManager.instance.PlaySound(explosionAudio, player.position);
    Destroy(gameObject);
    Destroy(explosionEffectVar, 2f);
  }
}
