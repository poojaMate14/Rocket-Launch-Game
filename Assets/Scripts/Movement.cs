using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngineSound;
    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem leftSideThrustParticles;
    [SerializeField] ParticleSystem rightSideThrustParticles;

    Rigidbody rigidbodyObj;
    AudioSource audioSourceObj;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyObj = GetComponent<Rigidbody>();
        audioSourceObj = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else 
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            StartLeftRotation();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartRightRotation();
        }
        else {
            StopRotation();
        }
    }

    void StartThrusting()
    {
        rigidbodyObj.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSourceObj.isPlaying)
        {
            audioSourceObj.PlayOneShot(mainEngineSound);
        }
        if (!thrustParticles.isPlaying)
        {
            thrustParticles.Play();
        }
    }

    void StopThrusting()
    {
        audioSourceObj.Stop();
        thrustParticles.Stop();
    }

    void StartLeftRotation()
    {
        ApplyRotation(rotationThrust);
        if (!rightSideThrustParticles.isPlaying)
        {
            rightSideThrustParticles.Play();
        }
    }

    void StartRightRotation()
    {
        ApplyRotation(-rotationThrust);
        if (!leftSideThrustParticles.isPlaying)
        {
            leftSideThrustParticles.Play();
        }
    }

    void StopRotation()
    {
        leftSideThrustParticles.Stop();
        rightSideThrustParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rigidbodyObj.freezeRotation = true; // freezing existing rotation so that we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rigidbodyObj.freezeRotation = false; // unfreezing rotation so that the physics can take over
    }
}
