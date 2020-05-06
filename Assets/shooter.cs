using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{
    public int bullets=0;
    public GameObject gun;
    public AudioSource gunsound;
    public Camera playerCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            if(bullets>0)
            Fire();
        }
    }
    void Fire(){
        Ray fireline=playerCam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(fireline,out hit)){
                if(hit.collider.gameObject.GetComponent<followtheplayer>()){
                    hit.collider.gameObject.GetComponent<followtheplayer>().hit();
                }
        }
        gunsound.Play();
        bullets--;
        if(bullets==0){
            gun.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("bullet")){
            bullets++;
            if(bullets>0)
                gun.SetActive(true);
            print("collision");
            Destroy(collision.gameObject);
        }
        
    }
}
