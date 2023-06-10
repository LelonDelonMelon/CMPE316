using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public int width;
    public int height;
    public int assignedItemID;

    public void PositionItself(int i, int j){

        gameObject.transform.position = new Vector3(i,0,j);

    }
}
