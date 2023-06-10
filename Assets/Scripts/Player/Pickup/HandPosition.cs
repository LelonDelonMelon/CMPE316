using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPosition : MonoBehaviour
{
    [SerializeField] private Transform orientation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation.Set(orientation.rotation.x, orientation.rotation.y, orientation.rotation.z, orientation.rotation.w);
    }
}
