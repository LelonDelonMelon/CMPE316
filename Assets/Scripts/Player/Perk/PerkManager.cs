using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    [SerializeField] private List<Perk> perks;
    [SerializeField] private GameObject inventory;
    // Start is called before the first frame update
    public void addPerk(Perk perk)
    {
        perks.Add(perk);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            inventory.SetActive(!inventory.activeSelf);
            Debug.Log("Perks");
            foreach (Perk perk in perks)
            {
                Debug.Log(perk.getHealthMultiplier());
                Debug.Log(perk.getDamageMultiplier());
                Debug.Log(perk.getSpeedMultiplier());
            }
        }
    }
}
