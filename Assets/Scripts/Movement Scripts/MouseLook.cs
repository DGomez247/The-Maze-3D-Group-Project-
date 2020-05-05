using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private PhotonView PV;
    public float mousesen = 100f;
    public Transform playerBody;

    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        PV = this.transform.parent.GetComponent<PhotonView>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (PV!=null&&PV.IsMine)
        {
            float mouseX = Input.GetAxis("Mouse X") * mousesen * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mousesen * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


            playerBody.Rotate(Vector3.up * mouseX);
        }
        else if(PV==null){
            float mouseX = Input.GetAxis("Mouse X") * mousesen * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mousesen * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
