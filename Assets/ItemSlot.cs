using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Display item in the slot, update image, make clickable when there is an item, invisible when there is not
public class ItemSlot : MonoBehaviour
{

    [SerializeField]
    private int itemCount = 0;
    public int ItemCount //getter ans setter for itemCount
    {
        get
        {
            return itemCount;
        }
        set
        {
            itemCount = value;
        }
    }
    public Item item;
    public Slot[] slotsInUse;

    private int columns;
    private int rows;

    void Start()
    {

    }

    public void UseItemInSlot()
    {
        
    }

    public void ChangeNumberOfItems()
    {
        if (ItemCount < 1)
        {
            //set itemSlots in slot to null
        }
    }

    
}
