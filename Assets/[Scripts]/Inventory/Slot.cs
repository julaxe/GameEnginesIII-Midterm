using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Slot : MonoBehaviour
{
 
    [SerializeField]
    private TMPro.TextMeshProUGUI itemCountText;
    [SerializeField]
    private Image icon;

    [SerializeProperty("Item")]
    private Item itemInSlot = null;

   
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
            if(root == value)
            {
                return;
            }
            root = value;
            //refresh image
            if(root)
            {
                
            }
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

    public int row;
    public int column;


    private void Start()
    {
        itemCountText = transform.Find("ItemCount").GetComponent<TMPro.TextMeshProUGUI>();
        icon = transform.Find("Icon").GetComponent<Image>();
        cllider = transform.Find("CollisionBox").gameObject;

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

    void RefreshItem()
    {
       if(!itemInSlot)
       {
            cllider.GetComponent<Image>().color = new Color(0.8584906f, 0.8584906f, 0.8584906f, 0.8117647f);
        }
        else
        {
            cllider.GetComponent<Image>().color = new Color(0.8396226f, 0.0f, 0.0f, 0.8117647f);
        }
    }
}
