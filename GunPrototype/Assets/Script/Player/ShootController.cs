using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShootController : MonoBehaviour {
    private PlayerMovement pm;
    private HackController hc;
    private Image akImage;
    private Image smgImage;
    private Image sniperImage;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject smgBulletPrefab;
    [SerializeField] private GameObject sniperBulletPrefab;
    [SerializeField] private GameObject gunPrefab;

    [HideInInspector] public int gunType; //0 AK, 1 SMG, 2 Sniper
    public bool shootable;

    private float fireRate = 0.2f;
    public Transform firingPoint;
    private float timeUntilFire;

    [HideInInspector]public float reloadTime;

    private int akBullet;
    private int smgBullet;
    private int sniperBullet;

    public int akMag;
    public int smgMag;
    public int sniperMag;

    public float akReloadTime;
    public float smgReloadTime;
    public float sniperReloadTime;

    private Vector2 lookDirection;
    private float lookAngle;
    public float offset;

    private void Start() {
        pm = gameObject.GetComponent<PlayerMovement>();
        akImage = GameObject.Find("AK").GetComponent<Image>();
        smgImage = GameObject.Find("SMG").GetComponent<Image>();
        sniperImage = GameObject.Find("Sniper").GetComponent<Image>();
        hc = GetComponent<HackController>();

        reloadTime = 0;
        if (LevelManager.InLevel()) {
            shootable = true;
            gunType = 0;
            akImage.enabled = true;
            smgImage.enabled = false;
            sniperImage.enabled = false;
        } else {
            shootable = false;
            akImage.enabled = false;
            smgImage.enabled = false;
            sniperImage.enabled = false;
        }

        ResetBullet();
    }

    private void Update() {
        if (!TimeManager.isPause) {
            if (shootable && !hc.isHacking) {
                Shoot();
            }
            if (shootable) {
                ChangeGun();
            }
            if (Input.GetKeyDown(KeyCode.R) && AmmoPercentage() != 1) {
                Reload();
            }
        }
    }

    private void Shoot() {
        if (Input.GetMouseButton(0) && timeUntilFire < Time.time && Ammo() >= 0 && reloadTime == 0) {
            if (Ammo() == 0) {
                Reload();
            }
            GameObject bulletClone;
            switch (gunType) {
                case 0:
                    akBullet--;
                    bulletClone = Instantiate(bulletPrefab);
                    timeUntilFire = Time.time + fireRate;
                    break;
                case 1:
                    smgBullet--;
                    bulletClone = Instantiate(smgBulletPrefab);
                    timeUntilFire = Time.time + 0.1f;
                    break;

                case 2:
                    sniperBullet--;
                    bulletClone = Instantiate(sniperBulletPrefab);
                    timeUntilFire = Time.time + 0.8f;
                    break;

                default:
                    bulletClone = Instantiate(bulletPrefab);
                    timeUntilFire = Time.time + fireRate;
                    break;
            }

            lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            firingPoint.rotation = Quaternion.Euler(0, 0, lookAngle);

            bulletClone.transform.position = firingPoint.position;
            bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
        }
    }

    private void ShootGun() {
        Debug.Log("ShootGun");
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        firingPoint.rotation = Quaternion.Euler(0, 0, lookAngle);

        GameObject GunClone = Instantiate(gunPrefab);
        GunClone.transform.position = firingPoint.position;
        GunClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
    }

    private void Reload() {
        switch (gunType) {
            case 0:
                reloadTime = akReloadTime;
                break;
            case 1:
                reloadTime = smgReloadTime;
                break;
            case 2:
                reloadTime = sniperReloadTime;
                break;
            default:
                break;
        }
        StartCoroutine(Reloading());
    }

    IEnumerator Reloading() {
        while (reloadTime > 0) {
            if (Input.GetAxisRaw("Mouse ScrollWheel") != 0) {
                reloadTime = 0;
                break;
            }
            reloadTime -= Time.deltaTime;
            if (reloadTime <= 0) {
                switch (gunType) {
                    case 0:
                        akBullet = akMag;
                        reloadTime = 0;
                        break;
                    case 1:
                        smgBullet = smgMag;
                        reloadTime = 0;
                        break;
                    case 2:
                        sniperBullet = sniperMag;
                        reloadTime = 0;
                        break;
                    default:
                        break;
                }
            }
            yield return null;
        }

        yield return null;
    }

    private int Ammo() {
        switch (gunType) {
            case 0:
                return akBullet;
            case 1:
                return smgBullet;
            case 2:
                return sniperBullet;
            default:
                return 0;
        }
    }

    public void ResetBullet() {
        reloadTime = 0;
        akBullet = akMag;
        smgBullet = smgMag;
        sniperBullet = sniperMag;
    }

    public float AmmoPercentage() {
        switch (gunType) {
            case 0:
                return akBullet / (akMag * 1f);

            case 1:
                return smgBullet / (smgMag * 1f);

            case 2:
                return sniperBullet / (sniperMag * 1f);

            default:
                return 0;
        }
    }

    public float ReloadPercentage() {
        switch (gunType) {
            case 0:
                return 1f - (reloadTime / (akReloadTime * 1f));

            case 1:
                return 1f - (reloadTime / (smgReloadTime * 1f));

            case 2:
                return 1f - (reloadTime / (sniperReloadTime * 1f));

            default:
                return 0;
        }
    }

    public void ChangeGun() {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0) {
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0) {
                gunType--;
            } else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0) {
                gunType++;
            }
            if (gunType < 0) {
                gunType = 2;
            } else if (gunType > 2) {
                gunType = 0;
            }
            switch (gunType) {
                case 0:
                    akImage.enabled = true;
                    smgImage.enabled = false;
                    sniperImage.enabled = false;
                    break;

                case 1:
                    akImage.enabled = false;
                    smgImage.enabled = true;
                    sniperImage.enabled = false;
                    break;

                case 2:
                    akImage.enabled = false;
                    smgImage.enabled = false;
                    sniperImage.enabled = true;
                    break;

                default:
                    akImage.enabled = false;
                    smgImage.enabled = false;
                    sniperImage.enabled = false;
                    break;
            }
        }
    }
}