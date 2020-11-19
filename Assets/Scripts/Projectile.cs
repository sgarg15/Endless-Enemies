using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GunController))]
[RequireComponent(typeof(Player))]
public class Projectile : MonoBehaviour {

    public LayerMask collisionMask;
    float speed = 10;
    float damage = 1;

    float lifeTime = 2;
    float skinWidth = 0.1f;

    GunController gunController;
    Player player;

    void Start() {
        Destroy(gameObject, lifeTime);

        Collider[] initialCollision = Physics.OverlapSphere(transform.position, 0.1f, collisionMask);
        gunController = GetComponent<GunController>();
        player = FindObjectOfType<Player>();
        if (initialCollision.Length > 0) {
            OnHitObject(initialCollision[0], transform.position);
        }
    }

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }

    // Update is called once per frame
    void Update() {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    void CheckCollisions(float moveDistance) {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Vector3 offset = new Vector3(0, 10, 0);

        if (Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide)) {
            OnHitObject(hit.collider, hit.point);
        }
    }

    void OnHitObject(Collider c, Vector3 hitPoint) {
        IDamageable damageableObject = c.GetComponent<IDamageable>();
        if (damageableObject != null) {
            damageableObject.TakeHit(damage, hitPoint, transform.forward);
        }
        if (player.sniperEquipped == false) {
            GameObject.Destroy(gameObject);
        } else if (c.gameObject.GetComponent<BoxCollider> () != null) {
            GameObject.Destroy(gameObject);
        }
    }
}
