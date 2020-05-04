using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenAdd : MonoBehaviour
{

    public GameObject inven;
    Inventory WholeInventory;

    public void Start()
    {
        inven = GameObject.Find("Inventory Manager");
        WholeInventory = inven.GetComponent<Inventory>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            GameObject itempickedup = other.gameObject;
            Item item = itempickedup.GetComponent<Item>();

            WholeInventory.AddItem(itempickedup, item.ID, item.type, item.description, item.icon);
        }
    }
}
