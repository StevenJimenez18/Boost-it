using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] AudioClip mainEngine;
    [SerializeField] float thrustSpeed = 100f;
    [SerializeField] float rotationSpeed = 20f;
    [SerializeField] ParticleSystem rocketPartciles;
    Rigidbody playerRB;
    AudioSource playerAudio;
    public bool collisionDisabled = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        // ProcessDebugMode();
        // ProcessSkipLevel();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }
    }

    void ProcessRotation()
    {
        playerRB.freezeRotation = false;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {

            ApplyRotation(-rotationSpeed);
        }
    }

    // void ProcessDebugMode()
    // {
    //     if (Input.GetKeyDown(KeyCode.C))
    //     {
    //         collisionDisabled = !collisionDisabled;
    //     }
    // }

    // void ProcessSkipLevel()
    // {
    //     if (Input.GetKeyDown(KeyCode.L))
    //     {
    //         GetComponent<CollisionHandler>().NextLevel();
    //     }
    // }

    void StartThrust()
    {
        playerRB.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
        if (!playerAudio.isPlaying)
        {
            playerAudio.PlayOneShot(mainEngine);
        }
        if (!rocketPartciles.isPlaying)
        {
            rocketPartciles.Play();
        }
    }

    void StopThrust()
    {
        playerAudio.Stop();
        rocketPartciles.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        playerRB.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    }

}
