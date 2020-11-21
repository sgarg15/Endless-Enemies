using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Spawner))]
[RequireComponent(typeof(GunController))]
public class Gun : MonoBehaviour {

    [Header("Main Gun Attributes")]
    public FireMode fireMode;
    public enum FireMode { Auto, Burst, Single };

    public Transform[] projectileSpawn;
    public Projectile projectile;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;
    public int burstCount;
    public int projectilesPerMag;
    public float reloadTime = 0.3f;
    public float _maxReloadAngle = 30;

    [Header("Recoil")]
    public Vector2 kickMinMax = new Vector2(0.5f, 0.2f);
    public Vector2 recoilAngleMinMax = new Vector2(3, 5);
    public float recoilMoveSettleTime = 0.1f;
    public float recoilRotationSettleTime = 0.1f;

    [Header("Effects")]
    public Transform shell;
    public Transform shellEjection;
    public AudioClip shootAudio;
    public AudioClip reloadAudio;

    MuzzleFlash muzzleFlash;
    Player player;
    Spawner spawner;
    GunController gunController;
    float nextShotTime;

    bool triggerReleasedSinceLastShot;
    public int shotsRemainingInBurst { get; private set; }
    public int projectileRemainingInMag { get; private set; }
    bool isReloading;

    Vector3 recoilSmoothDampVelocity;
    float recoilAngle;
    float recoilRotSmoothDampVelocity;

    void Start() {
        muzzleFlash = GetComponent<MuzzleFlash>();
        player = FindObjectOfType<Player>();
        gunController = FindObjectOfType<GunController>();
        spawner = FindObjectOfType<Spawner>();
        shotsRemainingInBurst = burstCount;
        projectileRemainingInMag = projectilesPerMag;
    }

    void LateUpdate() {
        //Animate recoil
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Vector3.zero, ref recoilSmoothDampVelocity, recoilMoveSettleTime);
        recoilAngle = Mathf.SmoothDamp(recoilAngle, 0, ref recoilRotSmoothDampVelocity, recoilRotationSettleTime);
        transform.localEulerAngles = transform.localEulerAngles + Vector3.right * -recoilAngle;

        if (!isReloading && projectileRemainingInMag == 0) {
            if (player.sniperEquipped) {
                gunController.EquipGun(spawner.currentWaveNumber - 1);
                return;
            }
            Reload();
        }
    }

    void Shoot() {
        if (!isReloading && Time.time > nextShotTime && projectileRemainingInMag > 0) {

            if (fireMode == FireMode.Burst) {
                if (shotsRemainingInBurst == 0) {
                    return;
                }
                shotsRemainingInBurst--;
            } else if (fireMode == FireMode.Single) {
                if (!triggerReleasedSinceLastShot) {
                    return;
                }
            }

            for (int i = 0; i < projectileSpawn.Length; i++) {
                if (projectileRemainingInMag == 0) {
                    break;
                }
                projectileRemainingInMag--;
                nextShotTime = Time.time + msBetweenShots / 1000;
                Projectile newProjectile = Instantiate(projectile, projectileSpawn[i].position, projectileSpawn[i].rotation) as Projectile;
                newProjectile.SetSpeed(muzzleVelocity);
            }
            Instantiate(shell, shellEjection.position, shellEjection.rotation);
            muzzleFlash.Activate();
            transform.localPosition -= Vector3.forward * Random.Range(kickMinMax.x, kickMinMax.y);
            // recoilAngle += Random.Range(recoilAngleMinMax.x, recoilAngleMinMax.y);
            // recoilAngle = Mathf.Clamp(recoilAngle, 0, 30);

            AudioManager.instance.PlaySound(shootAudio, transform.position);
        }
    }

    public void Reload() {
        if (!isReloading && projectileRemainingInMag != projectilesPerMag) {
            StartCoroutine(AnimateReload());
            AudioManager.instance.PlaySound(reloadAudio, transform.position);
        }
    }

    IEnumerator AnimateReload() {
        isReloading = true;
        yield return new WaitForSeconds(0.2f);

        float reloadSpeed = 1f / reloadTime;
        float percent = 0;
        Vector3 intialRot = transform.localEulerAngles;
        float maxReloadAngle = _maxReloadAngle;

        while (percent < 1) {
            percent += Time.deltaTime * reloadSpeed;
            float interpolation = 4 * (-Mathf.Pow(percent, 2) + percent);
            float reloadAngle = Mathf.Lerp(0, maxReloadAngle, interpolation);
            transform.localEulerAngles = intialRot + Vector3.left * reloadAngle;

            yield return null;
        }
        isReloading = false;
        projectileRemainingInMag = projectilesPerMag;
    }

    public void Aim(Vector3 aimPoint) {
        if (!isReloading) {
            transform.LookAt(aimPoint);
        }
    }

    public void OnTriggerHold() {
        Shoot();
        triggerReleasedSinceLastShot = false;

    }

    public void OnTriggerRelease() {
        triggerReleasedSinceLastShot = true;
        shotsRemainingInBurst = burstCount;
    }
}
