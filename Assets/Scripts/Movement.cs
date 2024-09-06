using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

   
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 200f;
    [SerializeField] AudioClip mainEngine;
    
    [SerializeField] ParticleSystem thrusterParticle;
    [SerializeField] ParticleSystem leftSideThrusterParticle;
    [SerializeField] ParticleSystem rightSideThrusterParticle;

    Rigidbody rb;
    AudioSource audioSource;

    float jHorizontal, jVertical;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        jHorizontal = Input.GetAxis("JoystickHorizontal");
        jVertical = Input.GetAxis("JoystickVertical");
        
        ProcessThrust();
        ProcessRotation();
        JoystickInput();
    }

    
    void JoystickInput()
    {
    
        Debug.Log(jHorizontal);
        Debug.Log(jVertical);
        
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A) || jVertical < 0)
        {
            RotateLeft(jVertical);

        }
        else if(Input.GetKey(KeyCode.D) || jVertical > 0)
        {
            RotateRight(jVertical);
        }
        else
        {
            StopParticles();
        }
    }


    void ProcessThrust()
    {
       if(Input.GetKey(KeyCode.W) || jHorizontal > 0)
        {
            StartThrust(jHorizontal);
        }
        else if(Input.GetKeyUp(KeyCode.W))
        {
            StopThrust();
        }

    }



    void StartThrust(float joyInput)
    {
        if(joyInput > 0)
        {
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust * joyInput);
        }
        else{
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
        }
        // audioSource.Play();

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!thrusterParticle.isPlaying)
        {
            thrusterParticle.Play();
        }
    }

    void StopThrust()
    {
       
        audioSource.Stop();
        thrusterParticle.Stop();
    }

     private void StopParticles()
    {
        leftSideThrusterParticle.Stop();
        rightSideThrusterParticle.Stop();
    }

    void RotateLeft(float joyInput)
    {
        ApplyRotation(-rotationThrust, -joyInput);

        if (!rightSideThrusterParticle.isPlaying)
        {
            rightSideThrusterParticle.Play();
        }
    }

     private void RotateRight(float joyInput)
    {
        ApplyRotation(rotationThrust, joyInput);

        if (!leftSideThrusterParticle.isPlaying)
        {
            leftSideThrusterParticle.Play();
        }
    }

    void ApplyRotation(float rotationThisFrame, float joyInput)
    {
        rb.freezeRotation = true; // Freezing rotation so we can manually rotate
        
        if (jVertical > 0 || jVertical < 0)
        {
        transform.Rotate(-Vector3.forward * Time.deltaTime * rotationThisFrame * joyInput);
        }
        else 
        {
        transform.Rotate(-Vector3.forward * Time.deltaTime * rotationThisFrame);
        }
        rb.freezeRotation = false; // Unfreezing rotation so physics can take over
    }
}
