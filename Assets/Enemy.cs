using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    
    public float moveSpeed = .05f;
    public float maxHealth = 2;
    public float health = 2;
    public float damage = 2;
    Animator animator;
    public Vector2 target, movement;
    public Transform player; 
    Rigidbody2D rb;
    SpriteRenderer sr;
    public static int numKilled;
    public bool isJerry = false;


    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();


    private void Start(){
        animator = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        // player = GameObject.FindGameObjectWithTag("Player");
        if(isJerry){
            numKilled = 0;
        }
        
    
    }

      
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hitbox")
        {
            PlayerController player = FindObjectOfType<PlayerController>();

            if (player != null)
            {
                player.Health -= damage;
            }
        }
    }
    
    void Update(){
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        movement = direction;
        
    }
    private void FixedUpdate(){
        Vector2 d2 = player.transform.position - transform.position;
        float playerHeight = player.GetComponent<SpriteRenderer>().bounds.size.y;
        d2.y -= playerHeight/4;
        
        float spd = Mathf.Sqrt(d2.x * d2.x + d2.y * d2.y);
        if(Mathf.Sqrt(d2.x * d2.x + d2.y * d2.y) > 0.1 && Mathf.Sqrt(d2.x * d2.x + d2.y * d2.y) < 1.2){
            if(d2 != Vector2.zero){
                        bool success = TryMove(d2);

                        if(!success && d2.x != 0){
                            success = TryMove(new Vector2(spd, 0));
                            //(d2.x / Mathf.Abs(d2.x))

                            if(!success){
                                success = TryMove(new Vector2(0, spd));
                                //(d2.y / Mathf.Abs(d2.y))
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
        if(numKilled >= 10){
            Defeated();
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

    public float getHealth(){
        float a = health/maxHealth;
        if(a >= 0){
            return(a);
        }else{
            return 0;
        }
        
    }
 
    public void Defeated(){
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy(){
        numKilled ++;
        print(numKilled);
        Destroy(gameObject);
    }
      
    public int getNK(){
        return numKilled;
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.tag == "Player")
    //     {
    //         PlayerController player = other.GetComponent<PlayerController>();

    //         if (player != null)
    //         {
    //             player.Health -= damage;
    //         }
    //     }
    // }
}

