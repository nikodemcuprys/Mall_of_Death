using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectileGun : MonoBehaviour
{
    // variable 
    public GameObject bullet;

    public float shootForce, upwardForce;
    public float timeBetweenShooting, spreed, reloadTime, timeBetweenShoots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    int bulletLeft, bulletShot;

    bool shooting, readyToShoot, reloading;

    public Camera fpsCam;
    public Transform attackPoint;

    public bool allowInvoke = true;


    private void Awake() {
        bulletLeft = magazineSize;
        readyToShoot = true;
    }
    private void MyInput() {
        
    }
}
