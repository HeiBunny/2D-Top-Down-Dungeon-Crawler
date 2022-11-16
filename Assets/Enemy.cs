using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    
    public float moveSpeed = .05f;
    public float health = 1;
    Animator animator;
    public Vector2 target, movement;
    public Transform player; 
    Rigidbody2D rb;
    SpriteRenderer sr;

    private void Start(){
            animator = GetComponent<Animator>();
            rb = this.GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
        
        }
    void Update(){
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        movement = direction;
        
    }
    private void FixedUpdate(){
        Vector3 d2 = player.transform.position - transform.position;
        if(Mathf.Sqrt(d2.x * d2.x + d2.y * d2.y) > 0.25 && Mathf.Sqrt(d2.x * d2.x + d2.y * d2.y) < 1){
            move(movement);
        }
        if(d2.x < 0){
            sr.flipX = true;
        }
        if(d2.x > 0){
            sr.flipX = false; 
        }
        
    }
    void move(Vector2 direction){
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed *Time.deltaTime));
    }


    public float Health{
        set{
            health = value;
            if(health <= 0){
                Defeated();
            }
        }
        get{
            return health;
        }
    }

   
 
    public void Defeated(){
       animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy(){
         Destroy(gameObject);
    }



}
