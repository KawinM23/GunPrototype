using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootController : MonoBehaviour
{
    PlayerMovement pm;
    HackController hc;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject smgBulletPrefab;
    [SerializeField] private GameObject sniperBulletPrefab;
    [SerializeField] private GameObject gunPrefab;

    private int gunType; //0 AK, 1 SMG, 2 Sniper
    public bool shootable;

    public float fireRate = 0.2f;
    public Transform firingPoint;
    float timeUntilFire;

    Vector2 lookDirection;
    float lookAngle;
    public float offset;

    private void Start() {
        pm = gameObject.GetComponent<PlayerMovement>();
        hc = GetComponent<HackController>();
        if (LevelManager.InLevel()) {
            shootable = true;
            gunType = 0;
        } else {
            shootable = false;
        }
        
    }

    private void Update() {
        if (!TimeManager.isPause) {
            if (shootable && !hc.isHacking) {
                Shoot();
                if (Input.GetMouseButtonDown(1)) {
                    //ShootGun();
                    timeUntilFire = Time.time + fireRate;
                }
            }
            if (shootable) {
                ChangeGun();
            }
        }
    }

    void Shoot() {
        if (Input.GetMouseButton(0) && timeUntilFire < Time.time) {
            Debug.Log("Shoot");

            GameObject bulletClone;

            lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            firingPoint.rotation = Quaternion.Euler(0, 0, lookAngle);

            switch (gunType) {
                case 0:
                    bulletClone = Instantiate(bulletPrefab);
                    timeUntilFire = Time.time + fireRate;
                    break;
                case 1:
                    bulletClone = Instantiate(smgBulletPrefab);
                    timeUntilFire = Time.time + 0.1f;
                    break;
                case 2:
                    bulletClone = Instantiate(sniperBulletPrefab);
                    timeUntilFire = Time.time + 0.8f;
                    break;
                default:
                    bulletClone = Instantiate(bulletPrefab);
                    timeUntilFire = Time.time + fireRate;
                    break;
            }
            
            bulletClone.transform.position = firingPoint.position;
            bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);



        }
    }
    void ShootGun() {
        Debug.Log("ShootGun");
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        firingPoint.rotation = Quaternion.Euler(0, 0, lookAngle);

        GameObject GunClone = Instantiate(gunPrefab);
        GunClone.transform.position = firingPoint.position;
        GunClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
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
            }else if (gunType > 2) {
                gunType = 0;
            }
            Debug.Log("Weapon: "+gunType);
        }
    }

}
