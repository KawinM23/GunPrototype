using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootController : MonoBehaviour
{
    PlayerMovement pm;
    HackController hc;

    public bool shootable;

    public float fireRate = 0.2f;
    public Transform firingPoint;
    public GameObject bulletPrefab;
    public GameObject gunPrefab;
    float timeUntilFire;

    Vector2 lookDirection;
    float lookAngle;
    public float offset;

    private void Start() {
        pm = gameObject.GetComponent<PlayerMovement>();
        hc = GetComponent<HackController>();
        if (!SceneManager.GetActiveScene().name.Equals("Menu")) {
            shootable = true;
        } else {
            shootable = false;
        }
        
    }

    private void Update() {
        if (!TimeManager.isPause) {
            if (shootable && !hc.isHacking) {
                if (Input.GetMouseButton(0) && timeUntilFire < Time.time) {
                    Shoot();
                    timeUntilFire = Time.time + fireRate;
                }
                if (Input.GetMouseButtonDown(1)) {
                    //ShootGun();
                    timeUntilFire = Time.time + fireRate;
                }
            }
        }
    }

    void Shoot() {
        Debug.Log("Shoot");
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        firingPoint.rotation = Quaternion.Euler(0, 0, lookAngle);

        GameObject bulletClone = Instantiate(bulletPrefab);
        bulletClone.transform.position = firingPoint.position;
        bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

        //float angle = pm.isFacingRight ? 0f : 180f;
        //Instantiate(bulletPrefab, firingPoint.position, Quaternion.Euler(new Vector3(0f, 0f, angle)));
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


}
