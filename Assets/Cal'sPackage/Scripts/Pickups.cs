using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public int totalPartsPickedUp;
    public int healthPickupHealAmount;
    ShipPatsUpdate shipPartsUpdate;
    GunScript gunScript;
    GunInventory gunInventory;
    HealthUpdate healthUpdate;
    public GameObject currentGun;
    
    // Start is called before the first frame update
    void Start()
    {
        totalPartsPickedUp = 0;
        shipPartsUpdate = GameObject.Find("HUD_ShipParts").GetComponent<ShipPatsUpdate>();
        healthUpdate = GameObject.Find("HUD_Health").GetComponent<HealthUpdate>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("ShipPickup"))
        {
            Destroy(other.gameObject);
            totalPartsPickedUp++;
            Debug.Log("Collide ShipPart");
            shipPartsUpdate.shipPartsLeft--;
            if (totalPartsPickedUp == 4)
            {
                GameManager.Instance.GameWon();
            }
        }

        if(other.gameObject.CompareTag("AmmoPickup"))
        {
            gunInventory = gameObject.GetComponent<GunInventory>();
            currentGun = gunInventory.currentGun;
            gunScript = currentGun.GetComponent<GunScript>();

            gunScript._bulletsIHave = gunScript.bulletStore;

            Destroy(other.gameObject);
            Debug.Log("Collide Ammo");
            
        }
        if(other.gameObject.CompareTag("HealthPickup"))
        {
            Debug.Log("Collide Health");
            
            healthUpdate.heal(healthPickupHealAmount);

            Destroy(other.gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
