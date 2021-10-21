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

    public Item itemInSlot = null;
    //connection to the other itemslots.
    public Dictionary<string, GameObject> NeighboorsSlots;
    public bool root = false; //image
    public bool tail = false; //text
    private void Awake()
    {
        NeighboorsSlots = new Dictionary<string, GameObject>();
    }
    private void Start()
    {
        itemCountText = transform.Find("ItemCount").GetComponent<TMPro.TextMeshProUGUI>();
        icon = transform.Find("Icon").GetComponent<Image>();
    }
    public void RefreshNumberOfItems(int ItemCount)
    {
        if(tail)
        {
            if (itemInSlot != null) // If an item is present
            {
                //update text
                itemCountText.text = ItemCount.ToString();
            }
        }
        else
        {
            itemCountText.text = "";
        }
    }

    public void DrawItem() //we only draw once, we don't change it everytime we update the number of items
    {
        if (root)
        {
            if (itemInSlot != null) // If an item is present
            {
                icon.sprite = itemInSlot.icon;
                //new size
                float newSizeX = 100 * itemInSlot.columns;
                float newSizeY = 100 * itemInSlot.rows;
                icon.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newSizeX);
                icon.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newSizeY);
                icon.rectTransform.localPosition = new Vector3(50 * itemInSlot.columns, -50 * itemInSlot.rows, 0.0f);

                icon.gameObject.SetActive(true);
            }
        }
        else
        {
            icon.gameObject.SetActive(false);
        }
    }
}
