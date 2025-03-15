using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ShopSystem : MonoBehaviour
{
    [Header("UI References")]
    public GameObject shopUI;
    public Button backButton;
   

    [Header("Item Spawning")]
    public Transform spawnPoint;
    public ItemDatabase itemDatabase;

    [System.Serializable]
    public class ShopItem
    {
        public string itemId;
        public float price;
    }
    public ShopItem[] shopItems;

    [Header("References")]
    public EconomySystem economySystem;
    public CrosshairManager crosshairManager;
    public PlayerCam cameraController;

    private bool isShopOpen = false;
  

    private void Start()
    {
        shopUI.SetActive(false);
        backButton.onClick.AddListener(CloseShop);
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 2f))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (!isShopOpen)
                    {
                        OpenShop();
                    }
                }
            }
        }
    }

    public void OpenShop()
    {
        isShopOpen = true;
        shopUI.SetActive(true);

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

    public void CloseShop()
    {
        isShopOpen = false;
        shopUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (crosshairManager != null)
        {
            crosshairManager.ShowCrosshair();
        }

        if (cameraController != null)
        {
            cameraController.UnlockMovement();
        }
    }

    public void TryBuyItem(string itemId)
    {
        ShopItem item = System.Array.Find(shopItems, x => x.itemId == itemId);
        if (item == null)
        {
            Debug.LogError($"Item with ID {itemId} not found in shop items!");
            return;
        }

        if (economySystem.CanAfford(item.price))
        {
            ItemData itemData = itemDatabase.GetItem(itemId);
            if (itemData != null)
            {
                GameObject spawnedItem = Instantiate(itemData.prefab, spawnPoint.position, spawnPoint.rotation);
                spawnedItem.GetComponent<ItemIdentifier>().itemData = itemData;

                economySystem.SpendMoney(item.price);
            }
            else
            {
                Debug.LogError($"Item with ID {itemId} not found in database!");
            }
        }
        else
        {
            Debug.Log("Not enough money!");
           
        }
    }
}
