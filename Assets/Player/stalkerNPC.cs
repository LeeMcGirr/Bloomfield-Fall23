using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stalkerNPC : MonoBehaviour
{
    //split horizontal and forward velocities
    //so the stalker can spiral in towards player
    [Header("Base Vars")]
    public GameObject targetPlayer;
    public float horSpeed = 50f;
    public float forSpeed = 5f;
    //accel increases the % of force towards player over time, so 
    //we don't just get stuck in orbit
    public float forAccel = .1f;
    //max speed clamps to avoid launching sideways
    public float maxSpeed = 100f;
    Rigidbody myRB;

    //these control the kick when stalker collides with player
    [Header("Explosion Vars")]
    public float expForce;
    public float expRadius;


    // Start is called before the first frame update
    void Start()
    {
        //finding our player and RB for later use
        targetPlayer = GameObject.FindWithTag("Player");
        myRB = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //set the look dir at the player
        transform.LookAt(targetPlayer.transform);

        //all we need to do is edit the X/Z and make sure we
        //apply forces in world space
        //this keeps our strafing and forward velocities always
        //perfectly parallel or perpendicular to the targetPlayer
        Vector3 newVel = Vector3.zero;
        newVel.x = horSpeed;
        newVel.z = forSpeed;
        newVel = transform.TransformDirection(newVel);
        Debug.DrawRay(transform.position, newVel, Color.magenta);
        myRB.AddForce(newVel);
        myRB.velocity = Vector3.ClampMagnitude(myRB.velocity, maxSpeed);
        //Debug.Log("velocity of stalker: " + myRB.velocity.magnitude + " " + myRB.velocity);

        //gradual decay and accel to make sure stalker eventually
        //contacts player
        maxSpeed -= Time.fixedDeltaTime;
        forSpeed += forAccel;

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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //AddExplosionForce applies a force from a position, making it useful
            //for "kick" or "recoil" or grenade-like behaviour
            Debug.Log("player hit");
            Rigidbody target = collision.gameObject.GetComponent<Rigidbody>();
            target.AddExplosionForce(expForce, transform.position - (Vector3.up * 2f), expRadius);
        }
    }
}
