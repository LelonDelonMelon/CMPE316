using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLayerChange : MonoBehaviour
{
    private void Awake()
    {
        changeLayer();
        Debug.Log("Current layer: " + gameObject.layer);
    }
    public void changeLayer()
    {

        gameObject.layer = LayerMask.NameToLayer("Pickable");

    }
}
