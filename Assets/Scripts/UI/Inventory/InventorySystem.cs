using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private Item currentSelectedItem;

    public GameObject unitPrefab;
    public int horizontalUnits;
    public int verticalUnits;

    public Item[] startingItems;
    private List<Item> inventoryItems = new List<Item>();

    private Unit[,] unitsGrid;

    void Start()
    {
        unitsGrid = new Unit[horizontalUnits, verticalUnits];

        for (int i = 0; i < horizontalUnits; i++)
        {
            for (int j = 0; j < verticalUnits; j++)
            {
                GameObject newUnit = Instantiate(unitPrefab, transform);
                newUnit.transform.localPosition = new Vector3(i - horizontalUnits / 2, 0, j - verticalUnits / 2);
                Unit unitClass = newUnit.AddComponent<Unit>();
                unitClass.x = i;
                unitClass.y = j;
                unitClass.itemID = -1;
                unitsGrid[i, j] = unitClass;
            }
        }

        unitPrefab.SetActive(false);

        for (int k = 0; k < startingItems.Length; k++)
        {
            PlaceItem(startingItems[k]);
        }
    }

    public bool PlaceItem(Item newItem)
    {
        for (int i = 0; i < horizontalUnits; i++)
        {
            for (int j = verticalUnits - 1; j >= 0; j--)
            {
                if (CheckUnitAvailability(i, j, newItem.width, newItem.height))
                {
                    inventoryItems.Add(newItem);
                    newItem.assignedItemID = inventoryItems.Count;
                    AssignToUnits(i, j, newItem.width, newItem.height, newItem);
                    newItem.PositionItself(i - horizontalUnits / 2, j - verticalUnits / 2);
                    return true;
                }
            }
        }

        return false;
    }

    public bool CheckUnitAvailability(int i, int j, int width, int height)
    {
        for (int ii = i; ii < i + width; ii++)
        {
            for (int jj = j; jj < j + height; jj++)
            {
                if (ii < horizontalUnits && jj < verticalUnits)
                {
                    if (unitsGrid[ii, jj].itemID != -1)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
        }

        return true;
    }

    public void AssignToUnits(int i, int j, int width, int height, Item item)
    {
        for (int ii = i; ii < i + width; ii++)
        {
            for (int jj = j; jj < j + height; jj++)
            {
                unitsGrid[ii, jj].itemID = item.assignedItemID;
            }
        }
    }

    public void RemoveFromUnits(int itemIndex)
    {
        for (int i = 0; i < horizontalUnits; i++)
        {
            for (int j = 0; j < verticalUnits; j++)
            {
                if (unitsGrid[i, j].itemID == itemIndex)
                {
                    unitsGrid[i, j].itemID = -1;
                }
            }
        }
    }

    private int lastValidX = -1;
    private int lastValidY = -1;

    private void Update()
    {
        int layerMask = 1;

        if (currentSelectedItem != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, layerMask))
            {
                GameObject targetObject = hit.collider.gameObject;
                Unit targetUnit = targetObject.GetComponent<Unit>();
                if (targetUnit)
                {
                    int x = targetUnit.x;
                    int y = targetUnit.y;
                    if (CheckUnitAvailability(x, y, currentSelectedItem.width, currentSelectedItem.height))
                    {
                        currentSelectedItem.PositionItself(x - horizontalUnits / 2, y - verticalUnits / 2);
                        lastValidX = x;
                        lastValidY = y;
                    }
                }
            }

            if (Input.GetMouseButtonDown(0) && lastValidX != -1 && lastValidY != -1)
            {
                AssignToUnits(lastValidX, lastValidY, currentSelectedItem.width, currentSelectedItem.height, currentSelectedItem);
                currentSelectedItem.gameObject.layer = 0;

                currentSelectedItem = null;
                lastValidX = -1;
                lastValidY = -1;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, layerMask))
                {
                    GameObject targetObject = hit.collider.gameObject;
                    Item targetItem = targetObject.GetComponent<Item>();
                    if (targetItem)
                    {
                        currentSelectedItem = targetItem;
                        currentSelectedItem.gameObject.layer = 5;

                        RemoveFromUnits(currentSelectedItem.assignedItemID);
                    }
                }
            }
        }
    }
}
