using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleHelper : MonoBehaviour
{
    public GameObject myParticles;
    // Start is called before the first frame update

    private void Start()
    {
        myParticles.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("COLLIDED");
            myParticles.SetActive(true);
            StartCoroutine(turnOff(1f));
        }

    }

    IEnumerator turnOff(float time)
    {
        //code execute here
        yield return new WaitForSeconds(time); //wait for X seconds
        //more code execute here
        myParticles.SetActive(false);
    }
}
