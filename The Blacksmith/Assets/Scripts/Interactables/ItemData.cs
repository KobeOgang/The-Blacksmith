using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Ore,
    Ingot,
    Handle,
    Tool,
    Weapon,
    Furniture
}

public enum WeaponType
{
    None,
    Dagger,
    Sword,
    Longsword
}

public enum MaterialType
{
    None,
    Copper,
    Iron,
    Steel
    }

[CreateAssetMenu(fileName = "New Item", menuName = "Blacksmith/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemId;
    public string displayName;
    public ItemType itemType;
    public WeaponType weaponType = WeaponType.None;
    public MaterialType materialType = MaterialType.None;
    public GameObject prefab;
    public float smeltTime;
    public int requiredIngots;
    public string resultingItemId; }
