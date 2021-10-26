using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotNode
{
    public SlotNode(int column, int row, GameObject item = null)
    {
        this.column = column;
        this.row = row;
        this.item = item;
    }
    public int row;
    public int column;
    public GameObject item;
}
public class Bag : MonoBehaviour
{
    public List<GameObject> listOfItems;

    private SlotNode[,] listSlotNode;

    private GameObject itemPrefab;
    private Transform ItemsLocation;

    //amount of items in the bag when it's created
    public int swords = 0;
    public int potions = 0;
    public int shields = 0;

    private int rows = 5;
    private int columns = 5;

    void Start()
    {
        listOfItems = new List<GameObject>();
        listSlotNode = new SlotNode[columns, rows];

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                listSlotNode[i, j] = new SlotNode(i, j);
            }
        }
        itemPrefab = Resources.Load<GameObject>("Prefabs/Item");
        ItemsLocation = GameObject.Find("Canvas/Items").transform;

        for(int i = 0; i < swords; i++)
        {
            AddASword();
        }
        for (int i = 0; i < potions; i++)
        {
            AddAPotion();
        }
        for (int i = 0; i < shields; i++)
        {
            AddAShield();
        }

        HideItems();

    }
    // Update is called once per frame
    private void AddASword()
    {
        GameObject sword = Instantiate(itemPrefab, ItemsLocation);
        sword.GetComponent<Item>().ItemTemplate = Resources.Load<ItemTemplate>("Items/Sword");
        AddAnItemToTheBagViaCode(sword);
    }
    private void AddAPotion()
    {
        GameObject potion = Instantiate(itemPrefab, ItemsLocation);
        potion.GetComponent<Item>().ItemTemplate = Resources.Load<ItemTemplate>("Items/Round Potion");
        AddAnItemToTheBagViaCode(potion);
    }
    private void AddAShield()
    {
        GameObject shield = Instantiate(itemPrefab, ItemsLocation);
        shield.GetComponent<Item>().ItemTemplate = Resources.Load<ItemTemplate>("Items/Shield");
        AddAnItemToTheBagViaCode(shield);
    }

    public void HideItems()
    {
        foreach (GameObject item in listOfItems)
        {
            item.SetActive(false);
        }
    }

    public void ShowItems()
    {
        foreach (GameObject item in listOfItems)
        {
            item.SetActive(true);
        }
    }

    private void AddAnItemToTheBagViaCode(GameObject item)
    {
        //the item is already created
        //First I need to know if there is space in the grid.
        item.GetComponent<Item>().Bag = this;
        int ItemRows = item.GetComponent<Item>().ItemTemplate.rows;
        int ItemColumns = item.GetComponent<Item>().ItemTemplate.columns;
        SlotNode availableNode = null;
        foreach (SlotNode node in listSlotNode)
        {
            if(node.item != null) // go to next node
            {
                continue;
            }
            //for this node check if there is space
            int currentRow = node.row;
            int currentColumn = node.column;
            bool available = true;
            for (int i = 0; i < ItemRows; i++) //check every extra row
            { 
                if(currentRow + i < rows) //not outside the grid
                {
                    for (int j = 0; j < ItemColumns; j++) //check every extra column
                    {
                        if (currentColumn + j < columns) //not outside the grid
                        {
                            if (listSlotNode[currentColumn + j, currentRow + i].item != null)
                            {
                                available = false;
                                break;
                            }
                        }
                        else
                        {
                            available = false;
                            break;
                        }
                    }
                }
                else
                {
                    available = false;
                    break;
                }
            }
            if(available)
            {
                availableNode = node;
                break;
            }
        }
        if(availableNode != null) //there is space
        {
            AddNewItemInFreeSpace(item, availableNode);
        }
        else
        {
            Debug.Log("There is no space in the bag for another Item");
        }
    }
    public void AddNewItemInFreeSpace(GameObject item, SlotNode root)
    {
        //add the item to the slots
        int currentRow = root.row;
        int currentColumn = root.column;
        int itemRows = item.GetComponent<Item>().ItemTemplate.rows;
        int itemColumns = item.GetComponent<Item>().ItemTemplate.columns;

        for(int i = 0; i < itemColumns; i++)
        {
            for(int j = 0; j < itemRows; j++)
            {
                listSlotNode[currentColumn + i, currentRow + j].item = item;
                item.GetComponent<Item>().slotNodes.Add(listSlotNode[currentColumn + i, currentRow + j]);
            }
        }
        AddItemToList(item);
    }
    public void DeleteFromlist(GameObject item)
    {
        listOfItems.Remove(item);
        RefreshList();
    }
    public void AddItemToList(GameObject item)
    {
        listOfItems.Add(item);
        RefreshList();
    }
    public void RefreshList()
    {
        foreach(GameObject item in listOfItems)
        {
            //item.GoBackToPreviousPosition();
        }
    }
}
