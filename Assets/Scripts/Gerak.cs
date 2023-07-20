using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Gerak : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] int powerJump;

    private Rigidbody2D myRigidbody2D;
    private Animator anim;

    [Header("CheckGroud")]
    public float radius;
    public Transform groundChecker;
    public LayerMask whatIsGround;

    [Header("CheckEnemy")]
    public float attackRange;
    public Transform attackPoint;
    public LayerMask whatIsEnemy;


    public float timeBeetWeenAttacks;

    float nextAttacksTime;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] private Image healthBar;

    public float health = 100f;
    public float damage = 100f;
    [SerializeField] GameObject loseCanvas;


    bool hadapkanan;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthBar.fillAmount = health / maxHealth;
    }
    // Update is called once per frame
    void Update()
    {

        healthBar.fillAmount = health / maxHealth;
        if(health <= 0)
        {
            loseCanvas.SetActive(true);
        }
        float move = Input.GetAxis("Horizontal");
        myRigidbody2D.velocity = new Vector2(move * moveSpeed, myRigidbody2D.velocity.y);

        if (move > 0 && hadapkanan == false)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            hadapkanan = true;
            Debug.Log("Kanan");
        }
        else if (move < 0 && hadapkanan)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            hadapkanan = false;
            Debug.Log("Kiri");

        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            myRigidbody2D.velocity = Vector2.up * powerJump;
        }

        if (!IsGrounded())
        {
            anim.SetTrigger("jump");
        }

        if (move != 0)
        {
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);

        }
        if (move != 0)
        {
            anim.SetBool("idle", true);
        }
        else
        {
            anim.SetBool("idle", false);

        }

        bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundChecker.position, radius, whatIsGround);
        }



        if(Time.time >= nextAttacksTime) //INI BUAT ATTACK ENEMY
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("attack");
                nextAttacksTime = Time.time + timeBeetWeenAttacks;
            }
        }

    }

    //DI LUAR UPDATE

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundChecker.position, radius);
    }


    void Attack()

    {
        Collider2D[] Enemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, whatIsEnemy);
        foreach(Collider2D col in Enemy)
        {
            //col.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if(health <= 0)
        {
            Invoke("death", 0.2f);
        }
    }

    void death()
    {
        Destroy(gameObject);

    }
}
