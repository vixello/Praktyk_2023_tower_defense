using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public GameObject destroyEffect;

    HealthBar healthBar;
    private int maxHealth = 100;

    private int enemyWaypointIndex;
    private float enemySpeed;
    private Waypoints enemyWaypoints;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar = GetComponentInChildren<HealthBar>();
        Application.targetFrameRate = 60;
        enemySpeed = Random.Range(2, 6);
        enemyWaypoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.UpdateHealth(health, maxHealth);
        if (health <= 0)
        {
            Death();
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

    // the enemy dies
    private void Death()
    {
        GameObject effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.45f);
        Destroy(gameObject);
    }

}
