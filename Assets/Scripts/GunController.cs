using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

  public Transform weaponHold;
  public Gun startingGun;
  Gun equippedGun;

  void Start(){
    if(startingGun != null){
      EquipGun(startingGun);
    }
  }

  public void EquipGun(Gun gunToEpic){
    if (equippedGun != null){
      Destroy(equippedGun.gameObject);
    }
    equippedGun = Instantiate (gunToEpic, weaponHold.position, weaponHold.rotation) as Gun;
    equippedGun.transform.parent = weaponHold;
  }

  public void Shoot(){
    if(equippedGun != null){
      equippedGun.Shoot();
    }
  }
}
