using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject controlsPanel;
    public GameObject anvilPanel;
    public GameObject forgePanel;
    public GameObject customerPanel;

    private GameObject currentPanel;

    // Flags to track whether each panel has been shown
    private bool controlsShown = false;
    private bool anvilShown = false;
    private bool forgeShown = false;
    private bool customerShown = false;

    private void Start()
    {
        Time.timeScale = 1f; // Ensure game starts unpaused
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("ControlsTrigger") && !controlsShown)
            {
                ShowPanel(controlsPanel);
                controlsShown = true;
            }
            else if (gameObject.CompareTag("AnvilTrigger") && !anvilShown)
            {
                ShowPanel(anvilPanel);
                anvilShown = true;
            }
            else if (gameObject.CompareTag("ForgeTrigger") && !forgeShown)
            {
                ShowPanel(forgePanel);
                forgeShown = true;
            }
            else if (gameObject.CompareTag("CustomerTrigger") && !customerShown)
            {
                ShowPanel(customerPanel);
                customerShown = true;
            }
        }
    }

    void ShowPanel(GameObject panel)
    {
        currentPanel = panel;
        panel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void ClosePanel()
    {
        if (currentPanel != null)
        {
            currentPanel.SetActive(false);
            currentPanel = null;
            Time.timeScale = 1f; // Unpause the game
        }
    }
}
