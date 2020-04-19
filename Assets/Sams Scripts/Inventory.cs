using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool inventoryon = false;
    public GameObject inventory;
    public GameObject Wall;

    private int allslots;
    private int enabledslots;
    public GameObject[] slot;

    int i = 0;
    int count = 0;

    Animator anim;
    Animator anim2;
    Animator anim3;
    Animator anim4;

    public Canvas message;
    public Canvas message2;
    public Canvas message3;
    public Canvas message4;

    public void Awake()
    {
        anim = message.GetComponent<Animator>();
        anim2 = message2.GetComponent<Animator>();
        anim3 = message3.GetComponent<Animator>();
        anim4 = message4.GetComponent<Animator>();
    }
    public void Start()
    {
        allslots = 4;
        slot = new GameObject[allslots];
        inventory = GameObject.Find("Inventory");

        while(i < allslots){
                slot[i] = inventory.transform.GetChild(i).gameObject;
                if (slot[i].GetComponent<Slot>().item == null)
                {
                    slot[i].GetComponent<Slot>().empty = true;
                }
                i++;
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryon = !inventoryon;
        }

        if(inventoryon == true)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
        
        if (Input.GetKey(KeyCode.E))
        {
            anim.SetTrigger("escPressed");
            anim2.SetTrigger("escPressed");
            anim3.SetTrigger("escPressed");
            anim4.SetTrigger("escPressed");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            print("Here");
            GameObject itempickedup = other.gameObject;
            Item item = itempickedup.GetComponent<Item>();

            AddItem(itempickedup,item.ID,item.type, item.description,item.icon);
        }
    }

    void AddItem(GameObject itemobject,int itemID, string ItemType, string ItemDescription, Sprite itemIcon)
    {
        count++;
        if(count == 1)
        {
            anim.SetTrigger("buttonPressed");
             if (Input.GetKey(KeyCode.E))
        {
            anim.SetTrigger("escPressed");
        }
        }
        else if(count == 2)
        {
            anim2.SetTrigger("buttonPressed");
        }
        else if(count == 3)
        {
            anim3.SetTrigger("buttonPressed");
        }
        else if(count == 4)
        {
            anim4.SetTrigger("buttonPressed");
            Wall.SetActive(false);
        }
        //print("Here");
        for (int i = 0; i < allslots; i++)
        {
            if (slot[i].GetComponent<Slot>().empty)
            {

                itemobject.GetComponent<Item>().pickedup= true;

                slot[i].GetComponent<Slot>().item = itemobject;
                slot[i].GetComponent<Slot>().icon = itemIcon;
                slot[i].GetComponent<Slot>().type = ItemType;
                slot[i].GetComponent<Slot>().ID = itemID;
                slot[i].GetComponent<Slot>().description = ItemDescription;

                itemobject.transform.parent = slot[i].transform;


                slot[i].GetComponent<Slot>().updateslot();
                slot[i].GetComponent<Slot>().empty = false;
                itemobject.SetActive(false);
            }
            else
            {

                continue;
            }

            return;
        }

    }

    public bool checkinven()
    {
        if(inventory.transform.GetChild(1).gameObject != null && inventory.transform.GetChild(2).gameObject != null && inventory.transform.GetChild(3).gameObject != null && inventory.transform.GetChild(4).gameObject != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
