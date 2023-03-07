using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Variables and declarations
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem engineParticles;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            //check to see if playing audio
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            } 
            //check to see if playing particles
            if(!engineParticles.isPlaying)
            {
                engineParticles.Play();
            }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        engineParticles.Stop();
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
