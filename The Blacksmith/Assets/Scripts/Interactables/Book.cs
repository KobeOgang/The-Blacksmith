using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Book : MonoBehaviour
{
    [Header("Book Reference")]
    [SerializeField] GameObject bookinterface;
    public CrosshairManager crosshairManager;
    public PlayerCam cameraController;

    public Button backButton;


    public void Start()
    {
        bookinterface.SetActive(false);
        backButton.onClick.AddListener(CloseBook);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Cast a ray to check if player is looking at the book
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 2f))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    bookinterface.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    if (crosshairManager != null)
                    {
                        crosshairManager.HideCrosshair();
                    }

                    if (cameraController != null)
                    {
                        cameraController.LockMovement();
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && bookinterface.activeSelf)
        {
            CloseBook();
        }
    }
    public void Interact()
    {
        bookinterface.SetActive(true);
    }
    

    public void CloseBook()
    {
        bookinterface.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (crosshairManager != null)
        {
            crosshairManager.ShowCrosshair();
        }

        // Unlock camera movement
        if (cameraController != null)
        {
            cameraController.UnlockMovement();
        }
    }
}
