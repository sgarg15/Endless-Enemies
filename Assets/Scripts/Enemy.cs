﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class Enemy : MonoBehaviour {

  NavMeshAgent pathFinder;
  Transform target;

  void Start() {
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
      pathFinder.SetDestination (target.position);
      yield return new WaitForSeconds(refershRate);
    }
  }
}
