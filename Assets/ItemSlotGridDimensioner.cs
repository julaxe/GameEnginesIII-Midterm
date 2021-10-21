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
            }
        }
        ConnectAllSlots();
    }

    void ConnectAllSlots()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                ConnectSlot(i, j, i - 1, j, "left");
                ConnectSlot(i, j, i + 1, j, "right");
                ConnectSlot(i, j, i, j-1, "top");
                ConnectSlot(i, j, i, j+1, "bottom");
            }
        }
    }
    void ConnectSlot(int acutalI, int actualJ, int i, int j, string direction)
    {
        if(i < 0 || i>= columns || j < 0 || j >= rows)// outside the grid set the direction to null
        {
            listSlots[acutalI, actualJ].GetComponent<Slot>().NeighboorsSlots.Add(direction, null);
            return;
        }
        listSlots[acutalI, actualJ].GetComponent<Slot>().NeighboorsSlots.Add(direction, listSlots[i, j]);

    }
}
