using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    float baseFootstepVolume;
    public AudioSource footsteps;

    private void Start()
    {
        footsteps.pitch = 0.5f;
        baseFootstepVolume=footsteps.volume;
    }
    void Update()
    {
        float workingSpeed = speed;
        float workingVolume = baseFootstepVolume;
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool crouching = Input.GetKey(KeyCode.LeftControl);
        bool running = Input.GetKey(KeyCode.LeftShift) && !crouching;
        if (crouching)
        {
            workingSpeed *= 0.66f;
            workingVolume *= 0.33f;
            footsteps.pitch = 0.33f;
        }
        if (running)
        {
            workingSpeed *= 1.66f;
            workingVolume *= 1.5f;
            footsteps.pitch = 1.5f;
        }
        if (!crouching && !running)
        {
            footsteps.pitch = 0.5f;
        }
        Vector3 move = transform.right * x + transform.forward * z;
        if (move != Vector3.zero&&!footsteps.isPlaying)
        {
            footsteps.volume = workingVolume;
            footsteps.Play();
        }
        else if(move == Vector3.zero && footsteps.isPlaying)
        {
            footsteps.Stop();
        }
        controller.Move(move * workingSpeed * Time.deltaTime);
    }
}
