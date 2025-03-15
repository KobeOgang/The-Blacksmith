using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    [Header("AI Stats")]
    public float speed = 2;
    [SerializeField] TextMeshProUGUI BudgetPrice;
    [SerializeField] TextMeshProUGUI quantity;
    public Transform player;
    public Transform exit;
    public Transform spawn;


    [Header("Orders")]
    public List<GameObject> orders;

    [Header("Waypoints")]
    public List<GameObject> waypoints;

    [Header("Other AIs")]
    public List<GameObject> AIs;

    private int index = 0;
    private int randomInt;
    private int randomQuantity;
    private int randomAI;
    private bool waitingForOrder = false;
    private int budget = 200;
    private int currentQuantity = 0;
    private int finalbudget;
    private bool isWalking;
    private bool isLooking;

    private Animator animator;

    private EconomySystem economysystem;
    void Start()
    {
        isLooking = true;
        animator = GetComponent<Animator>();

        randomInt = UnityEngine.Random.Range(0, orders.Count);
        randomQuantity = UnityEngine.Random.Range(1, 3);
        randomAI = UnityEngine.Random.Range(0, AIs.Count);

        economysystem = FindObjectOfType<EconomySystem>();
    }

    void Update()
    {
        if (isLooking == true)
        {
            transform.LookAt(player);
        }
        else
        {
            transform.LookAt(exit);
        }

        if (isWalking == true)
        {
            animator.SetBool("IsWalking", true);
            
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        if (waitingForOrder) return; 

        Vector3 destination = waypoints[index].transform.position;
        Vector3 newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.position = newPos;

        float distance = Vector3.Distance(transform.position, destination);
        if (distance <= 0.05f)
        {
            
            isWalking = true;
            index++;
            Debug.Log(index);

            if (index == 4)
            {
                
                isWalking = false;
                speed = 0;
                waitingForOrder = true;
                orders[randomInt].SetActive(true);
                AddCollider();
                ChooseOrder(); 
            }

            if (index == 6)
            {
                RespawnAI();
                Destroy(gameObject);
            }
        }
    }


    private void ChooseOrder()
    {      
        switch (randomInt) { 
            
            case 0:
                Debug.Log("Ordered Iron Sword");
                budget = budget + 200;
                finalbudget = budget * randomQuantity;
                Debug.Log("Final budget: " + finalbudget);
                BudgetPrice.text = finalbudget.ToString();
                UpdateQuantity();
                break;
            case 1:
                Debug.Log("Ordered Copper Sword");
                budget = budget + 100;
                finalbudget = budget * randomQuantity;
                Debug.Log("Final budget: " + finalbudget);
                BudgetPrice.text = finalbudget.ToString();
                UpdateQuantity();
                break;
            case 2:
                Debug.Log("Ordered Steel Sword");
                budget = budget + 300;
                finalbudget = budget * randomQuantity;
                Debug.Log("Final budget: " + finalbudget);
                BudgetPrice.text = finalbudget.ToString();
                UpdateQuantity();
                break;
            case 3:
                Debug.Log("Ordered Copper Dagger");
                budget = budget + 25;
                finalbudget = budget * randomQuantity;
                Debug.Log("Final budget: " + finalbudget);
                BudgetPrice.text = finalbudget.ToString();
                UpdateQuantity();
                break;
            case 4:
                Debug.Log("Ordered Iron Dagger");
                budget = budget + 50;
                finalbudget = budget * randomQuantity;
                Debug.Log("Final budget: " + finalbudget);
                BudgetPrice.text = finalbudget.ToString();
                UpdateQuantity();
                break;
            case 5:
                Debug.Log("Ordered Steel Dagger");
                budget = budget + 85;
                finalbudget = budget * randomQuantity;
                Debug.Log("Final budget: " + finalbudget);
                BudgetPrice.text = finalbudget.ToString();
                UpdateQuantity();
                break;
            case 6:
                Debug.Log("Ordered Copper Longsword");
                budget = budget + 180;
                finalbudget = budget * randomQuantity;
                Debug.Log("Final budget: " + finalbudget);
                BudgetPrice.text = finalbudget.ToString();
                UpdateQuantity();
                break;
            case 7:
                Debug.Log("Ordered Iron Longsword");
                budget = budget + 280;
                finalbudget = budget * randomQuantity;
                Debug.Log("Final budget: " + finalbudget);
                BudgetPrice.text = finalbudget.ToString();
                UpdateQuantity();
                break;
            case 8:
                Debug.Log("Ordered Steel Longsword");
                budget = budget + 380;
                finalbudget = budget * randomQuantity;
                Debug.Log("Final budget: " + finalbudget);
                BudgetPrice.text = finalbudget.ToString();
                UpdateQuantity();
                break;
        }
    }

    public void SetQuantityText(TextMeshProUGUI quantityText)
    {
        quantity = quantityText;
        UpdateQuantity();
    }

    private void AddCollider()
    {
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.size = new Vector3(4.056851f, 2.183175f, 1.264439f);
        collider.center = new Vector3(0.1215744f, 0.7182327f, 1.282165f);
    }

    private void OnTriggerEnter(Collider other)
    {
        

        switch (randomInt)
        {
            case 0:
                if (waitingForOrder && other.CompareTag("IronSword"))
                {
                    
                    currentQuantity++;
                    Debug.Log("Current Quantity: " + currentQuantity);
                    UpdateQuantity();
                    if (currentQuantity == randomQuantity)
                    {
                        AcceptOrder();
                    }
                    Destroy(other.gameObject);
                }
                break;
            case 1:
                if (waitingForOrder && other.CompareTag("CopperSword"))
                {
                    currentQuantity++;
                    Debug.Log("Current Quantity: " + currentQuantity);
                    UpdateQuantity();
                    if (currentQuantity == randomQuantity)
                    {
                        AcceptOrder();
                    }
                    Destroy(other.gameObject);
                }
                break;
            case 2:
                if (waitingForOrder && other.CompareTag("SteelSword"))
                {
                    currentQuantity++;
                    Debug.Log("Current Quantity: " +  currentQuantity);
                    UpdateQuantity();
                    if (currentQuantity == randomQuantity)
                    {
                        AcceptOrder();
                    }
                    Destroy(other.gameObject);
                }
                break;
            case 3:
                if (waitingForOrder && other.CompareTag("CopperDagger"))
                {
                    currentQuantity++;
                    Debug.Log("Current Quantity: " + currentQuantity);
                    UpdateQuantity();
                    if (currentQuantity == randomQuantity)
                    {
                        AcceptOrder();
                    }
                    Destroy(other.gameObject);
                }
                break;
            case 4:
                if (waitingForOrder && other.CompareTag("IronDagger"))
                {
                    currentQuantity++;
                    Debug.Log("Current Quantity: " + currentQuantity);
                    UpdateQuantity();
                    if (currentQuantity == randomQuantity)
                    {
                        AcceptOrder();
                    }
                    Destroy(other.gameObject);
                }
                break;
            case 5:
                if (waitingForOrder && other.CompareTag("SteelDagger"))
                {
                    currentQuantity++;
                    Debug.Log("Current Quantity: " + currentQuantity);
                    UpdateQuantity();
                    if (currentQuantity == randomQuantity)
                    {
                        AcceptOrder();
                    }
                    Destroy(other.gameObject);
                }
                break;
            case 6:
                if (waitingForOrder && other.CompareTag("CopperLongsword"))
                {
                    currentQuantity++;
                    Debug.Log("Current Quantity: " + currentQuantity);
                    UpdateQuantity();
                    if (currentQuantity == randomQuantity)
                    {
                        AcceptOrder();
                    }
                    Destroy(other.gameObject);
                }
                break;
            case 7:
                if (waitingForOrder && other.CompareTag("IronLongsword"))
                {
                    currentQuantity++;
                    Debug.Log("Current Quantity: " + currentQuantity);
                    UpdateQuantity();
                    if (currentQuantity == randomQuantity)
                    {
                        AcceptOrder();
                    }
                    Destroy(other.gameObject);
                }
                break;
            case 8:
                if (waitingForOrder && other.CompareTag("SteelLongsword"))
                {
                    currentQuantity++;
                    Debug.Log("Current Quantity: " + currentQuantity);
                    UpdateQuantity();
                    if (currentQuantity == randomQuantity)
                    {
                        AcceptOrder();
                    }
                    Destroy(other.gameObject);
                }
                break;
        }

    }

    private void UpdateQuantity()
    {
      
        quantity.text = currentQuantity.ToString() + "/" + randomQuantity.ToString();
        Debug.Log("UI Updated: " + quantity.text);
        quantity.ForceMeshUpdate(); // Ensure UI refresh

    }

    private void AcceptOrder()
    {
        Debug.Log("Order received!");
        orders[randomInt].SetActive(false);
        waitingForOrder = false;
        speed = 2;
        isWalking = true;
        isLooking = false;

        if (economysystem != null)
        {
            economysystem.AddMoney(finalbudget);
            Debug.Log("Added " + finalbudget + " to player's money!");
        }
        else
        {
            Debug.LogWarning("EconomySystem not found!");
        }
    }

    private void RespawnAI()
    {
        GameObject newAI = Instantiate(AIs[randomAI], spawn.position, Quaternion.identity);
        AIScript aiScript = newAI.GetComponent<AIScript>();

        // Set essential references
        aiScript.spawn = spawn;
        aiScript.player = player;
        aiScript.exit = exit;
        aiScript.waypoints = new List<GameObject>(waypoints);
        aiScript.orders = new List<GameObject>(orders);
        aiScript.AIs = AIs;
        aiScript.BudgetPrice = BudgetPrice;
        aiScript.SetQuantityText(quantity);

        // Ensure the new AI generates a new order
        aiScript.GenerateNewOrder();
    }

    public void GenerateNewOrder()
    {
        randomInt = UnityEngine.Random.Range(0, orders.Count);
        randomQuantity = UnityEngine.Random.Range(1, 3);

        ChooseOrder(); // Recalculate and update UI
    }

    public void PlayerSleeps()
    {
        RespawnAI();
        Destroy(gameObject);
    }
    

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3f);
        speed = 2;
    }
}