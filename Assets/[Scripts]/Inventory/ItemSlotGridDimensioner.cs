using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instantiates prefabs to fill a grid
[RequireComponent(typeof(GridLayout))]
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

    void Start()
    {
        int numCells = GridDimensions.x * GridDimensions.y;
        rows = GridDimensions.y;
        columns = GridDimensions.x;
        listSlots = new GameObject[columns,rows];

        for(int i = 0; i < columns; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                listSlots[i, j] = Instantiate(itemSlotPrefab, this.transform);
                listSlots[i, j].GetComponent<Slot>().column = i;
                listSlots[i, j].GetComponent<Slot>().row = j;
            }
        }
    }

    
}
