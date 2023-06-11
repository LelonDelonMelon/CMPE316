using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int damage;

    [SerializeField] public ParticleSystem muzzleFlash;

 

    // Update is called once per frame
    void Update()
    {   
        
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pew");
            Shoot();
            
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
       // Debug.Log("Health: " + health);
    }
    public void setDamage(int newDamage)
    {
        this.damage = newDamage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy Melee")
        {
            Debug.Log("Hit");
            EnemyCombat ec = collision.gameObject.GetComponent<EnemyCombat>();
            if (ec != null)
            {
                ec.attack(this.gameObject);
            }
        }
        if(collision.gameObject.tag == "Enemy Ranged")
        {
            Debug.Log("Hit");
            EnemyCombat ec = collision.gameObject.GetComponent<EnemyCombat>();
            if (ec != null)
            {
                ec.attack(this.gameObject);
            }
        }
        if(collision.gameObject.tag == "Enemy Mine")
        {
            Debug.Log("Hit");
            EnemyCombat ec = collision.gameObject.GetComponent<EnemyCombat>();
            if (ec != null)
            {
                ec.attack(this.gameObject);
            }
        }
        if(collision.gameObject.tag == "Perk")
        {
            Perk perk = collision.gameObject.GetComponent<Perk>();
            if(perk != null)
            {
                Debug.Log("Perk");
                PerkManager pm = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PerkManager>();
                pm.addPerk(perk);
                Destroy(collision.gameObject);
            }
        }
    }

}
