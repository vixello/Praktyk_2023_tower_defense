using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int EnemyHealth = 0;
    public GameObject EnemyDestroyEffect;
    public HealthBar EnemyHealthBar;

    private int enemyMaxHealth = 100;
    private float enemySpeed = 0f;

    private int enemyWaypointIndex = 0;
    private Waypoints enemyWaypoints;

    // Start is called before the first frame update
    void Start()
    {
        EnemyHealth = enemyMaxHealth;
        EnemyHealthBar = GetComponentInChildren<HealthBar>();
        Application.targetFrameRate = 60;
        enemySpeed = Random.Range(2f, 5f);
        enemyWaypoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
    }

    void FixedUpdate()
    {
        EnemyHealthBar.UpdateHealth(EnemyHealth, enemyMaxHealth);
        if (EnemyHealth <= 0)
        {
            EnemyDeath();
        }
        transform.position = Vector2.MoveTowards(transform.position, enemyWaypoints.waypoints[enemyWaypointIndex].position, enemySpeed * Time.deltaTime);

        // rotate to correct angle
        Vector3 enemyDirection = enemyWaypoints.waypoints[enemyWaypointIndex].position - transform.position;
        float angle = Mathf.Atan2(enemyDirection.x, enemyDirection.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);

        // move to waypoint
        if (Vector2.Distance(transform.position, enemyWaypoints.waypoints[enemyWaypointIndex].position) < 0.1f){
            if(enemyWaypointIndex < enemyWaypoints.waypoints.Length - 1){
                
                enemyWaypointIndex++;
            }
            else{
                Destroy(gameObject);
            }
        }
    }

    // the BulletTargetEnemy dies
    private void EnemyDeath()
    {
        GameObject effect = Instantiate(EnemyDestroyEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.45f);
        Destroy(gameObject);
    }

}
