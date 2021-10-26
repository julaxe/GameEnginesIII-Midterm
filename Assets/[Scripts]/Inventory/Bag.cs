using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> listOfItems;

    Slot[,] listSlots;

    private GameObject sword, potion, spear;

    private int rows = 5;
    private int columns = 5;
    void Start()
    {
        //listOfItems = new List<GameObject>();
        //listSlots = new Slot[columns, rows];

        //for (int i = 0; i < columns; i++)
        //{
        //    for (int j = 0; j < rows; j++)
        //    {
        //        listSlots[i, j].column = i;
        //        listSlots[i, j].row = j;
        //    }
        //}

        sword = Resources.Load<GameObject>("Prefabs/Sword");
        potion = Resources.Load<GameObject>("Prefabs/Potion");
        spear = Resources.Load<GameObject>("Prefabs/Spear");
    }

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddAnItemToTheBagViaCode(GameObject item)
    {
        //First I need to know if there is space in the grid.


        AddNewItem(item);
    }
    public void AddNewItem(GameObject item)
    {
        listOfItems.Add(item);
        RefreshList();
    }
    public void DeleteFromlist(GameObject item)
    {
        listOfItems.Remove(item);
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
