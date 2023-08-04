using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float towerRotationSpeed = 2f;

    public int damageValue;
    public int turretColor;
    public Transform firePoint;
    public Rigidbody2D weaponBody;
    public GameObject bullet;

    private Color bulletColor;
    private float timeTillNextBullet;
    private float timeBetweenBullets;
    public Enemy targetEnemy = null;
    private bool shooting = false;

    private void Start()
    {
        switch (turretColor)
        {
            case 0:
                damageValue = 5;
                bulletColor = Color.blue;
                timeBetweenBullets = 0.2f;
                break;
            case 1:
                damageValue = 10;
                bulletColor = Color.green;
                timeBetweenBullets = 1.2f;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (shooting)
        {
            if (timeTillNextBullet <= 0 && targetEnemy != null)
            {
                Shoot(targetEnemy);
                timeTillNextBullet = timeBetweenBullets;

            }
            else
            {
                timeTillNextBullet -= Time.fixedDeltaTime;
            }
        }

    }
    private void FixedUpdate()
    {

        if (targetEnemy != null ) {
            //rotate the turret gun body
            Vector2 direction = targetEnemy.transform.position - weaponBody.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            weaponBody.rotation = angle;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision detected");
/*        if (collision.gameObject.tag == "Vurnerablepart") {
            
            SetTarget(collision.gameObject);
        }*/
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            targetEnemy = enemy;
            shooting = true;
        }
    }

/*    public void SetTarget(Enemy target)
    {
        Shoot(target);
    }*/

    private void Shoot(Enemy target)
    {
            GameObject bulletNew = Instantiate(bullet, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bulletNew.GetComponent<Bullet>();

            if (bulletScript != null)
            {
                bulletScript.SetDamage(damageValue);
                bulletScript.SetBulletColor(bulletColor);
                bulletScript.GetComponent<Bullet>().enemy = target;
            }
            else
            {
                Debug.Log("BULLET is null");
            }
       
    }
}
