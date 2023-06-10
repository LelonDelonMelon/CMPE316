using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int damage;

    [SerializeField] public ParticleSystem muzzleFlash;
    private GameObject muzzle;
    private bool muzzleActive;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pew");
            muzzleActive = true;
            Shoot();
            
        }
        if(Input.GetMouseButtonUp(0))
        {
            muzzleActive = false;
        }
    }
    void Shoot()
    {
       // muzzleFlash.Play();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50))
        {
            if (hit.transform.tag == "Enemy")
            {
                Debug.Log("Hit");
                EnemyCombat ec = hit.transform.GetComponent<EnemyCombat>();

                if(ec != null)
                {
                    ec.takeDamage(damage);
                }
            }
        }
    }
    public void takeDamage(int damage) {
        this.health -= damage;
    }
    public void setDamage(int newDamage)
    {
        this.damage = newDamage;
    }

    public void setMuzzleObjects(ParticleSystem muzzleFlash, GameObject muzzle)
    {
        this.muzzleFlash = muzzleFlash;
        this.muzzle = muzzle;
    }
    public bool isShooting()
    {
        return muzzleActive;
    }
}
