using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent (typeof (PlayerControl))]
[RequireComponent (typeof (GunController))]
public class Player : LivingEntity {

  public float moveSpeed = 5;
  public float regainSpeed = 0.1f;

  public CrossHairs crossHairs;

  Camera viewCamera;
  PlayerControl controller;
  GunController gunController;
  AudioManager audioManager;

  float interpolation = 0;

  protected override void Start() {
    base.Start ();
  }

  void Awake(){
    controller = GetComponent<PlayerControl> ();
    gunController = GetComponent<GunController> ();
    viewCamera = Camera.main;
    StartCoroutine(HealthRegain());
  }

  void OnNewWave(int waveNumber){
    health = startingHealth;
    gunController.EquipGun(waveNumber - 1);
  }

  void Update() {
    //Movement Input
    Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    Vector3 moveVelocity = moveInput.normalized * moveSpeed;
    controller.Move(moveVelocity);

    //Look Input
    Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
    Plane groundPlane = new Plane(Vector3.up, Vector3.up * gunController.GunHeight);
    float rayDistance;

    if(groundPlane.Raycast(ray, out rayDistance)){
      Vector3 pointOfIntersection = ray.GetPoint(rayDistance);
      //Debug.DrawLine(ray.origin, pointOfIntersection, Color.red);
      controller.LookAt(pointOfIntersection);
      crossHairs.transform.position = pointOfIntersection;
      crossHairs.DetectTarget(ray);
      if((new Vector2(pointOfIntersection.x, pointOfIntersection.z) - new Vector2(transform.position.x, transform.position.z)).sqrMagnitude > Mathf.Pow(1.9f, 2f)){
        gunController.Aim(pointOfIntersection);
      }
    }

    //Weapon Input
    if(Input.GetMouseButton(0)){
      gunController.OnTriggerHold();
    }
    if(Input.GetMouseButtonUp(0)){
      gunController.OnTriggerRelease();
    }
    if(Input.GetKeyDown(KeyCode.R)){
      gunController.Reload();
    }

    if(transform.position.y < -10){
      TakeDamage(health);
    }
  }

  IEnumerator HealthRegain(){
    while (true){
      if(health < (startingHealth * 0.85)){
        interpolation += Time.deltaTime / regainSpeed;
        health = Mathf.Lerp(health, startingHealth, interpolation);
        //health += regainSpeed;
        yield return new WaitForSeconds(2);
      } else {
        yield return null;
      }
    }
  }

  public override void Die(){
    AudioManager.instance.PlaySound("Player Death", transform.position);
    base.Die();
  }
}
