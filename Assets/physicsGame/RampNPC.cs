using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RampNPC : MonoBehaviour
{
    //set our target player and speed
    [Header("base vars")]
    public GameObject targetPlayer;
    public float speed = 100f;
    Rigidbody myRB;

    [Header("Plow Vars")]
    //proxAttack tells our Plow when to launch at the player
    float proxAttack = 10f;
    public float launchSpeed = 2000f;
    public bool nearPlayer;
    // Start is called before the first frame update
    void Start()
    {
        //find target player on start since this is a single player game (for now)
        targetPlayer = GameObject.FindWithTag("Player");
        //getComponent (searches gameObject script is attached to for a component of TYPE)
        myRB = GetComponent<Rigidbody>();
        nearPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //use Vector3.Distance to find the magnitude between two objects (float)
        float distToPlayer = Vector3.Distance(targetPlayer.transform.position, transform.position);
        
        //if the enemy is still far away, and has not yet launched - move towards player
        if (distToPlayer > proxAttack && !nearPlayer)
        {
            //find the dir to player and add force along that vector
            myRB.AddForce(VectorToPlayer() * speed);
            //using built in LookAt() to rotate the enemy to face
            transform.LookAt(targetPlayer.transform);
        }

        //if the enemy is close enough, launch it
        else
        {   //use this bool to make sure we only launch once!
            if(nearPlayer == false)
            {
                Launch();
                nearPlayer = true;
            }
        }
    }

    //method to calculate direction to the player object
    Vector3 VectorToPlayer()
    {
        Vector3 targetDir;
        //with any 2 vectors in space, you can subtract one from the other to get the direction between the two
        targetDir = targetPlayer.transform.position - transform.position;
        //normalizing the vector sets its magnitude == 1. this lets us clean it up
        //and add a consistent speed magnifier
        targetDir = targetDir.normalized;
        Debug.DrawRay(transform.position, targetDir * 3f, Color.red);
        return targetDir;
    }

    //this function can be used to launch an NPC quickly at the player
    //used for the plow or other 'missile' like enemies
    void Launch()
    {
        myRB.AddForce(VectorToPlayer() * launchSpeed);
    }
}
