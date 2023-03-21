using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyController : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private Transform bulletPoolParent;
    [SerializeField] private float fireCooldown;

    private float tempCooldown;

    private void Start()
    {
        this.bulletPoolParent = GameObject.Find("BulletPool").GetComponent<Transform>();
    }

    private void Update()
    {
        if (tempCooldown <= 0)
        {
            Fire();
            this.tempCooldown = this.fireCooldown;
        }
        this.tempCooldown -= Time.deltaTime;
    }

    private void Fire()
    {
        BulletController bullet = bulletPool.Spawn(this.firePoint.position, this.bulletPoolParent);
        bullet.DestroyBullet();
    }

}
