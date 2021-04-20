using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody playerRB;
    [SerializeField] float thrustSpeed = 100f;
    [SerializeField] float rotationSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
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
            playerRB.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
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

    private void ApplyRotation(float rotationThisFrame)
    {
        playerRB.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);


    }
}
