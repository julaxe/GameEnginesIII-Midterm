using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Slot : MonoBehaviour
{
 
    [SerializeField]
    private TMPro.TextMeshProUGUI itemCountText;

    [SerializeProperty("Item")]
    private Item itemInSlot = null;

    public Bag bag;
   
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
    private bool tail;
    public bool Tail
    {
        get
        {
            return tail;
        }
        set
        {
            if (tail == value)
            {
                return;
            }
            tail = value;
            //refresh text
            if(tail)
            {
                RefreshNumberOfItems();
            }
        }
    }

    private GameObject cllider;

    //possition in the grid
    public int row;
    public int column;


    private void Start()
    {
        itemCountText = transform.Find("ItemCount").GetComponent<TMPro.TextMeshProUGUI>();
        //this is just for debuggin purposes.
        cllider = transform.Find("CollisionBox").gameObject;
        RefreshItem();

    }
    public void RefreshNumberOfItems()
    {
        if(tail)
        {
            if (itemInSlot != null) // If an item is present
            {
                //update text
                itemCountText.text = Item.ItemCount.ToString();
            }
        }
        else
        {
            itemCountText.text = "";
        }
    }

    public void RefreshItem()
    {
        RefreshNumberOfItems();
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
