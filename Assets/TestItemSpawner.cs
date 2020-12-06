using System.Collections;
using System.Collections.Generic;
using Item_Management;
using UnityEngine;

public class TestItemSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ItemWorld.SpawnItemWorld(new Vector3(2, 2), new Item {itemType = Item.ItemType.JuggernautBuff});
        ItemWorld.SpawnItemWorld(new Vector3(-2, -1), new Item {itemType = Item.ItemType.SpeedBuff});
        ItemWorld.SpawnItemWorld(new Vector3(4, 4), new Item {itemType = Item.ItemType.ShootDamageBuff});
    }
}
