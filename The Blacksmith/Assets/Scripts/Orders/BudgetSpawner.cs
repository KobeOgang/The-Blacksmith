using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudgetSpawner : MonoBehaviour
{
    public GameObject textPrefab;
    public GameObject quantityPrefab;
    public Transform parentCanvas;

    private List<GameObject> spawnedObjects = new List<GameObject>(); // Store spawned objects

    private GameObject order;

    private void Start()
    {
        order = gameObject;
    }

    private void Update()
    {
        if (order.activeSelf)
        {
            if (spawnedObjects.Count == 0) // Prevents infinite spawning
            {
                SpawnText();
                SpawnQuantity();
            }
        }
        else
        {
            DestroyAllSpawnedObjects();
        }
    }

    private void SpawnText()
    {
        GameObject text = Instantiate(textPrefab, parentCanvas);
        spawnedObjects.Add(text);
    }

    private void SpawnQuantity()
    {
        GameObject quantity = Instantiate(quantityPrefab, parentCanvas);
        spawnedObjects.Add(quantity);



        
        AIScript aiScript = FindObjectOfType<AIScript>();
        if (aiScript != null)
        {
            aiScript.SetQuantityText(quantity.GetComponent<TMPro.TextMeshProUGUI>());
        }
        else
        {
            Debug.Log("No AIscript found");
        }
    }

    private void DestroyAllSpawnedObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();
    }
}