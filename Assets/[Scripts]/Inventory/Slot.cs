using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Node
{
    public int row;
    public int column;
    public bool inUse;
    public bool root;
}

public class Slot : MonoBehaviour
{

    [SerializeProperty("Item")]
    private Item itemInSlot = null;

    private Bag bag;
    public Bag Bag
    {
        get { return bag; }
        set { bag = value; }
    }
   
    public Item Item
    {
        get
        {
            return itemInSlot;
        }
        set
        {
            if(itemInSlot == value)
            {
                return;
            }
            itemInSlot = value;
            Debug.Log("item Changed");
            RefreshItem();

        }
    }
    private bool root;
    public bool Root
    {
        get
        {
            return root;
        }
        set
        {
            if (root == value)
            {
                return;
            }
            root = value;
        }
    }

    private GameObject cllider;

    //possition in the grid
    public int row;
    public int column;


    private void Start()
    {      
        RefreshItem();
    }

    public void RefreshItem()
    {
        cllider = transform.Find("CollisionBox").gameObject;
        if (!itemInSlot)
       {
            cllider.GetComponent<Image>().color = new Color(0.8584906f, 0.8584906f, 0.8584906f, 0.8117647f);
        }
        else
        {
            cllider.GetComponent<Image>().color = new Color(0.8396226f, 0.0f, 0.0f, 0.8117647f);
        }
    }
}
