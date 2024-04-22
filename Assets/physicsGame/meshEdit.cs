using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshEdit : MonoBehaviour
{
    [Header("basic Vars")]
    public Renderer myRender;
    public Mesh myMesh;
    public Material myMat;
    Rigidbody myRB;

    [Header("Bools")]
    public bool colorOnHit = true;
    public bool speedStretch = true;

    [Header("Squash")]
    public float speed;
    public float gate = 0.01f;

    float previousMagn;
    Vector3[] referencePos;
    // Start is called before the first frame update
    void Start()
    {
        myMesh = GetComponent<MeshFilter>().mesh;
        myRB = GetComponent<Rigidbody>();
        myMat = myRender.material;
        myMat.color = Color.white;
    }
    public void Update()
    {

        speed = myRB.velocity.magnitude * .1f;

        Vector3 placeholder = Vector3.one;
        if(speedStretch)
        {
            float newZ = Mathf.Clamp(speed, .8f, 1.5f);
            if(transform.localScale.z >= .8f && transform.localScale.z <= 1.5f)
            {
                transform.localScale = new Vector3(1, 1, newZ);
            }

            float newY = Mathf.Clamp(speed, 0.9f, 1.5f);
            newY = 2 - newY;
            if(transform.localScale.y >= .5f && transform.localScale.y <= 1.1f)
            {
                transform.localScale = new Vector3(1, newY, transform.localScale.z);
            }

        }

    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "NPC" || collision.gameObject.tag == "Player")
        {
            //run code that changes the material color of the player
            if (colorOnHit) { StartCoroutine(takeHit(1f)); }
        }
    }
    IEnumerator takeHit(float time)
    {
        //code that executes when the co-routine condition is first met goes here
        myMat.color = Color.red;
        yield return new WaitForSeconds(time); //wait for X seconds
        //code that executes when the co-routine wait time up, goes here
        myMat.color = Color.white;

    }
}
