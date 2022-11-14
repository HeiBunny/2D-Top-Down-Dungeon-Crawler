using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    public float moveSpeed = .05f;
    public float health = 1;
    Animator animator;
    public Vector2 target;
    public GameObject player; 

    private void Start(){
            animator = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player");
        
        }
    void Update(){
        float step = moveSpeed * Time.deltaTime;
        target = new Vector2(player.transform.position.x, player.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, step);
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
