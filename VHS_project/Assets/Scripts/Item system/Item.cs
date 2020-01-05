using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType Type;
}

public enum ItemType
{
    Cigarette,
    Food,
    Map,
    Ring
}
