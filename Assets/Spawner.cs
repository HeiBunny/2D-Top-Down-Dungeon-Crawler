using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Enemy[] Enemies;
    public float spawnTime, spawnDelay;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 0;
        spawnDelay = 1;
        //InvokeRepeating("spawn", spawnTime, spawnDelay);
        spawn();
    }

    // Update is called once per frame
    void spawn()
    {
        Instantiate(Enemies[0], new Vector2(0.5f, 0.4f), Quaternion.identity);
    }
}
