using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float TowerRotationSpeed = 2f;

    public int TowerDamageValue = 0;
    public int TowerColorIndex = 0;
    public Transform TowerWeaponFirePoint;
    public Rigidbody2D TowerWeaponBody;
    public Enemy TowerTargetEnemy = null;
    
    public GameObject Bullet;
    private Color bulletColor;

    private bool isShooting = false;
    private float timeTillNextBullet = 0f;
    private float timeBetweenBullets = 0f;

    private void Start()
    {
        switch (TowerColorIndex)
        {
            case 0:
                TowerDamageValue = 5;
                bulletColor = Color.blue;
                timeBetweenBullets = 0.2f;
                break;
            case 1:
                TowerDamageValue = 10;
                bulletColor = Color.green;
                timeBetweenBullets = 1.2f;
                break;
        }
    }
    // Update is called once per frame
/*    void Update()
    {
        if (isShooting)
        {
            if (timeTillNextBullet <= 0 && TowerTargetEnemy != null)
            {
                Shoot(TowerTargetEnemy);
                timeTillNextBullet = timeBetweenBullets;

            }
            else
            {
                timeTillNextBullet -= Time.fixedDeltaTime;
            }
        }

    }*/
    private void FixedUpdate()
    {
        if (TowerTargetEnemy != null)
        {
            //rotate the turret gun body
            Vector2 direction = TowerTargetEnemy.transform.position - TowerWeaponBody.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            TowerWeaponBody.rotation = angle;
        }
        if (isShooting)
        {
            if (timeTillNextBullet <= 0 && TowerTargetEnemy != null)
            {

                Shoot(TowerTargetEnemy);
                timeTillNextBullet = timeBetweenBullets;

            }
            else
            {
                timeTillNextBullet -= Time.fixedDeltaTime;
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision detected");

        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            TowerTargetEnemy = enemy;
            isShooting = true;
        }
    }



    private void Shoot(Enemy shootTarget)
    {
            GameObject bulletNew = Instantiate(Bullet, TowerWeaponFirePoint.position, TowerWeaponFirePoint.rotation);
            Bullet bulletScript = bulletNew.GetComponent<Bullet>();

            if (bulletScript != null)
            {
                bulletScript.SetDamage(TowerDamageValue);
                bulletScript.SetBulletColor(bulletColor);
                bulletScript.GetComponent<Bullet>().BulletTargetEnemy = shootTarget;
            }
            else
            {
                Debug.Log("BULLET is null");
            }
       
    }
}
