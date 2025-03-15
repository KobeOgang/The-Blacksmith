using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EconomySystem : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI quotaText;
    public TextMeshProUGUI resultText; 

    [Header("Economy Settings")]
    public float startingMoney = 1000f;
    public string currencySymbol = "coins";
    public float quota = 500f;

    [Header("Upgrades Buy Buttons")]
    public GameObject TableSet;
    public GameObject ShrineSet;

    [Header("Game References")]
    public DayNight dayNight; 

    [Header("Debug Settings")]
    public bool enableDebugControls = true;
    public float debugMoneyIncrement = 100f;

    private float currentMoney;
    private float totalAddedMoney;
    private bool gameEnded = false;

    private void Start()
    {
        currentMoney = startingMoney;
        totalAddedMoney = 0f;
        UpdateMoneyDisplay();
        UpdateQuotaDisplay();
    }

    private void Update()
    {
        if (gameEnded) return;

        UpdateUpgradeAvailability(TableSet, 1000);
        UpdateUpgradeAvailability(ShrineSet, 1000);

        if (enableDebugControls)
        {
            if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                AddMoney(debugMoneyIncrement);
            }
            else if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                SpendMoney(debugMoneyIncrement);
            }
        }

        CheckGameOutcome();
    }

    public void AddMoney(float amount)
    {
        currentMoney += amount;
        totalAddedMoney += amount;
        UpdateMoneyDisplay();
        UpdateQuotaDisplay();
    }

    public string GetQuotaProgress()
    {
        return $"{totalAddedMoney} / {quota}";
    }

    public bool SpendMoney(float amount)
    {
        if (CanAfford(amount))
        {
            currentMoney -= amount;
            UpdateMoneyDisplay();
            return true;
        }
        return false;
    }

    public bool CanAfford(float amount)
    {
        return currentMoney >= amount;
    }

    public float GetCurrentMoney()
    {
        return currentMoney;
    }

    private void UpdateMoneyDisplay()
    {
        if (moneyText != null)
        {
            moneyText.text = $"{currencySymbol}{currentMoney:N2}";
        }
    }

    private void UpdateUpgradeAvailability(GameObject upgrade, float cost)
    {
        if (upgrade != null)
        {
            upgrade.SetActive(currentMoney >= cost);
        }
    }

    private void UpdateQuotaDisplay()
    {
        if (quotaText != null)
        {
            quotaText.text = $"Quota Progress: {GetQuotaProgress():N2}";
            quotaText.color = totalAddedMoney >= quota ? Color.green : Color.white;
        }
    }

    private void CheckGameOutcome()
    {
        if (dayNight == null) return;

        float currentHour = dayNight.GetCurrentHour();

        if (currentHour >= 23f) 
        {
            if (totalAddedMoney >= quota)
            {
                WinGame();
            }
            else
            {
                LoseGame();
            }
        }
    }

    private void WinGame()
    {
        SceneManager.LoadScene("Win");
        gameEnded = true;
        if (resultText != null) resultText.text = "You Win!";
        Debug.Log("Game Won!");
    }


    private void LoseGame()
    {
        SceneManager.LoadScene("Lose");
        gameEnded = true;
        if (resultText != null) resultText.text = "You Lose!";
        Debug.Log("Game Over! You didn't meet the quota.");
       
    }

    public void AnalyzeGame()
    {
        if (totalAddedMoney <= quota)
        {
            LoseGame();
            
        }
        else
        {
            WinGame();
        }
    }

   
}
