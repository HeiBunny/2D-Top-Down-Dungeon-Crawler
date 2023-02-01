using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public float speed;
    public Vector3 direction;
    public float damage; 
    SpriteRenderer sr; 



    public Arrow(Vector3 direction, float speed){
        this.speed = speed;
        this.direction = direction;
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if(direction.y > 0){
            sr.flipY = true;
        }
        if(direction.x < 0){
            sr.flipX = true;
        }

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move();
    }

    public void Move(){
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Hitbox"){
             PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                player.Health -= damage;
            }
        }
    }
}
