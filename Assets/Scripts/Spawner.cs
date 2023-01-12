using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Enemy[] Enemies;
    public float spawnTime, spawnDelay, count;
    public Enemy enemy;
    public bool canSpawn = true;
    
    void Start()
    {
        Enemy enemy = GetComponent<Enemy>();
        spawnTime = 5;
        spawnDelay = 5;
        InvokeRepeating("spawn", spawnTime, spawnDelay);
        spawn();
        count = 1;
        
        
    }

    void spawn()
    {
        if(canSpawn){
            if(count == 0){
                Instantiate(Enemies[0], new Vector2(-0.05f, -0.65f), Quaternion.identity);
                count ++;
            }
            else if(count == 1){
                Instantiate(Enemies[0], new Vector2(-1.15f, 0.67f), Quaternion.identity);
                count ++;
            }
            else if(count == 2){
                Instantiate(Enemies[0], new Vector2(0.8f, 0.955f), Quaternion.identity);
                count ++;
            }
            else if(count == 3){
                Instantiate(Enemies[0], new Vector2(1.23f, 0.2f), Quaternion.identity);
                count = 0;
            }
        }
        
    }
    void Update(){
        int nk = enemy.getNK();
        if(nk >= 10){
            canSpawn = false;
            // print("10 slimes killed");
        }
    }
}
