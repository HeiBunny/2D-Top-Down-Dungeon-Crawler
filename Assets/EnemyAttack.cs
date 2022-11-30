using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public SpriteRenderer sr;
    public PlayerController player;

    public Vector2 targetPos;
    public float speed = 1f;
    
    // Start is called before the first frame update
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        targetPos = GetComponent<PlayerController>().transform.position;
    }

    public void AttackEnemy(){
        sr.transform.position = Vector2.MoveTowards(sr.transform.position, targetPos, speed * Time.deltaTime);
    }
}