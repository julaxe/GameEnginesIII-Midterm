using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotGridDimensioner : MonoBehaviour
{
    [SerializeField]
    GameObject itemSlotPrefab;

    [SerializeField]
    Vector2Int GridDimensions = new Vector2Int(6, 6);
    [SerializeField]
    GameObject [,] listSlots;

    private int rows;
    private int columns;

    //connections with another bags.
    public Bag currentBag;
    private void Start()
    {
        int numCells = GridDimensions.x * GridDimensions.y;
        rows = GridDimensions.y;
        columns = GridDimensions.x;
        listSlots = new GameObject[columns, rows];

        Transform Grid = transform.Find("InventoryBackground");
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                listSlots[i, j] = Instantiate(itemSlotPrefab, Grid);
                listSlots[i, j].GetComponent<Slot>().column = i;
                listSlots[i, j].GetComponent<Slot>().row = j;
            }
        }

        transform.Find("InventoryBackground/Items").SetAsLastSibling();

    }

    public void SetCurrentBag(Bag bag)
    {
        currentBag = bag;
        LoadBag();
    }
    public void LoadBag()
    {
        ClearSlots();

        foreach (GameObject item in currentBag.listOfItems)
        {
            //for each item we are going to update the slots and the slotInUse for the item
            List<Slot> newSlotsInUse = new List<Slot>();
            foreach (SlotNode node in item.GetComponent<Item>().slotNodes)
            {
                listSlots[node.column, node.row].GetComponent<Slot>().Item = item.GetComponent<Item>();
                newSlotsInUse.Add(listSlots[node.column, node.row].GetComponent<Slot>());
            }
            item.GetComponent<Item>().SetSlotsInUse(newSlotsInUse);
        }
        currentBag.ShowItems();
    }
    public void UnLoadBag()
    {
        currentBag.HideItems();
    }
    
    public void ClearSlots()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                listSlots[i, j].GetComponent<Slot>().Item = null;
                listSlots[i, j].GetComponent<Slot>().Bag = currentBag;
            }
        }
    }

    private void OnEnable()
    {
    }

}
