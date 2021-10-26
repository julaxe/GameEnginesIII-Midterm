using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Display item in the slot, update image, make clickable when there is an item, invisible when there is not
public class Item : MonoBehaviour
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
    [SerializeField]
    private ItemTemplate itemTemplate;

    public ItemTemplate ItemTemplate
    {
        get
        {
            return itemTemplate;
        }
        set
        {
            if(itemTemplate == value)
            {
                return;
            }
            itemTemplate = value;
            //update
            Debug.Log("item changed for something else");
            
            RefreshItem();
            
        }
    }

    private Image icon;
    private BoxCollider2D boxcollider;


    private int numberOfSlots;
    public List<Slot> slotsInUse;
    [SerializeField]
    private List<Slot> previousSlots;

    private float width;
    private float height;
    private bool dragging =false;

    void Start()
    {
        slotsInUse = new List<Slot>();
        previousSlots = new List<Slot>();
        RefreshItem();
    }

    private void Update()
    {
        if(dragging)
        {
            transform.position = Input.mousePosition;
        }
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
    public void RefreshItem()
    {
        // get all the values when the item is change
        boxcollider = GetComponent<BoxCollider2D>();
        icon = GetComponent<Image>();

        icon.sprite = ItemTemplate.icon;
        //new size
        width = 100 * ItemTemplate.columns;
        height  = 100 * ItemTemplate.rows;
        icon.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        icon.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        boxcollider.size = new Vector2(width - 20, height - 20);

        numberOfSlots = ItemTemplate.columns * ItemTemplate.rows;
        
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Slot")
        {
            slotsInUse.Add(collision.transform.parent.GetComponent<Slot>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Slot")
        {
            slotsInUse.Remove(collision.transform.parent.GetComponent<Slot>());
        }
    }

    public void onClickEventStart()
    {
        dragging = true;
    }

    public void onClickEventEnd()
    {
        dragging = false;
        //check if it can be added to the current slots
        if(slotsInUse.Count == numberOfSlots)
        {
            //same amount of slots, now check if they are available.
            foreach(Slot slot in slotsInUse)
            {
                //check if the slot doesn't have an item, and if it has, check that is not being use by this item.
                if(slot.Item != null && !previousSlots.Find(x => x == slot))
                {
                    GoBackToPreviousPosition();
                    return;
                }
            }
            //if I'm here it means that the slots are available, so set the item.
            //also get the min,max for rows and columns.
            DeleteOldBag();
            ClearPreviousSlots();
            AddToBag();
        }
        else
        {
            GoBackToPreviousPosition();
        }
    }
    public void GoBackToPreviousPosition()
    {
        if(previousSlots.Count>0)
        {
            SetPositionWithSlots(previousSlots);
        }
    }
    private void SetPositionWithSlots(List<Slot> slots)
    {
        foreach (Slot slot in slots)
        {
            if (slot.Root)
            {
                Vector3 newPos = new Vector3(slot.transform.position.x + (width * 0.2f), slot.transform.position.y - (height * 0.2f), 0.0f);
                
                transform.position = newPos;
            }
        }
    }
    private void ClearPreviousSlots()
    {
        foreach(Slot slot in previousSlots)
        {
            slot.Root = false;
            slot.Tail = false;
            slot.Item = null;
        }
        previousSlots.Clear();
    }
    private void AddToBag()
    {
        //slotsInUse[0].bag.AddNewItem(this);
        SetNewSlots();
        SetPositionWithSlots(slotsInUse);
    }
    private void DeleteOldBag()
    {
        if(previousSlots.Count>1)
        {
            //previousSlots[0].bag.DeleteFromlist(this);
        }
    }
    private void SetNewSlots()
    {
        int minRow = slotsInUse[0].row;
        int maxRow = slotsInUse[0].row;
        int minCol = slotsInUse[0].column;
        int maxCol = slotsInUse[0].column;
        foreach (Slot slot in slotsInUse)
        {
            if (slot.row < minRow)
            {
                minRow = slot.row;
            }
            if (slot.row > maxRow)
            {
                maxRow = slot.row;
            }
            if (slot.column < minCol)
            {
                minCol = slot.column;
            }
            if (slot.column > maxCol)
            {
                maxCol = slot.column;
            }
            slot.Item = this;
            previousSlots.Add(slot);
        }
        foreach (Slot slot in slotsInUse)
        {
            if (slot.row == minRow && slot.column == minCol)
            {
                //he is the root.
                slot.Root = true;
            }
            if (slot.row == maxRow && slot.column == maxCol)
            {
                //he is the tail
                slot.Tail = true;
            }
        }
    }
}
