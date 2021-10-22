using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField]
    private List<Item> listOfItems;
    void Start()
    {
        listOfItems = new List<Item>();
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
    public void AddNewItem(Item item)
    {
        listOfItems.Add(item);
        RefreshList();
    }
    public void DeleteFromlist(Item item)
    {
        listOfItems.Remove(item);
        RefreshList();
    }
    public void RefreshList()
    {
        foreach(Item item in listOfItems)
        {
            item.GoBackToPreviousPosition();
        }
    }
}
