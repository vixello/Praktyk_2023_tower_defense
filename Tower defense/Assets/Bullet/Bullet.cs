using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    public int BulletDamage = 0;
    public Color BulletColor = Color.green;
    public Enemy BulletTargetEnemy;
    private float bulletSpeed = 10f;

    [Header ("Hit Effects")]
    public GameObject BulletHitEffect;
    public GameObject BulletHitEffect2;
    //private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        //target = BulletTargetEnemy.transform.position;
        GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);

    }

    // Update is called once per frame
 /*   void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
    }
*/
    public void SetDamage(int damageValue)
    {
        BulletDamage = damageValue;
    }

    public void SetBulletColor(Color color)
    {
        BulletColor = color;

        transform.GetChild(0).GetComponent<SpriteRenderer>().material.color = color;
    }

    void DealDamage(int dealtDamageValue)
    {
        if(BulletTargetEnemy != null)
        {
            BulletTargetEnemy.GetComponent<Enemy>();
            BulletTargetEnemy.EnemyHealth -= dealtDamageValue;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            DealDamage(BulletDamage);

            GameObject hitEffect = (BulletColor == Color.blue) ? BulletHitEffect : BulletHitEffect2;
            DoAnEffect(hitEffect);

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("turretWall1") || collision.gameObject.CompareTag("turretWall2"))
        {
            Color bulletColor = transform.GetChild(0).GetComponent<SpriteRenderer>().material.color;
            string wallTag = collision.gameObject.tag;

            // regarding of color and wall type there is a different effect
            if (bulletColor == Color.blue && (wallTag == "turretWall2" || wallTag == "Wall")){
                
                DoAnEffect(BulletHitEffect);
            }
            else if (bulletColor == Color.green && (wallTag == "turretWall1" || wallTag == "Wall")){
                
                DoAnEffect(BulletHitEffect2);
            }

            Destroy(gameObject);
        }

    }

    private void DoAnEffect(GameObject gameEffect)
    {
        GameObject effect = Instantiate(gameEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.45f);
    }
}
