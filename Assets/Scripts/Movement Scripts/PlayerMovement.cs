using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPunObservable
{
    private PhotonView PV;
    public CharacterController controller;
    public float speed = 20f;
    float baseFootstepVolume;
    public AudioSource footsteps;

    public InputStr input;

    public struct InputStr
    {
        public float x;
        public float z;
    }

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        footsteps.pitch = 0.5f;
        baseFootstepVolume=footsteps.volume;
    }
    void Update()
    {
        if (PV!=null&&PV.IsMine)
        {
            float workingSpeed = speed;
            float workingVolume = baseFootstepVolume;

            input.x = Input.GetAxis("Horizontal");
            input.z = Input.GetAxis("Vertical");
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
            Vector3 move = transform.right * input.x + transform.forward * input.z;
            if (move != Vector3.zero && !footsteps.isPlaying)
            {
                footsteps.volume = workingVolume;
                footsteps.Play();
            }
            else if (move == Vector3.zero && footsteps.isPlaying)
            {
                footsteps.Stop();
            }
            controller.Move(move * workingSpeed * Time.deltaTime);
        }
        else if(PV==null){
           float workingSpeed = speed;
            float workingVolume = baseFootstepVolume;

            input.x = Input.GetAxis("Horizontal");
            input.z = Input.GetAxis("Vertical");
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
            Vector3 move = transform.right * input.x + transform.forward * input.z;
            if (move != Vector3.zero && !footsteps.isPlaying)
            {
                footsteps.volume = workingVolume;
                footsteps.Play();
            }
            else if (move == Vector3.zero && footsteps.isPlaying)
            {
                footsteps.Stop();
            }
            controller.Move(move * workingSpeed * Time.deltaTime);
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(input.x);
            stream.SendNext(input.z);
        }
        else
        {
            input.x = (float)stream.ReceiveNext();
            input.z = (float)stream.ReceiveNext();
        }

    }

}
