using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float distanceToObject;
    RaycastHit objectHit;

    Animator anim;
    public Canvas message;

    public void Awake()
    {
        anim = message.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * distanceToObject, Color.magenta);

        if(Physics.Raycast(this.transform.position,this.transform.forward, out objectHit,distanceToObject) && Input.GetMouseButtonDown(0))
        {
            Debug.Log("I touched" + objectHit.collider.gameObject.name);
            anim.SetTrigger("buttonPressed");

            
            
        }

        if (Input.GetKey(KeyCode.E))
        {
            anim.SetTrigger("escPressed");
        }
    }
}
