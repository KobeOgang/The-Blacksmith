using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerEquipSystem : MonoBehaviour
{
    [Header("Hammer Settings")]
    public GameObject hammerPrefab;      public Transform toolSpawnPoint; 
    private GameObject equippedHammer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (equippedHammer == null)
            {
                EquipHammer();
            }
            else
            {
                UnequipHammer();
            }
        }
    }

    void EquipHammer()
    {
        if (hammerPrefab != null && toolSpawnPoint != null)
        {
            equippedHammer = Instantiate(hammerPrefab, toolSpawnPoint.position, toolSpawnPoint.rotation);
            equippedHammer.transform.SetParent(toolSpawnPoint);

                        Rigidbody rb = equippedHammer.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }

            Debug.Log("Hammer equipped.");
        }
    }

    void UnequipHammer()
    {
        if (equippedHammer != null)
        {
            Destroy(equippedHammer);
            equippedHammer = null;
            Debug.Log("Hammer unequipped.");
        }
    }

    public bool IsHammerEquipped()
    {
        return equippedHammer != null;
    }
}
