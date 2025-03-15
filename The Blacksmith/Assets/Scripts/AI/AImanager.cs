using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AImanager : MonoBehaviour
{
    public Transform spawn;

    public List<GameObject> AIs = new List<GameObject>();
    private int randomInt;
    private int index;


    private void Start()
    {
        randomInt = UnityEngine.Random.Range(0, AIs.Count);
    }


    void Update()
    {
        if (index == 0)
        {
            SpawnAI();
        }
    }

    void SpawnAI()
    {
        index++;
        if (AIs.Count > 0) // Make sure there's an AI prefab available
        {
            GameObject newAI = Instantiate(AIs[randomInt], spawn.position, Quaternion.identity);
            AIs.Add(newAI); // Add the new AI to the list
        }
    }


}
