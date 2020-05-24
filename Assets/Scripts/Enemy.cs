using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class Enemy : LivingEntity {

  public enum State {Idle, Chasing, Attacking};
  State currentState;

  NavMeshAgent pathFinder;
  Transform target;
  Material skinMaterial;

  Color originalColor;

  float attackDistanceThershold = .5f;
  float timeBetweenAttacks = 1;

  float nextAttackTime;
  float myCollisionRadius;
  float targetCollisionRadius;

  protected override void Start() {
    base.Start ();
    pathFinder = GetComponent<NavMeshAgent> ();
    skinMaterial = GetComponent<Renderer> ().material;
    originalColor = skinMaterial.color;

    currentState = State.Chasing;
    target = GameObject.FindGameObjectWithTag("Player").transform;

    myCollisionRadius = GetComponent<CapsuleCollider> ().radius;
    targetCollisionRadius = GetComponent<CapsuleCollider> ().radius;

    StartCoroutine(UpdatePath());
  }

  void Update() {
    if(Time.time > nextAttackTime){
      float sqrDistToTarget = (target.position - transform.position).sqrMagnitude;
      if(sqrDistToTarget < Mathf.Pow(attackDistanceThershold + myCollisionRadius + targetCollisionRadius, 2)){
        nextAttackTime = Time.time + timeBetweenAttacks;
        StartCoroutine(Attack());
      }
    }
  }

  IEnumerator Attack(){

    currentState = State.Attacking;
    pathFinder.enabled = false;

    Vector3 originalPosition = transform.position;
    Vector3 dirToTarget = (target.position - transform.position).normalized;
    Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius);

    float attackSpeed = 3;
    float percent = 0;


    skinMaterial.color = Color.red;

    while(percent <= 1){
      percent += Time.deltaTime * attackSpeed;
      float interpolation = 4 * (-Mathf.Pow(percent, 2) + percent);
      transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

      yield return null;
    }

    skinMaterial.color = originalColor;
    currentState = State.Chasing;
    pathFinder.enabled = true;
  }

  IEnumerator UpdatePath(){
    float refershRate = 0.25f;

    while (target != null){
      if(currentState == State.Chasing){
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThershold/2);
        if(!dead){
          pathFinder.SetDestination (targetPosition);
        }
      }
      yield return new WaitForSeconds(refershRate);
    }
  }
}
