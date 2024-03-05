using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physicsGameManager : MonoBehaviour
{
    [Header("Timer Vars")]
    public float timer;
    public float enemyTimer;
    public float spawnInterval = 2f;

    [Header("Player Vars")]
    public GameObject myPlayer;

    [Header("Enemy Vars")]
    public Vector2 spawnBounds;
    public NPCwave[] spawnWaves;
    public int waveCount;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        waveCount = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        enemyTimer += Time.fixedDeltaTime;

        //checking against an interval to spawn
        if(enemyTimer > spawnInterval)
        {
            enemyTimer = 0f; //reset the time
            GameObject[] newWave = spawnWaves[waveCount].myNPCs;
            for(int i = 0; i < newWave.Length; i++)
            {
                //Random.insideUnitSphere returns a value of Vector3(x,y,z) where the magnitude (total length) is <= 1
                Vector3 targetPos = Random.insideUnitSphere;
                //since we are centered at (0,0,0) we can multiply and then the enemies will only
                //spawn in a ring
                float mult = Random.Range(spawnBounds.x, spawnBounds.y);
                targetPos *= mult;
                targetPos.y = 1f;
                Instantiate(newWave[i], targetPos, Quaternion.identity);
            }
   
            waveCount++;
         
        }

    }
}
