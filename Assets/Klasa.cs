using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Klasa : MonoBehaviour
{
    // Rigidbody rigidbody;
    private CharacterController controller;
    private Vector3 playerVelocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        // rigidbody = GetComponent<Rigidbody>();
    }

    // void FixedUpdate(){
    //     if(Input.GetButton("Jump")){
    //         rigidbody.AddForce(transform.up * 10);
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump")){
            playerVelocity.y += 10.0f;
        }

        if(Input.GetKey(KeyCode.W)){
            playerVelocity.z += 10.0f * Time.deltaTime;
        }

        
        if(Input.GetKey(KeyCode.S)){
            playerVelocity.z -= 10.0f * Time.deltaTime;
        }


        if(Input.GetKey(KeyCode.D)){
            playerVelocity.x += 10.0f * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.A)){
            playerVelocity.x -= 10.0f * Time.deltaTime;
        }

        playerVelocity.y += -9.81f * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
