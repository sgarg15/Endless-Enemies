using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class Enemy : LivingEntity {

  NavMeshAgent pathFinder;
  Transform target;

  protected override void Start() {
    base.Start ();
    pathFinder = GetComponent<NavMeshAgent> ();
    target = GameObject.FindGameObjectWithTag("Player").transform;

    StartCoroutine(UpdatePath());
  }

  void Update() {
  }

  IEnumerator UpdatePath(){
    float refershRate = 0.25f;

    while (target != null){
      Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
      if(!dead){
        pathFinder.SetDestination (target.position);
      }
      yield return new WaitForSeconds(refershRate);
    }
  }
}
