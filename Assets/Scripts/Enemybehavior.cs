using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybehavior : MonoBehaviour
{
    AttackBehavior attackBehavior;
    Animator animator;
    public float AttackDistance;
    public float speed;
    public int attackPower;
    public int totalHealth;
    [SerializeField]private int currentHealth;
    
    public float attackCooldown;
    private float nextAttackTime;

    [SerializeField] LayerMask IgnoreLayer;
    private Rigidbody2D rb;

    public GameObject currentTarget;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackBehavior = GetComponent<AttackBehavior>();
        animator = GetComponent<Animator>();
        currentHealth = totalHealth;
        nextAttackTime = 0;
        healthBar.setMaxHealth(totalHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(nextAttackTime < attackCooldown) nextAttackTime+=Time.deltaTime;
        detectObjects();
    }

    void detectObjects(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left,AttackDistance,IgnoreLayer);
    
        // If it hits something...
        if (hit.collider != null)
        {
            float distance = Mathf.Abs(hit.point.x - transform.position.x);
            //Debug.Log (hit.collider.gameObject.name);
            if(distance <= AttackDistance){
                currentTarget = hit.collider.gameObject;
                Attack();
                //Change to attack behavior
            }else{
                Move();
            }
        }else{
            Move();
        }
    }

    private void Attack()
    {
        rb.velocity = Vector2.zero;
        animator.SetBool("IsMoving",false);

        if(nextAttackTime < attackCooldown) return;
        nextAttackTime = 0;
        attackBehavior.attack();
    }

    private void Move(){
        animator.SetBool("IsMoving",true);
        rb.velocity = Vector2.left*speed;
    }   

    public void LoseHealth(int amount){
        currentHealth -= amount;
        StartCoroutine("blinkRedEffect");
        if(currentHealth <=  0){
            Destroy(transform.parent.gameObject);
        }
        healthBar.setHealth(currentHealth);
    }

    IEnumerator blinkRedEffect(){
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
