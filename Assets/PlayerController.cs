using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    //Rigidbody of Player (not Hitbox) is preventing upward movement by slimes, not sure why
    //Tried changing kinematic rigidbody, didn't change, tried changing layer matrix, didn't work

    //invicibility frames isnt working, maybe delay between communication between Enemy OnCollision and player.Heallth?


    public float health = 10;
    public float maxHealth = 10;
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public swordAttack swordAttack;
    public bool isDead = false;
    public Enemy enemy;
    public PortalController pc;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    bool canMove = true;
    bool unlockedTwo, isActivated;
    public int numWeapon, numCoins;
    public bool canTakeDamage = true;
    public float frames;
    public float invFrames = 1;


    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        numWeapon = 1;
        unlockedTwo = false;
        isActivated = false;
        numCoins = 0;
    }
    private void FixedUpdate() {
        frames++;
        print(frames);
        if(frames >= invFrames){
            canTakeDamage = true;
        }

        if(canMove && isDead == false){
            if(movementInput != Vector2.zero){
                bool success = TryMove(movementInput);

                if(!success && movementInput.x > 0){
                    success = TryMove(new Vector2(movementInput.x, 0));

                    if(!success){
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }

                animator.SetBool("isMoving", success);
            }else{
                animator.SetBool("isMoving", false);
            }

            //set direction of sprite to movement direction
            if(movementInput.x < 0){
                spriteRenderer.flipX = true;
            }else if(movementInput.x > 0){
                spriteRenderer.flipX = false;
            }
        }
        int b = enemy.getNK();
        if(b >= 1 && isActivated == false){
            pc.Activate();
            isActivated = true;
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
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }else{
                return false;   
            }
        }else{
            return false;
        }
    }

    // Update is called once per frame
    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire() {
        if(numWeapon == 1){
            animator.SetTrigger("SwordAttack");
        }
        if(numWeapon == 2){
            animator.SetTrigger("SwordAttack2");
        }
    }

    void OnToggleWeapon1(){
        numWeapon = 1;
        swordAttack.setDamage(3);
    }
    void OnToggleWeapon2(){
        if(unlockedTwo == true){
            numWeapon = 2;
            swordAttack.setDamage(1);
        }
        
    }

    public void SwordAttack(){
        if(numWeapon == 1){
            LockMovement();
        }
        if(spriteRenderer.flipX == true){
            swordAttack.AttackLeft();
        }else{
            swordAttack.AttackRight();
        }
        
        
    }

    public void EndSwordAttack(){
        swordAttack.StopAttack();
        UnlockMovement();
    }

    public void LockMovement(){
        canMove = false;
    }
    public void UnlockMovement(){
        canMove = true;
    }
    
    public void setU2(){
        unlockedTwo = true;
    }

    public float Health
    {
        set
        {
            if(canTakeDamage){
                health = value;
                canTakeDamage = false;
                frames = 0;
                if (health <= 0)
                {
                    Dead();
                }
            }
            
        }
        get
        {
            return health;
        }
    }

    public void Dead()
    {
        isDead = true;
        animator.SetBool("Dead", true);
    }

    public void RemovePlayer(){
        Destroy(gameObject);
    }

    public float getHealth()
    {
        float a = health / maxHealth;
        if (a >= 0){
            return (a);
        }else{
            return 0;
        }

    }

    public void CoinCollected(){
        numCoins++;
        print("Coin Count: " + numCoins);
    }


}
