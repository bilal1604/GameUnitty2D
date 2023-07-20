using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Patroling Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform[] waypoint;
    [SerializeField] private float waitTime;

    float startWaitTime = 2;
    private int index = 0;
    private Animation anim;

    public float damage;
    public float health;

    void Start()
    {
        anim = GetComponent<Animation>();
        transform.position = waypoint[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoint[index].position, moveSpeed * Time.deltaTime);
        if (transform.position == waypoint[index].position)
        {
            transform.rotation = waypoint[index].rotation;

            if (waitTime <= 0)
            {
                if (index + 1 < waypoint.Length)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
                waitTime = startWaitTime;
            }
            else
                waitTime -= Time.deltaTime;
        }
    }

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Gerak>().TakeDamage(damage);
            Invoke("TakeDamage", 1);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <=0)
        {
            Invoke("death", 0.2f);
        }
    }

    void death()
    {
        Destroy(gameObject);

    }
}
