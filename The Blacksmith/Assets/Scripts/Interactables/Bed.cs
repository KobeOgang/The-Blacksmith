using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bed : MonoBehaviour
{
    public DayNight dayNight;
    public EconomySystem economysystem;
    public GameObject sleepPromptUI;


    private bool isPlayerLookingAtBed = false;

    void Start()
    {
        sleepPromptUI.SetActive(false);
 

    }

    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f))
        {
            isPlayerLookingAtBed = hit.collider.gameObject == gameObject;
        }
        else
        {
            isPlayerLookingAtBed = false;
        }

        if (isPlayerLookingAtBed && Input.GetKeyDown(KeyCode.E))
        {
            ShowSleepPrompt();
   
        }
    }

    void ShowSleepPrompt()
    {
        if (sleepPromptUI != null)
        {
            sleepPromptUI.SetActive(true);
            LockCursor(false); // Unlock cursor when UI opens
        }
    }

    public void ConfirmSleep()
    {
        if (dayNight != null)
        {
            dayNight.ForceNextDay();
        }

        AIScript[] allAI = FindObjectsOfType<AIScript>();
        foreach (AIScript ai in allAI)
        {
            ai.PlayerSleeps();
        }


        if (sleepPromptUI != null)
        {
            sleepPromptUI.SetActive(false);
            LockCursor(true);
        }

        // Ensure sleep UI is disabled after sleeping
        sleepPromptUI.SetActive(false);

        economysystem.AnalyzeGame();




    }

    public void CancelSleep()
    {
        if (sleepPromptUI != null)
        {
            sleepPromptUI.SetActive(false);

            LockCursor(true); // Lock cursor back after canceling
        }
    }

    void LockCursor(bool lockCursor)
    {
        // Prevent locking cursor if the shop is open
        ShopSystem shop = FindObjectOfType<ShopSystem>();
        if (shop != null && shop.shopUI.activeSelf)
        {
            return;
        }

        Cursor.visible = !lockCursor;
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
    }
}