using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    public float health = 10;
    public float maxHealth = 10;
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public swordAttack swordAttack;
    public bool isDead = false;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    bool canMove = true;
    bool unlockedTwo;
    public int numWeapon;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        numWeapon = 1;
        unlockedTwo = false;
    }


    private void FixedUpdate() {
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
        animator.SetTrigger("SwordAttack");
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

            if(spriteRenderer.flipX == true){
                swordAttack.AttackLeft();
            }else{
                swordAttack.AttackRight();
            }
        }
        if(numWeapon == 2){

            if(spriteRenderer.flipX == true){
                swordAttack.AttackLeft();
            }else{
                swordAttack.AttackRight();
            }
        }
    }

    public void EndSwordAttack(){
        UnlockMovement();
        swordAttack.StopAttack();
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
            health = value;
            if (health <= 0)
            {
                Dead();
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


}
