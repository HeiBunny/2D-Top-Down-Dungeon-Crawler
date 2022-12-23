using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float moveSpeed = .05f;
    public float maxHealth = 2;
    public float health = 2;
    public float attackRange = 1.5f; // The range at which the slime can attack
    public float damage = 2;
    public float jumpHeight = 1f; // The force with which the slime jumps
    public float jumpDuration = 1f;
    public float returnDuration = 1;
    Animator animator;
    private Transform playerTransform; 
    Rigidbody2D rb;
    SpriteRenderer sr;
    private bool isJumping = false;

    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();


    private void Start(){
            animator = GetComponent<Animator>();
            rb = this.GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
            
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        }
    void Update(){
    }
    private void FixedUpdate(){
        if(playerTransform != null){
            Vector2 d2 = playerTransform.transform.position - transform.position;

            float spd = d2.magnitude;
            if(spd > 0.2f && spd < 1.2f){
                bool success = TryMove(d2);
                if(!success && d2.x != 0){
                    success = TryMove(new Vector2(spd, 0));
                }
                else if(!success && d2.y != 0) {
                    success = TryMove(new Vector2(0, spd));
                }
            }
            //check range for attack
            else if(spd <= attackRange){
                if(playerTransform != null){
                    if(!isJumping){
                        StartCoroutine(JumpAttack());
                        isJumping = true;
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
    }


    IEnumerator JumpAttack()
    {
        Vector3 startPos = transform.position;

        //Jump towards
        yield return StartCoroutine(MoveOverTime(jumpDuration, playerTransform.position));

        //Return back
        yield return StartCoroutine(MoveOverTime(jumpDuration, startPos));

        isJumping = false;       
          
    }

    IEnumerator MoveOverTime(float duration, Vector3 destination){

        float elapsedTime = 0;
        while (elapsedTime < duration )
        {   
            // Calculate the direction to the starting position
            Vector3 direction = destination - transform.position;

            // Calculate the new position for the slime based on the distance and direction to the starting position
            Vector3 newPos = transform.position + direction * (elapsedTime / returnDuration); //if use elapsedTime instead of distance it doesn't work

            // Move the slime to the new position
            rb.MovePosition(newPos);

            elapsedTime += Time.deltaTime;
            yield return null;
           
        }
    }






    //Javadoc style comments below
    ///<summary>
    ///Tries to move towards a direction, returns false if there is an obstacle, or direction is a zero vector
    ///</summary>
    private bool TryMove(Vector2 direction){
        if (direction == Vector2.zero) return false;
               
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if(count != 0) return false; 
        
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed *Time.deltaTime)); 
        return true;
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
        Destroy(gameObject);
    }

    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.Health -= damage;
            }
        }
    }
    

}

