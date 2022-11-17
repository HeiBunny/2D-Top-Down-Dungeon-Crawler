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

    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();


    private void Start(){
            animator = GetComponent<Animator>();
            rb = this.GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
        
        }
    void Update(){
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        movement = direction;
        
    }
    private void FixedUpdate(){
        Vector2 d2 = player.transform.position - transform.position;
        if(Mathf.Sqrt(d2.x * d2.x + d2.y * d2.y) > 0.2 && Mathf.Sqrt(d2.x * d2.x + d2.y * d2.y) < 1.2){
            if(d2 != Vector2.zero){
                        bool success = TryMove(d2);

                        if(!success && d2.x != 0){
                            success = TryMove(new Vector2(d2.x, 0));

                            if(!success){
                                success = TryMove(new Vector2(0, d2.y));
                            }
                        }

                    }
        }
        if(d2.x < 0){
            sr.flipX = true;
        }
        if(d2.x > 0){
            sr.flipX = false; 
        }
        
    }
    private bool TryMove(Vector2 direction){
        if(direction != Vector2.zero){
               
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if(count == 0){
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed *Time.deltaTime)); 
            return true;
        }else{
            return false;   
        }}else{
            return false;
        }
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
