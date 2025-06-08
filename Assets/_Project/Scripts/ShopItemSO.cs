using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scriptable Object/Menu Shop Item", order = 1)]
public class ShopItemSO : ScriptableObject
{
    public string path;
    public string title;
    public string description;
    public int cost;
}
