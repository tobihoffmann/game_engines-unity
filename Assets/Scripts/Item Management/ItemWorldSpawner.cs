﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Item_Management;
using Item_Management;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour
{
    public Item item;

    private void Start()
    {
        ItemWorld.SpawnItemWorld(transform.position, item);
        Destroy(gameObject);
    }
}
