using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleHelper : MonoBehaviour
{
    public ParticleSystem myParticles;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLIDED");
        myParticles.Play(true);

    }
}
