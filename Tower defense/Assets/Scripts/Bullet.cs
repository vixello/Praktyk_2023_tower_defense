using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    public int damage;
    public Color bulletColor = Color.green;
    public Enemy enemy;

    [Header ("Hit Effects")]
    public GameObject bulletHitEffect;
    public GameObject bulletHitEffect2;
    private float bulletSpeed = 4;
    private Vector2 target;
    // Start is called before the first frame update
    void Start()
    {
        target = enemy.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
    }

    public void SetDamage(int damageValue)
    {
        damage = damageValue;
    }

    public void SetBulletColor(Color color)
    {
        bulletColor = color;

        transform.GetChild(0).GetComponent<SpriteRenderer>().material.color = color;
    }

    void DealDamage(int damageVakue)
    {
        if(enemy != null)
        {
            enemy.GetComponent<Enemy>();
            enemy.health -= damageVakue;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            DealDamage(damage);

            GameObject hitEffect = (bulletColor == Color.blue) ? bulletHitEffect : bulletHitEffect2;
            doAnEffect(hitEffect);

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("turretWall1") || collision.gameObject.CompareTag("turretWall2"))
        {
            Color bulletColor = transform.GetChild(0).GetComponent<SpriteRenderer>().material.color;
            string wallTag = collision.gameObject.tag;

            // regarding of color and wall type there is a different effect
            if (bulletColor == Color.blue && (wallTag == "turretWall2" || wallTag == "Wall")){
                
                doAnEffect(bulletHitEffect);
            }
            else if (bulletColor == Color.green && (wallTag == "turretWall1" || wallTag == "Wall")){
                
                doAnEffect(bulletHitEffect2);
            }

            Destroy(gameObject);
        }

    }

    private void doAnEffect(GameObject bulletHitEffect)
    {
        GameObject effect = Instantiate(bulletHitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.45f);
    }
}
