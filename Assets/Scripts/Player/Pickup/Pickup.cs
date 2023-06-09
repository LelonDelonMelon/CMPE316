using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private Transform handTransform;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Transform orientation;
    private GameObject heldObject;
    private Rigidbody heldObjectRb;
    private Vector3 hitPoint;
    [SerializeField] private bool isPickedUp;
    [SerializeField] private GameObject CameraReference;
    private float xRotation;
    private PlayerCam pc;
    private bool isShooting;
    private PlayerCombat playerCombat;

    [SerializeField] public ParticleSystem muzzleFlash;
    private GameObject muzzle;
    void Start()
    {
        muzzleFlash = new ParticleSystem();
        muzzle = new GameObject();
        playerCombat =  GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerCombat>();
        cam = Camera.main;
        mask = LayerMask.GetMask("Pickable", "Enemy");

        pc = CameraReference.GetComponent<PlayerCam>();
    }

    void Update()
    {

        if (pc != null)
        {
            xRotation = pc.getXRotation();
        }
        else
        {
            Debug.Log("NULL");
        }

        //Debug.Log(xRotation);

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10f, mask))
        {

            if (Input.GetKey(KeyCode.E))
            {
                // If the object has a rigidbody, store it
                if(hit.transform.gameObject.layer == 7)
                {
                    if (heldObject == null)
                    {
                        heldObjectRb = hit.rigidbody;
                        hitPoint = hit.point;
                        hit.transform.GetComponent<Renderer>().material.color = Color.red;
                        heldObject = hit.transform.gameObject;



                        heldObject.transform.rotation = Quaternion.Euler(180, orientation.eulerAngles.y, handTransform.eulerAngles.z);

                        // Set the hit object's parent to the player's hand
                        heldObject.transform.parent = handTransform;

                        // Disable the hit object's rigidbody to prevent it from being affected by physics
                        if (heldObjectRb != null)
                        {
                            heldObjectRb.isKinematic = true;
                        }
                        muzzle = heldObject.transform.GetChild(0).gameObject;
                        muzzleFlash = muzzle.GetComponent<ParticleSystem>();
                        playerCombat.setMuzzleObjects(muzzleFlash, muzzle);

                        if (playerCombat.isShooting())
                        {
                            if (muzzleFlash != null)
                            {
                                muzzleFlash.Play();
                            }
                            else
                            {
                                Debug.Log("NULL");
                            }
                        }
                    }

                }


            }


        }
        if (heldObject != null)
        {
            isPickedUp = true;

            Vector3 distanceOffset = new Vector3(0, 0, 1.26f);
            Vector3 newPosition = handTransform.position + handTransform.TransformDirection(distanceOffset);

            heldObject.transform.position = newPosition;
            heldObject.transform.rotation = Quaternion.Euler(Mathf.Clamp(xRotation-90,-97,90), orientation.eulerAngles.y, orientation.eulerAngles.z);
            //heldObject.transform.rotation = Quaternion.Euler(90, orientation.eulerAngles.y, orientation.eulerAngles.z);
            
        }

        if (Input.GetKey(KeyCode.F))
        {
            isPickedUp = false;
        }

        if (isPickedUp == false)
        {
            if (heldObject != null)
            {
                // Re-enable the hit object's rigidbody when it is dropped
                if (heldObjectRb != null)
                {
                    heldObjectRb.isKinematic = false;
                }

                // Clear the reference to the held object and remove its parent
                heldObject.transform.parent = null;
                heldObject = null;
                heldObjectRb = null;
            }
        }
    }



}
